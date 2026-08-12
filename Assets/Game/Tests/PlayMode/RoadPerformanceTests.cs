using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Surface;

namespace Varynth.Tests.PlayMode
{
    // Performance/GC sanity pass for the road network -- not a formal benchmark
    // harness. Real elapsed time documented, no invented FPS numbers.
    public class RoadPerformanceTests
    {
        [UnityTest]
        public IEnumerator SyntheticLargeMap_BuildsSeveralThousandSegments_RoutesAndRebuildsMeshQuickly()
        {
            var terrainData = new TerrainData { heightmapResolution = 33, size = new Vector3(800f, 40f, 800f) };
            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = 0.5f;
            terrainData.SetHeights(0, 0, heights);

            var terrainGo = new GameObject("SyntheticRoadStressTestTerrain");
            var terrain = terrainGo.AddComponent<Terrain>();
            terrain.terrainData = terrainData;
            terrainGo.transform.position = new Vector3(0f, -15f, 0f);

            var runtimeData = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[200 * 200];
            for (var i = 0; i < flags.Length; i++) flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            runtimeData.SetData(0, 0, 200, 200, flags);

            var grid = new WorldGrid(4f, Vector2.zero);
            var state = new RoadNetworkState(grid);
            state.AddIsland(runtimeData, terrain);

            var registry = new ContentRegistry<RoadDefinition>();
            var roadId = ContentId.Parse("road.prototype.basic");
            registry.Register(new RoadDefinition(roadId, LocalizationKey.Parse("road.name"), "road"));

            // 50 long horizontal roads, one TryBuildPath call each (not per-segment
            // commands, for speed) -> a few thousand real segments.
            var buildStopwatch = Stopwatch.StartNew();
            var totalCreated = 0;
            for (var row = 0; row < 50; row++)
            {
                var path = new GridCoordinate[200];
                for (var x = 0; x < 200; x++)
                {
                    path[x] = new GridCoordinate(x, row * 4);
                }

                state.TryBuildPath(roadId, path, PlayerId.NewId(), registry, null, out var created, out _);
                totalCreated += created.Count;
            }
            buildStopwatch.Stop();

            UnityEngine.Debug.Log($"[PERF] Road build: {totalCreated} segments across 50 rows in {buildStopwatch.ElapsedMilliseconds} ms.");
            Assert.GreaterOrEqual(totalCreated, 5000);
            Assert.Less(buildStopwatch.ElapsedMilliseconds, 5000);

            var routeStopwatch = Stopwatch.StartNew();
            var found = state.TryFindRoute(roadId, new GridCoordinate(0, 0), new GridCoordinate(199, 0), registry, null, out var path0);
            routeStopwatch.Stop();
            UnityEngine.Debug.Log($"[PERF] Route across existing 199-segment row: found={found} in {routeStopwatch.ElapsedMilliseconds} ms.");
            Assert.IsTrue(found);
            Assert.Less(routeStopwatch.ElapsedMilliseconds, 2000);

            var meshStopwatch = Stopwatch.StartNew();
            registry.TryGet(roadId, out var definition);
            var mesh = RoadMeshBuilder.BuildIslandMesh(grid, state.GetGraph(0), definition, state.GetHeights(0), 0.05f);
            meshStopwatch.Stop();
            UnityEngine.Debug.Log($"[PERF] Full-island road mesh rebuild: {mesh.vertexCount} vertices in {meshStopwatch.ElapsedMilliseconds} ms.");
            Assert.Greater(mesh.vertexCount, 0);
            Assert.Less(meshStopwatch.ElapsedMilliseconds, 5000);

            Object.Destroy(terrainGo);
            yield return null;
        }
    }
}
