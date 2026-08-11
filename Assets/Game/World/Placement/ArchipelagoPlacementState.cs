using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Placement
{
    /// <summary>
    /// The World-side "world building state". Built entirely from runtime-safe data
    /// (IslandSurfaceRuntimeData + Terrain, never any Varynth.Tooling.Editor type),
    /// so it works identically in the Editor Play session and in a real Player build.
    /// Kept command-agnostic: its public API takes plain values (ContentId,
    /// GridCoordinate, BuildingRotation, PlayerId, BuildingInstanceId), never
    /// PlaceBuildingCommand/RemoveBuildingCommand/ISimulationCommand -- so it stays
    /// directly reusable later for AI-issued placement, replay, host-authoritative
    /// co-op reconciliation, or save/restore, without any coupling to the command
    /// boundary. (PlayerId itself is a plain identity value type from
    /// Varynth.Core.Simulation.Common, not a command -- using it here doesn't
    /// reintroduce that coupling.)
    /// </summary>
    public sealed class ArchipelagoPlacementState
    {
        private sealed class IslandEntry
        {
            public IslandSurfaceMap Surface;
            public IslandOccupancyMap Occupancy;
            public RectInt CellBounds;
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

        /// <summary>
        /// Adds one island's data. Called once per island at bootstrap (Awake), in
        /// the same order as the parallel Terrain[]/IslandSurfaceRuntimeData[] arrays
        /// the scene builder wires -- the resulting index is what ghost/grid-visibility
        /// code refers to as "the hovered island".
        /// </summary>
        public int AddIsland(IslandSurfaceRuntimeData runtimeData, UnityEngine.Terrain terrain)
        {
            var originCell = new GridCoordinate(runtimeData.OriginCellX, runtimeData.OriginCellZ);
            var flags = new SurfaceCellFlags[runtimeData.Flags.Length];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = (SurfaceCellFlags)runtimeData.Flags[i];
            }

            var surface = IslandSurfaceMap.FromRawFlags(originCell, runtimeData.Width, runtimeData.Height, flags);
            var occupancy = new IslandOccupancyMap(originCell, runtimeData.Width, runtimeData.Height);
            var cellBounds = new RectInt(runtimeData.OriginCellX, runtimeData.OriginCellZ, runtimeData.Width, runtimeData.Height);
            var heights = new UnityTerrainHeightSource(terrain);

            _islands.Add(new IslandEntry { Surface = surface, Occupancy = occupancy, CellBounds = cellBounds, Heights = heights });
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

        public PlacementValidationResult ValidatePlacementAt(
            ContentId definitionId, GridCoordinate origin, BuildingRotation rotation, ContentRegistry<BuildingDefinition> registry)
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
            return PlacementValidator.Validate(cells, island.Surface, island.Occupancy, island.Heights, _grid, definition, _config);
        }

        public bool TryPlace(
            ContentId definitionId,
            GridCoordinate origin,
            BuildingRotation rotation,
            PlayerId owner,
            ContentRegistry<BuildingDefinition> registry,
            out BuildingInstance instance,
            out PlacementValidationResult validation)
        {
            instance = null;
            validation = ValidatePlacementAt(definitionId, origin, rotation, registry);
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
    }
}
