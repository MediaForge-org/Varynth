using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Roads
{
    /// <summary>
    /// The World-side road network state -- the road-side counterpart to
    /// ArchipelagoPlacementState. Built from the same runtime-safe
    /// IslandSurfaceRuntimeData/Terrain data, one RoadGraph per island. Never
    /// references ArchipelagoPlacementState directly (the two world-state systems
    /// stay decoupled) -- an optional IBuildingOccupancyQuery is accepted per-call
    /// instead. Command-agnostic: public API takes plain values only. Implements
    /// IRoadOccupancyQuery so the building side can perform the symmetric check
    /// without a reference back into this type.
    /// </summary>
    public sealed class RoadNetworkState : IRoadOccupancyQuery
    {
        private sealed class IslandEntry
        {
            public IslandSurfaceMap Surface;
            public RoadGraph Graph;
            public RectInt CellBounds;
            public IWorldHeightSource Heights;
        }

        private readonly WorldGrid _grid;
        private readonly RoadPlacementConfig _config;
        private readonly List<IslandEntry> _islands = new List<IslandEntry>();
        private readonly Dictionary<RoadSegmentId, int> _segmentIslandIndex = new Dictionary<RoadSegmentId, int>();

        private ulong _nextSegmentIdRaw = 1;

        public RoadNetworkState(WorldGrid grid, RoadPlacementConfig config = null)
        {
            _grid = grid;
            _config = config ?? new RoadPlacementConfig();
        }

        public int AddIsland(IslandSurfaceRuntimeData runtimeData, UnityEngine.Terrain terrain)
        {
            var originCell = new GridCoordinate(runtimeData.OriginCellX, runtimeData.OriginCellZ);
            var flags = new SurfaceCellFlags[runtimeData.Flags.Length];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = (SurfaceCellFlags)runtimeData.Flags[i];
            }

            var surface = IslandSurfaceMap.FromRawFlags(originCell, runtimeData.Width, runtimeData.Height, flags);
            var cellBounds = new RectInt(runtimeData.OriginCellX, runtimeData.OriginCellZ, runtimeData.Width, runtimeData.Height);
            var heights = new UnityTerrainHeightSource(terrain);

            _islands.Add(new IslandEntry { Surface = surface, Graph = new RoadGraph(), CellBounds = cellBounds, Heights = heights });
            return _islands.Count - 1;
        }

        public bool TryFindIslandIndex(GridCoordinate cell, out int islandIndex)
        {
            for (var i = 0; i < _islands.Count; i++)
            {
                var bounds = _islands[i].CellBounds;
                if (cell.X >= bounds.xMin && cell.X < bounds.xMax && cell.Z >= bounds.yMin && cell.Z < bounds.yMax)
                {
                    islandIndex = i;
                    return true;
                }
            }

            islandIndex = -1;
            return false;
        }

        public bool IsCellRoadOccupied(GridCoordinate cell)
        {
            return TryFindIslandIndex(cell, out var islandIndex) && _islands[islandIndex].Graph.TryGetNode(cell, out _);
        }

        public RoadGraph GetGraph(int islandIndex) => _islands[islandIndex].Graph;

        public IWorldHeightSource GetHeights(int islandIndex) => _islands[islandIndex].Heights;

        public int IslandCount => _islands.Count;

        public IReadOnlyCollection<GridCoordinate> ConsumeDirtyCells(int islandIndex) => _islands[islandIndex].Graph.ConsumeDirtyCells();

        public RoadPlacementValidationResult ValidateSegment(
            ContentId roadDefinitionId, GridCoordinate from, GridCoordinate to,
            ContentRegistry<RoadDefinition> registry, IBuildingOccupancyQuery buildingOccupancy)
        {
            if (!registry.TryGet(roadDefinitionId, out var definition))
            {
                return RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
            }

            if (!TryFindIslandIndex(from, out var fromIsland) || !TryFindIslandIndex(to, out var toIsland))
            {
                return RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
            }

            if (fromIsland != toIsland)
            {
                return RoadPlacementValidationResult.Invalid(RoadPlacementIssue.DifferentIsland);
            }

            if (!RoadDirectionExtensions.TryFromDelta(to.X - from.X, to.Z - from.Z, out var direction))
            {
                return RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
            }

            var island = _islands[fromIsland];
            return RoadPlacementValidator.ValidateSegment(from, to, direction, island.Surface, island.Graph, _grid, island.Heights, definition, buildingOccupancy, _config);
        }

        public bool TryFindRoute(
            ContentId roadDefinitionId, GridCoordinate start, GridCoordinate end,
            ContentRegistry<RoadDefinition> registry, IBuildingOccupancyQuery buildingOccupancy,
            out IReadOnlyList<GridCoordinate> path)
        {
            path = null;
            if (!registry.TryGet(roadDefinitionId, out var definition))
            {
                return false;
            }

            if (!TryFindIslandIndex(start, out var startIsland) || !TryFindIslandIndex(end, out var endIsland) || startIsland != endIsland)
            {
                return false;
            }

            var island = _islands[startIsland];
            return RoadRouter.TryFindRoute(start, end, island.CellBounds, island.Surface, island.Graph, _grid, island.Heights, definition, buildingOccupancy, _config, out path);
        }

        /// <summary>
        /// Atomic full-path commit (round-1 correction 3, round-2 correction 4/17):
        /// pass 1 pre-validates every genuinely missing pair against current world
        /// state (already-existing edges are treated as no-ops, never re-validated/
        /// flagged duplicate); if any missing pair is invalid, zero segments are
        /// created and the RoadSegmentId counter does not advance; only on full
        /// success does pass 2 actually mutate the graph, in path order.
        /// </summary>
        public bool TryBuildPath(
            ContentId roadDefinitionId,
            IReadOnlyList<GridCoordinate> orderedPath,
            PlayerId owner,
            ContentRegistry<RoadDefinition> registry,
            IBuildingOccupancyQuery buildingOccupancy,
            out IReadOnlyList<RoadSegment> created,
            out RoadPlacementValidationResult validation)
        {
            created = System.Array.Empty<RoadSegment>();
            validation = RoadPlacementValidationResult.Valid;

            if (!registry.TryGet(roadDefinitionId, out var definition))
            {
                validation = RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
                return false;
            }

            if (orderedPath == null || orderedPath.Count < 2)
            {
                return true; // a 0/1-cell "path" has no segments to create -- a no-op success, not a failure
            }

            if (!TryFindIslandIndex(orderedPath[0], out var islandIndex))
            {
                validation = RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
                return false;
            }

            var island = _islands[islandIndex];

            var toCreate = new List<(GridCoordinate From, GridCoordinate To, RoadDirection Direction)>();
            for (var i = 0; i < orderedPath.Count - 1; i++)
            {
                var from = orderedPath[i];
                var to = orderedPath[i + 1];

                if (!TryFindIslandIndex(from, out var fromIslandIndex) || fromIslandIndex != islandIndex
                    || !TryFindIslandIndex(to, out var toIslandIndex) || toIslandIndex != islandIndex)
                {
                    validation = RoadPlacementValidationResult.Invalid(RoadPlacementIssue.DifferentIsland);
                    return false;
                }

                if (island.Graph.HasSegmentBetween(from, to))
                {
                    continue; // existing edge -- no-op, never re-validated/flagged duplicate
                }

                if (!RoadDirectionExtensions.TryFromDelta(to.X - from.X, to.Z - from.Z, out var direction))
                {
                    validation = RoadPlacementValidationResult.Invalid(RoadPlacementIssue.OutsideSurfaceMap);
                    return false;
                }

                var segmentValidation = RoadPlacementValidator.ValidateSegment(from, to, direction, island.Surface, island.Graph, _grid, island.Heights, definition, buildingOccupancy, _config);
                if (!segmentValidation.IsValid)
                {
                    validation = segmentValidation;
                    return false;
                }

                toCreate.Add((from, to, direction));
            }

            var createdList = new List<RoadSegment>(toCreate.Count);
            foreach (var (from, to, direction) in toCreate)
            {
                var id = RoadSegmentId.FromRaw(_nextSegmentIdRaw);
                _nextSegmentIdRaw++;

                var segment = island.Graph.AddSegment(id, roadDefinitionId, from, to, direction, owner);
                _segmentIslandIndex[id] = islandIndex;
                createdList.Add(segment);
            }

            created = createdList;
            return true;
        }

        public bool TryRemoveSegment(RoadSegmentId id, out RoadSegment removed)
        {
            if (!_segmentIslandIndex.TryGetValue(id, out var islandIndex))
            {
                removed = null;
                return false;
            }

            var result = _islands[islandIndex].Graph.RemoveSegment(id, out removed);
            if (result)
            {
                _segmentIslandIndex.Remove(id);
            }

            return result;
        }
    }
}
