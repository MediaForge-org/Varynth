using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.World.Surface;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Plain, engine-free bootstrap representation of one island's data, as needed by
    /// authoritative simulation state (Phase 2E) -- replaces passing a UnityEngine.Terrain
    /// and the ScriptableObject IslandSurfaceRuntimeData directly into
    /// ArchipelagoPlacementState/RoadNetworkState.AddIsland. Built exactly once by
    /// SimulationWorldBootstrap (Varynth.World, the one place UnityEngine.Terrain is
    /// still sampled for authoritative purposes) from real Terrain + the existing
    /// IslandSurfaceRuntimeData ScriptableObject; everything downstream only ever sees
    /// this plain data.
    ///
    /// Flags/CellHeights use the exact same dense row-major layout (index = localZ *
    /// Width + localX) as IslandSurfaceMap -- both are just parallel per-cell arrays.
    /// CellHeights is sufficient (not a continuous height field) because authoritative
    /// validation (PlacementValidator/RoadPlacementValidator/SlopeEstimator) only ever
    /// samples height at grid cell centers, verified directly in their source before
    /// this design was chosen -- never at arbitrary continuous world positions. Visual
    /// terrain-following mesh generation (RoadMeshBuilder/GridCellMeshBuilder) needs
    /// continuous sampling and keeps using the live UnityTerrainHeightSource in
    /// Varynth.World/Presentation, unaffected by this type.
    /// </summary>
    public sealed class SimulationIslandData
    {
        public IslandId Id { get; }
        public string Name { get; }
        public int OriginCellX { get; }
        public int OriginCellZ { get; }
        public int Width { get; }
        public int Height { get; }
        public IReadOnlyList<SurfaceCellFlags> Flags { get; }
        public IReadOnlyList<float> CellHeights { get; }

        public SimulationIslandData(
            IslandId id,
            string name,
            int originCellX,
            int originCellZ,
            int width,
            int height,
            IReadOnlyList<SurfaceCellFlags> flags,
            IReadOnlyList<float> cellHeights)
        {
            Id = id;
            Name = name;
            OriginCellX = originCellX;
            OriginCellZ = originCellZ;
            Width = width;
            Height = height;
            Flags = flags;
            CellHeights = cellHeights;
        }
    }

    /// <summary>
    /// Plain, engine-free bootstrap bundle for the whole archipelago -- injected into
    /// ManagedSimulation's constructor (Phase 2E point 7: ManagedSimulation never
    /// loads/bakes this itself).
    /// </summary>
    public sealed class SimulationWorldData
    {
        public float CellSize { get; }
        public IReadOnlyList<SimulationIslandData> Islands { get; }

        public SimulationWorldData(float cellSize, IReadOnlyList<SimulationIslandData> islands)
        {
            CellSize = cellSize;
            Islands = islands;
        }
    }
}
