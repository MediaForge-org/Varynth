using System.Collections.Generic;
using System.Text;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.Core.Simulation.Context;
using Varynth.Core.Simulation.Road;
using Varynth.Core.Simulation.Scheduling;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Terrain;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// The concrete, fully engine-free ISimulation implementation (Phase 2E).
    /// Coordinates the existing, unmodified-in-logic ArchipelagoPlacementState/
    /// RoadNetworkState/BuildingPlacementCommandHandler/RoadCommandHandler -- no
    /// gameplay/validation/routing logic is duplicated here, only orchestrated.
    ///
    /// Deterministic, fully injected bootstrap (point 7): never loads XML, never
    /// calls PlayerId.NewId() itself -- SimulationWorldData/the two ContentRegistry
    /// instances/localPlayerId all come from the caller (UnitySimulationDriver in
    /// practice, a plain object literal in headless tests).
    ///
    /// Headless by construction: everything this type touches (ArchipelagoPlacementState,
    /// RoadNetworkState, SimulationWorldData, GameClock, SimulationScheduler) lives in
    /// this same noEngineReferences assembly -- constructible in a plain EditMode test
    /// with zero Terrain/GameObject/ScriptableObject involved.
    /// </summary>
    public sealed class ManagedSimulation : ISimulation, ISimulationPlacementQueries, ISimulationRoadQueries
    {
        private readonly WorldGrid _grid;
        private readonly ArchipelagoPlacementState _buildingState;
        private readonly RoadNetworkState _roadState;
        private readonly BuildingPlacementCommandHandler _buildingHandler;
        private readonly RoadCommandHandler _roadHandler;
        private readonly ContentRegistry<BuildingDefinition> _buildingRegistry;
        private readonly ContentRegistry<RoadDefinition> _roadRegistry;

        private readonly GameClock _clock = new GameClock();
        private readonly SimulationScheduler _scheduler = new SimulationScheduler();

        // Per-tick command queue (point 6): each command is wrapped in a
        // CommandEnvelope with an explicit TargetTick + SubmitSequence, never applied
        // "all at once before the loop".
        private readonly List<CommandEnvelope> _pending = new List<CommandEnvelope>();
        private ulong _nextSubmitSequence = 1;
        private ulong _nextTicketRaw = 1;

        private readonly List<SimulationCommandResult> _genericResults = new List<SimulationCommandResult>();
        private readonly List<BuildingCommandResult> _buildingResults = new List<BuildingCommandResult>();
        private readonly List<RoadCommandResult> _roadResults = new List<RoadCommandResult>();

        // Real double buffering (point 4): each domain has its own pair of reusable
        // Lists and its own "active" index, flipped only when that domain's content
        // actually changed -- a previously handed-out SimulationSnapshot's
        // Buildings/Roads references therefore stay frozen forever, even while newer
        // ticks keep mutating the OTHER (currently-inactive) buffer.
        private readonly List<BuildingRenderSnapshot>[] _buildingBuffers =
        {
            new List<BuildingRenderSnapshot>(), new List<BuildingRenderSnapshot>()
        };
        private readonly List<RoadRenderSnapshot>[] _roadBuffers =
        {
            new List<RoadRenderSnapshot>(), new List<RoadRenderSnapshot>()
        };
        private int _buildingActiveIndex;
        private int _roadActiveIndex;
        private int _buildingStateVersion;
        private int _roadStateVersion;
        private int _lastSnapshottedBuildingVersion = -1;
        private int _lastSnapshottedRoadVersion = -1;
        private SimulationSnapshot _currentSnapshot;

        public ManagedSimulation(
            SimulationWorldData worldData,
            ContentRegistry<BuildingDefinition> buildingRegistry,
            ContentRegistry<RoadDefinition> roadRegistry,
            PlayerId localPlayerId,
            PlacementConfig placementConfig = null,
            RoadPlacementConfig roadPlacementConfig = null)
        {
            _grid = new WorldGrid(worldData.CellSize, (0f, 0f));
            _buildingState = new ArchipelagoPlacementState(_grid, placementConfig);
            _roadState = new RoadNetworkState(_grid, roadPlacementConfig);

            foreach (var island in worldData.Islands)
            {
                var heights = new DenseGridHeightSource(
                    _grid, island.OriginCellX, island.OriginCellZ, island.Width, island.Height, island.CellHeights);
                _buildingState.AddIsland(island, heights);
                _roadState.AddIsland(island, heights);
            }

            _buildingRegistry = buildingRegistry;
            _roadRegistry = roadRegistry;
            _buildingHandler = new BuildingPlacementCommandHandler(_buildingState, buildingRegistry, _roadState);
            _roadHandler = new RoadCommandHandler(_roadState, roadRegistry, _buildingState);

            LocalPlayerId = localPlayerId;

            RebuildSnapshot();
        }

        public PlayerId LocalPlayerId { get; }

        public GameTick CurrentTick => _clock.CurrentTick;

        /// <summary>Developer-debug only (Phase 2E point 35) -- not part of ISimulation.</summary>
        public int PendingCommandCount => _pending.Count;

        public SimulationCommandTicket Submit(ISimulationCommand command)
        {
            var ticket = new SimulationCommandTicket(_nextTicketRaw);
            _nextTicketRaw++;

            var envelope = new CommandEnvelope(_clock.CurrentTick.Add(1), _nextSubmitSequence, ticket, command);
            _nextSubmitSequence++;

            _pending.Add(envelope);
            return ticket;
        }

        public void AdvanceTicks(int tickCount)
        {
            if (tickCount <= 0)
            {
                return;
            }

            for (var i = 0; i < tickCount; i++)
            {
                var targetTick = _clock.CurrentTick.Add(1);
                ApplyDueCommands(targetTick);
                _scheduler.RunTick(new SimulationContext(targetTick, SimulationLevel.ActiveNear));
                _clock.Advance(1);
            }

            RebuildSnapshot();
        }

        private void ApplyDueCommands(GameTick targetTick)
        {
            if (_pending.Count == 0)
            {
                return;
            }

            var due = new List<CommandEnvelope>();
            var remaining = new List<CommandEnvelope>();
            foreach (var envelope in _pending)
            {
                if (envelope.TargetTick == targetTick)
                {
                    due.Add(envelope);
                }
                else
                {
                    remaining.Add(envelope);
                }
            }

            due.Sort((a, b) => a.SubmitSequence.CompareTo(b.SubmitSequence));

            _pending.Clear();
            _pending.AddRange(remaining);

            foreach (var envelope in due)
            {
                ApplyOne(envelope, targetTick);
            }
        }

        private void ApplyOne(CommandEnvelope envelope, GameTick processedAtTick)
        {
            switch (envelope.Command)
            {
                case PlaceBuildingCommand cmd:
                {
                    var accepted = _buildingHandler.Handle(cmd, out var instance, out var validation);
                    var outcome = accepted ? SimulationCommandOutcome.Accepted : SimulationCommandOutcome.Rejected;
                    _buildingResults.Add(new BuildingCommandResult(envelope.Ticket, processedAtTick, outcome, validation, accepted ? instance.Id : BuildingInstanceId.None));
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, outcome, accepted ? SimulationCommandRejectionReason.None : SimulationCommandRejectionReason.ValidationFailed));
                    if (accepted) _buildingStateVersion++;
                    break;
                }
                case PlaceBuildingBatchCommand cmd:
                {
                    _buildingHandler.Handle(cmd, out var placed, out var rejected);
                    foreach (var instance in placed)
                    {
                        _buildingResults.Add(new BuildingCommandResult(envelope.Ticket, processedAtTick, SimulationCommandOutcome.Accepted, PlacementValidationResult.Valid, instance.Id));
                    }
                    foreach (var (_, validation) in rejected)
                    {
                        _buildingResults.Add(new BuildingCommandResult(envelope.Ticket, processedAtTick, SimulationCommandOutcome.Rejected, validation, BuildingInstanceId.None));
                    }
                    var batchOutcome = placed.Count > 0 ? SimulationCommandOutcome.Accepted : SimulationCommandOutcome.Rejected;
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, batchOutcome, batchOutcome == SimulationCommandOutcome.Accepted ? SimulationCommandRejectionReason.None : SimulationCommandRejectionReason.ValidationFailed));
                    if (placed.Count > 0) _buildingStateVersion++;
                    break;
                }
                case RemoveBuildingCommand cmd:
                {
                    var removed = _buildingHandler.Handle(cmd, out var instance);
                    var outcome = removed ? SimulationCommandOutcome.Accepted : SimulationCommandOutcome.Rejected;
                    _buildingResults.Add(new BuildingCommandResult(envelope.Ticket, processedAtTick, outcome, removed ? PlacementValidationResult.Valid : PlacementValidationResult.Invalid(PlacementIssue.None), removed ? instance.Id : BuildingInstanceId.None));
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, outcome, removed ? SimulationCommandRejectionReason.None : SimulationCommandRejectionReason.TargetNotFound));
                    if (removed) _buildingStateVersion++;
                    break;
                }
                case BuildRoadCommand cmd:
                {
                    var accepted = _roadHandler.Handle(cmd, out var created, out var validation);
                    var ids = new List<RoadSegmentId>(created.Count);
                    foreach (var segment in created) ids.Add(segment.Id);
                    var outcome = accepted ? SimulationCommandOutcome.Accepted : SimulationCommandOutcome.Rejected;
                    _roadResults.Add(new RoadCommandResult(envelope.Ticket, processedAtTick, outcome, validation, ids, RoadSegmentId.None));
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, outcome, accepted ? SimulationCommandRejectionReason.None : SimulationCommandRejectionReason.ValidationFailed));
                    if (accepted && ids.Count > 0) _roadStateVersion++;
                    break;
                }
                case RemoveRoadCommand cmd:
                {
                    var removed = _roadHandler.Handle(cmd, out var segment);
                    var outcome = removed ? SimulationCommandOutcome.Accepted : SimulationCommandOutcome.Rejected;
                    _roadResults.Add(new RoadCommandResult(envelope.Ticket, processedAtTick, outcome, RoadPlacementValidationResult.Valid, System.Array.Empty<RoadSegmentId>(), removed ? segment.Id : RoadSegmentId.None));
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, outcome, removed ? SimulationCommandRejectionReason.None : SimulationCommandRejectionReason.TargetNotFound));
                    if (removed) _roadStateVersion++;
                    break;
                }
                default:
                    _genericResults.Add(new SimulationCommandResult(envelope.Ticket, processedAtTick, SimulationCommandOutcome.Rejected, SimulationCommandRejectionReason.UnknownCommandType));
                    break;
            }
        }

        private void RebuildSnapshot()
        {
            if (_buildingStateVersion != _lastSnapshottedBuildingVersion)
            {
                var next = 1 - _buildingActiveIndex;
                var buffer = _buildingBuffers[next];
                buffer.Clear();
                foreach (var (instance, island) in _buildingState.EnumerateInstances())
                {
                    buffer.Add(new BuildingRenderSnapshot(instance.Id, instance.DefinitionId, island, instance.Origin, instance.Rotation, instance.Owner));
                }
                _buildingActiveIndex = next;
                _lastSnapshottedBuildingVersion = _buildingStateVersion;
            }

            if (_roadStateVersion != _lastSnapshottedRoadVersion)
            {
                var next = 1 - _roadActiveIndex;
                var buffer = _roadBuffers[next];
                buffer.Clear();
                for (var i = 0; i < _roadState.IslandCount; i++)
                {
                    var islandId = _roadState.GetIslandId(i);
                    foreach (var segment in _roadState.EnumerateSegments(i))
                    {
                        buffer.Add(new RoadRenderSnapshot(segment.Id, segment.DefinitionId, islandId, segment.From, segment.To, segment.Direction, segment.Owner));
                    }
                }
                _roadActiveIndex = next;
                _lastSnapshottedRoadVersion = _roadStateVersion;
            }

            // Always a fresh wrapper: Tick must be current on every AdvanceTicks call,
            // even when neither domain changed (point 5). Cheap -- five reference
            // fields, never copies the underlying buffer contents.
            _currentSnapshot = new SimulationSnapshot(_clock.CurrentTick, _buildingStateVersion, _roadStateVersion, _buildingBuffers[_buildingActiveIndex], _roadBuffers[_roadActiveIndex]);
        }

        public SimulationSnapshot GetSnapshot() => _currentSnapshot;

        public IReadOnlyList<SimulationCommandResult> ConsumeResults()
        {
            var result = _genericResults.ToArray();
            _genericResults.Clear();
            return result;
        }

        public IReadOnlyList<BuildingCommandResult> ConsumeBuildingResults()
        {
            var result = _buildingResults.ToArray();
            _buildingResults.Clear();
            return result;
        }

        public IReadOnlyList<RoadCommandResult> ConsumeRoadResults()
        {
            var result = _roadResults.ToArray();
            _roadResults.Clear();
            return result;
        }

        // ---- ISimulationPlacementQueries / ISimulationRoadQueries ----

        public PlacementValidationResult ValidateBuildingPlacement(ContentId definitionId, GridCoordinate origin, BuildingRotation rotation)
            => _buildingState.ValidatePlacementAt(definitionId, origin, rotation, _buildingRegistry, _roadState);

        public bool TryFindIslandIndex(GridCoordinate cell, out int islandIndex)
            => _buildingState.TryFindIslandIndex(cell, out islandIndex);

        public bool TryGetOccupantAt(GridCoordinate cell, out BuildingInstanceId occupant)
            => _buildingState.TryGetOccupantAt(cell, out occupant);

        public bool IsBuildingConnectedToRoad(BuildingInstanceId instanceId)
        {
            if (!_buildingState.TryGetInstance(instanceId, out var instance) || !_buildingRegistry.TryGet(instance.DefinitionId, out var definition))
            {
                return false;
            }

            return BuildingRoadConnectionQuery.IsConnected(instance, definition, _roadState);
        }

        public bool TryFindRoadRoute(ContentId roadDefinitionId, GridCoordinate start, GridCoordinate end, out IReadOnlyList<GridCoordinate> path)
            => _roadState.TryFindRoute(roadDefinitionId, start, end, _roadRegistry, _buildingState, out path);

        public int GetRoadStateVersion(int islandIndex) => _roadState.GetStateVersion(islandIndex);

        public IslandId GetIslandId(int islandIndex) => _roadState.GetIslandId(islandIndex);

        // ---- Determinism / state-digest support (point 9, point 37) ----

        /// <summary>
        /// Digest over the FULL authoritative state (every instance/segment sorted by
        /// ID, plus CurrentTick, plus both next-ID counters) -- not just the render
        /// snapshot, so two simulations that are visually identical right now but
        /// consumed different ID sequences (e.g. one placed-then-removed a building)
        /// do not falsely digest equal. Not a production/crypto hash (FNV-1a) -- a
        /// test/regression tool only, per the project's own stated bar for this.
        /// </summary>
        public ulong ComputeStateDigest()
        {
            const ulong offsetBasis = 14695981039346656037UL;
            const ulong prime = 1099511628211UL;

            var hash = offsetBasis;
            hash = MixULong(hash, _clock.CurrentTick.Value);
            hash = MixULong(hash, _buildingState.NextInstanceIdRawPreview);
            hash = MixULong(hash, _roadState.NextSegmentIdRawPreview);

            var buildings = new List<BuildingInstance>();
            foreach (var (instance, _) in _buildingState.EnumerateInstances())
            {
                buildings.Add(instance);
            }
            buildings.Sort((a, b) => a.Id.Value.CompareTo(b.Id.Value));
            foreach (var b in buildings)
            {
                hash = MixULong(hash, b.Id.Value);
                hash = MixString(hash, b.DefinitionId.ToString());
                hash = MixULong(hash, PackCell(b.Origin));
                hash = MixULong(hash, (ulong)b.Rotation);
            }

            var segments = new List<RoadSegment>();
            for (var i = 0; i < _roadState.IslandCount; i++)
            {
                foreach (var s in _roadState.EnumerateSegments(i))
                {
                    segments.Add(s);
                }
            }
            segments.Sort((a, b) => a.Id.Value.CompareTo(b.Id.Value));
            foreach (var s in segments)
            {
                hash = MixULong(hash, s.Id.Value);
                hash = MixString(hash, s.DefinitionId.ToString());
                hash = MixULong(hash, PackCell(s.From));
                hash = MixULong(hash, PackCell(s.To));
                hash = MixULong(hash, (ulong)s.Direction);
            }

            return hash;

            static ulong PackCell(GridCoordinate cell)
                => (unchecked((ulong)(uint)cell.X) << 32) | unchecked((uint)cell.Z);

            static ulong MixULong(ulong h, ulong v)
            {
                for (var i = 0; i < 8; i++)
                {
                    h ^= (byte)(v >> (i * 8));
                    h *= prime;
                }
                return h;
            }

            static ulong MixString(ulong h, string s)
            {
                var bytes = Encoding.UTF8.GetBytes(s ?? string.Empty);
                foreach (var b in bytes)
                {
                    h ^= b;
                    h *= prime;
                }
                return h;
            }
        }
    }
}
