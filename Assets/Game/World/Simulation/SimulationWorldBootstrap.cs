using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Simulation
{
    /// <summary>
    /// The one adapter that reads real UnityEngine.Terrain (via the existing
    /// UnityTerrainHeightSource) for authoritative-simulation purposes -- bakes each
    /// island's per-cell heights (sampled once, at bootstrap, at every grid cell
    /// center -- exactly where PlacementValidator/RoadPlacementValidator/
    /// SlopeEstimator already only ever sample) into a plain SimulationWorldData that
    /// ManagedSimulation (fully engine-free) consumes. Never called again after
    /// bootstrap -- the authoritative state never re-samples live Terrain (Phase 2E
    /// point 1/12).
    /// </summary>
    public static class SimulationWorldBootstrap
    {
        public readonly struct IslandSource
        {
            public readonly string Name;
            public readonly IslandSurfaceRuntimeData Surface;
            public readonly UnityEngine.Terrain Terrain;

            public IslandSource(string name, IslandSurfaceRuntimeData surface, UnityEngine.Terrain terrain)
            {
                Name = name;
                Surface = surface;
                Terrain = terrain;
            }
        }

        public static SimulationWorldData Build(WorldGrid grid, IReadOnlyList<IslandSource> islands)
        {
            var result = new List<SimulationIslandData>(islands.Count);
            foreach (var source in islands)
            {
                result.Add(BuildIsland(grid, source));
            }

            return new SimulationWorldData(grid.CellSize, result);
        }

        private static SimulationIslandData BuildIsland(WorldGrid grid, IslandSource source)
        {
            var runtimeData = source.Surface;
            var width = runtimeData.Width;
            var height = runtimeData.Height;

            var flags = new SurfaceCellFlags[runtimeData.Flags.Length];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = (SurfaceCellFlags)runtimeData.Flags[i];
            }

            var heightSource = new UnityTerrainHeightSource(source.Terrain);
            var cellHeights = new float[width * height];
            for (var localZ = 0; localZ < height; localZ++)
            {
                for (var localX = 0; localX < width; localX++)
                {
                    var cell = new GridCoordinate(runtimeData.OriginCellX + localX, runtimeData.OriginCellZ + localZ);
                    var center = grid.CellToWorldCenter(cell);
                    cellHeights[localZ * width + localX] = heightSource.TryGetHeight(center.X, center.Z, out var h) ? h : 0f;
                }
            }

            var id = IslandId.FromName(source.Name);
            return new SimulationIslandData(id, source.Name, runtimeData.OriginCellX, runtimeData.OriginCellZ, width, height, flags, cellHeights);
        }
    }
}
