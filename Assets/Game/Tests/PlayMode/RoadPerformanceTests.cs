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
using Varynth.World.Terrain;

namespace Varynth.Tests.PlayMode
{
    // Performance/GC sanity pass for the road network -- not a formal benchmark
    // harness. Real elapsed time documented, no invented FPS numbers.
    public class RoadPerformanceTests
    {
        [UnityTest]
        public IEnumerator SyntheticLargeMap_BuildsSeveralThousandSegments_RoutesAndRebuildsMeshQuickly()
        {
            // Phase 2E: RoadNetworkState is fully engine-free -- no
            // Terrain/GameObject/ScriptableObject needed for this headless stress test.
            var flags = new SurfaceCellFlags[200 * 200];
            for (var i = 0; i < flags.Length; i++) flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            var cellHeights = new float[200 * 200];
            for (var i = 0; i < cellHeights.Length; i++) cellHeights[i] = 0.5f;

            var grid = new WorldGrid(4f, (0f, 0f));
            var state = new RoadNetworkState(grid);
            var islandData = new SimulationIslandData(IslandId.FromName("SyntheticRoadStressTestIsland"), "SyntheticRoadStressTestIsland", 0, 0, 200, 200, flags, cellHeights);
            var heightsSource = new DenseGridHeightSource(grid, 0, 0, 200, 200, cellHeights);
            state.AddIsland(islandData, heightsSource);

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

            yield return null;
        }
    }
}
