using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.Tests.PlayMode
{
    // Performance/GC sanity pass for drag/repeat batch placement -- not a formal
    // benchmark harness. Mirrors PlacementPerformanceTests' synthetic-map pattern.
    public class DragPerformanceTests
    {
        [UnityTest]
        public IEnumerator DragBatch_150To200Origins_PlacesQuicklyViaOneCommand()
        {
            var terrainData = new TerrainData { heightmapResolution = 33, size = new Vector3(800f, 40f, 800f) };
            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = 0.5f;
            terrainData.SetHeights(0, 0, heights);

            var terrainGo = new GameObject("SyntheticDragStressTestTerrain");
            var terrain = terrainGo.AddComponent<Terrain>();
            terrain.terrainData = terrainData;
            terrainGo.transform.position = new Vector3(0f, -15f, 0f);

            var runtimeData = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[200 * 200];
            for (var i = 0; i < flags.Length; i++) flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            runtimeData.SetData(0, 0, 200, 200, flags);

            var grid = new Varynth.World.Grid.WorldGrid(4f, Vector2.zero);
            var state = new ArchipelagoPlacementState(grid);
            state.AddIsland(runtimeData, terrain);

            var registry = new ContentRegistry<BuildingDefinition>();
            registry.Register(new BuildingDefinition(
                ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house",
                allowsCoastPlacement: false, placementBehavior: BuildingPlacementBehavior.DragRepeat));

            var handler = new BuildingPlacementCommandHandler(state, registry);

            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(198, 0), 2, 2);
            Assert.GreaterOrEqual(origins.Count, 99); // 200/2 = 100 columns worth of 2-wide houses

            var stopwatch = Stopwatch.StartNew();
            var command = new PlaceBuildingBatchCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, origins);
            handler.Handle(command, out var placed, out var rejected);
            stopwatch.Stop();

            UnityEngine.Debug.Log($"[PERF] Drag batch: planned {origins.Count} origins, placed {placed.Count} in {stopwatch.ElapsedMilliseconds} ms.");

            Assert.AreEqual(origins.Count, placed.Count, $"Fully-buildable synthetic map should place every planned origin. Rejected: {rejected.Count}");
            Assert.Less(stopwatch.ElapsedMilliseconds, 2000, "A 100+ origin batch should complete in well under a couple of seconds.");

            Object.Destroy(terrainGo);
            yield return null;
        }
    }
}
