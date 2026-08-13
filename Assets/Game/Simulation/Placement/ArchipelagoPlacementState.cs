using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Placement
{
    /// <summary>
    /// The authoritative building state (Phase 2E: fully engine-free, lives in
    /// Varynth.Core.Simulation). Built entirely from plain SimulationIslandData
    /// (baked once by SimulationWorldBootstrap in Varynth.World from real
    /// Terrain/IslandSurfaceRuntimeData -- never any Varynth.Tooling.Editor type, and
    /// never a live UnityEngine.Terrain reference), so it works identically headless
    /// in a plain EditMode test, in the Editor Play session, and in a real Player build.
    /// Kept command-agnostic: its public API takes plain values (ContentId,
    /// GridCoordinate, BuildingRotation, PlayerId, BuildingInstanceId), never
    /// PlaceBuildingCommand/RemoveBuildingCommand/ISimulationCommand -- so it stays
    /// directly reusable later for AI-issued placement, replay, host-authoritative
    /// co-op reconciliation, or save/restore, without any coupling to the command
    /// boundary. (PlayerId itself is a plain identity value type from
    /// Varynth.Core.Simulation.Common, not a command -- using it here doesn't
    /// reintroduce that coupling.)
    ///
    /// Never references RoadNetworkState directly (the two world-state systems stay
    /// decoupled) -- an optional IRoadOccupancyQuery is accepted per-call instead,
    /// composed by whichever caller (PlacementController/BuildingPlacementCommandHandler)
    /// already holds both systems. Implements IBuildingOccupancyQuery so the road
    /// side can perform the symmetric check without a reference back into this type.
    /// </summary>
    public sealed class ArchipelagoPlacementState : IBuildingOccupancyQuery
    {
        private sealed class IslandEntry
        {
            public IslandId Id;
            public IslandSurfaceMap Surface;
            public IslandOccupancyMap Occupancy;
            public GridBounds CellBounds;
            public IWorldHeightSource Heights;
        }

        private readonly WorldGrid _grid;
        private readonly PlacementConfig _config;
        private readonly List<IslandEntry> _islands = new List<IslandEntry>();
        private readonly Dictionary<BuildingInstanceId, (BuildingInstance Instance, int IslandIndex)> _instances =
            new Dictionary<BuildingInstanceId, (BuildingInstance, int)>();

        private ulong _nextInstanceIdRaw = 1;

        public ArchipelagoPlacementState(WorldGrid grid, PlacementConfig config = null)
        {
            _grid = grid;
            _config = config ?? new PlacementConfig();
        }

        /// <summary>Read-only, test/digest-support only -- the next id that would be assigned.</summary>
        public ulong NextInstanceIdRawPreview => _nextInstanceIdRaw;

        /// <summary>
        /// Adds one island's data from plain, engine-free SimulationIslandData (Phase
        /// 2E -- no UnityEngine.Terrain/ScriptableObject dependency). Called once per
        /// island at bootstrap, in the same order as SimulationWorldData.Islands -- the
        /// resulting index is what ghost/grid-visibility code refers to as "the
        /// hovered island". heights is a DenseGridHeightSource baked by the caller.
        /// </summary>
        public int AddIsland(SimulationIslandData islandData, IWorldHeightSource heights)
        {
            var originCell = new GridCoordinate(islandData.OriginCellX, islandData.OriginCellZ);
            var flags = new SurfaceCellFlags[islandData.Flags.Count];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = islandData.Flags[i];
            }

            var surface = IslandSurfaceMap.FromRawFlags(originCell, islandData.Width, islandData.Height, flags);
            var occupancy = new IslandOccupancyMap(originCell, islandData.Width, islandData.Height);
            var cellBounds = new GridBounds(islandData.OriginCellX, islandData.OriginCellZ, islandData.Width, islandData.Height);

            _islands.Add(new IslandEntry { Id = islandData.Id, Surface = surface, Occupancy = occupancy, CellBounds = cellBounds, Heights = heights });
            return _islands.Count - 1;
        }

        /// <summary>
        /// Which island (if any) a cell belongs to -- linear-scan-first-hit-wins, same
        /// documented-acceptable pattern as CompositeWorldHeightSource for this island count.
        /// </summary>
        public bool TryFindIslandIndex(GridCoordinate cell, out int islandIndex)
        {
            for (var i = 0; i < _islands.Count; i++)
            {
                if (_islands[i].CellBounds.Contains(cell))
                {
                    islandIndex = i;
                    return true;
                }
            }

            islandIndex = -1;
            return false;
        }

        public PlacementValidationResult ValidatePlacementAt(
            ContentId definitionId, GridCoordinate origin, BuildingRotation rotation, ContentRegistry<BuildingDefinition> registry,
            IRoadOccupancyQuery roadOccupancy = null)
        {
            if (!registry.TryGet(definitionId, out var definition))
            {
                return PlacementValidationResult.Invalid(PlacementIssue.OutsideSurfaceMap);
            }

            var cells = BuildingFootprint.GetOccupiedCells(origin, definition.FootprintWidth, definition.FootprintLength, rotation);

            if (!TryFindIslandIndex(origin, out var islandIndex))
            {
                return PlacementValidationResult.Invalid(PlacementIssue.OutsideSurfaceMap);
            }

            var island = _islands[islandIndex];
            return PlacementValidator.Validate(cells, island.Surface, island.Occupancy, island.Heights, _grid, definition, _config, roadOccupancy);
        }

        public bool TryPlace(
            ContentId definitionId,
            GridCoordinate origin,
            BuildingRotation rotation,
            PlayerId owner,
            ContentRegistry<BuildingDefinition> registry,
            out BuildingInstance instance,
            out PlacementValidationResult validation,
            IRoadOccupancyQuery roadOccupancy = null)
        {
            instance = null;
            // Authoritative -- the same call path (and, when the caller passes the
            // same roadOccupancy instance, the same live road state) that any
            // Presentation-side ghost preview already used, never a separate/looser
            // check for the real command-application path.
            validation = ValidatePlacementAt(definitionId, origin, rotation, registry, roadOccupancy);
            if (!validation.IsValid)
            {
                return false;
            }

            registry.TryGet(definitionId, out var definition);
            TryFindIslandIndex(origin, out var islandIndex);
            var island = _islands[islandIndex];
            var cells = BuildingFootprint.GetOccupiedCells(origin, definition.FootprintWidth, definition.FootprintLength, rotation);

            var id = BuildingInstanceId.FromRaw(_nextInstanceIdRaw);
            _nextInstanceIdRaw++;

            island.Occupancy.Occupy(cells, id);
            instance = new BuildingInstance(id, definitionId, origin, rotation, owner, cells);
            _instances[id] = (instance, islandIndex);
            return true;
        }

        public bool TryRemove(BuildingInstanceId target, out BuildingInstance removed)
        {
            if (!_instances.TryGetValue(target, out var entry))
            {
                removed = null;
                return false;
            }

            _islands[entry.IslandIndex].Occupancy.Release(entry.Instance.OccupiedCells);
            _instances.Remove(target);
            removed = entry.Instance;
            return true;
        }

        public bool TryGetOccupantAt(GridCoordinate cell, out BuildingInstanceId occupant)
        {
            if (TryFindIslandIndex(cell, out var islandIndex))
            {
                return _islands[islandIndex].Occupancy.TryGetOccupant(cell, out occupant);
            }

            occupant = BuildingInstanceId.None;
            return false;
        }

        /// <summary>IBuildingOccupancyQuery -- used by RoadPlacementValidator via a per-call parameter, never a stored reference.</summary>
        public bool IsCellOccupied(GridCoordinate cell)
        {
            return TryGetOccupantAt(cell, out _);
        }

        public bool TryGetInstance(BuildingInstanceId id, out BuildingInstance instance)
        {
            if (_instances.TryGetValue(id, out var entry))
            {
                instance = entry.Instance;
                return true;
            }

            instance = null;
            return false;
        }

        public int IslandCount => _islands.Count;

        public IslandId GetIslandId(int islandIndex) => _islands[islandIndex].Id;

        /// <summary>All current instances with their owning island -- render-snapshot/digest support only.</summary>
        public IEnumerable<(BuildingInstance Instance, IslandId Island)> EnumerateInstances()
        {
            foreach (var entry in _instances.Values)
            {
                yield return (entry.Instance, _islands[entry.IslandIndex].Id);
            }
        }
    }
}
