using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Presentation.Placement;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.Tests.PlayMode
{
    // Performance/GC sanity pass -- not a formal benchmark harness.
    public class PlacementPerformanceTests
    {
        private const string SceneName = "WorldPrototype";

        [UnityTest]
        public IEnumerator RealArchipelago_PlacesAllAchievableBuildings_QuicklyAndWithoutErrors()
        {
            // Diagnosed via a real per-cell issue breakdown (not guessed). TestIsland_Large
            // was later enlarged/flattened (440x440, see WorldPrototypeIslands.cs) for the
            // 0.2.0 building-placement sandbox, which raised real archipelago capacity from
            // ~38 to 150+ non-overlapping 2x2 footprints. This test documents the real
            // achievable count and asserts it completes fast; the companion
            // SyntheticLargeMap test below additionally proves the underlying data
            // structures themselves handle 150+ instances without degradation, independent
            // of any particular island's real buildable land.
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<PlacementController>();
            Assert.IsNotNull(controller);

            var houseId = ContentId.Parse("bld.prototype.house");
            var placedCount = 0;
            var attempts = 0;
            var grid = new WorldGrid(4f, Vector2.zero);
            var stopwatch = Stopwatch.StartNew();

            var terrains = Object.FindObjectsByType<UnityEngine.Terrain>(FindObjectsSortMode.None);
            foreach (var terrain in terrains)
            {
                var position = terrain.transform.position;
                var size = terrain.terrainData.size;
                var minCell = grid.WorldToCell(position.x, position.z);
                var maxCell = grid.WorldToCell(position.x + size.x, position.z + size.z);

                for (var cz = minCell.Z; cz <= maxCell.Z; cz += 1)
                {
                    for (var cx = minCell.X; cx <= maxCell.X; cx += 1)
                    {
                        attempts++;
                        var placed = controller.State.TryPlace(
                            houseId, new GridCoordinate(cx, cz), BuildingRotation.Deg0,
                            Varynth.Core.Simulation.Common.PlayerId.NewId(), controller.Registry,
                            out _, out _);

                        if (placed)
                        {
                            placedCount++;
                        }
                    }
                }
            }

            stopwatch.Stop();
            UnityEngine.Debug.Log(
                $"[PERF] Real archipelago: placed {placedCount} buildings ({attempts} attempts across {terrains.Length} terrains) in {stopwatch.ElapsedMilliseconds} ms.");

            Assert.GreaterOrEqual(placedCount, 100, "Expected at least ~100 valid non-overlapping placements across the real prototype archipelago (sandbox island enlarged for Phase 2C).");
            Assert.Less(stopwatch.ElapsedMilliseconds, 5000, "An exhaustive scan of the whole archipelago should complete well within a few seconds.");
        }

        [UnityTest]
        public IEnumerator SyntheticLargeMap_Places150Buildings_WithoutObviousRuntimeCollapse()
        {
            // Direct architecture-level stress test for the brief's literal "100+
            // Prototype-Buildings platzierbar" requirement, decoupled from how much
            // buildable land the current small prototype islands happen to have: a
            // synthetic, fully-buildable 200x200 cell island, placed via direct state
            // calls (not simulated input, for speed).
            var grid = new WorldGrid(4f, Vector2.zero);
            var state = new ArchipelagoPlacementState(grid);

            var terrainData = new TerrainData { heightmapResolution = 33, size = new Vector3(800f, 40f, 800f) };
            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = 0.5f;
            terrainData.SetHeights(0, 0, heights);

            var terrainGo = new GameObject("SyntheticStressTestTerrain");
            var terrain = terrainGo.AddComponent<UnityEngine.Terrain>();
            terrain.terrainData = terrainData;
            terrainGo.transform.position = new Vector3(0f, -15f, 0f);

            var runtimeData = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[200 * 200];
            for (var i = 0; i < flags.Length; i++) flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            runtimeData.SetData(0, 0, 200, 200, flags);
            state.AddIsland(runtimeData, terrain);

            var registry = new ContentRegistry<BuildingDefinition>();
            registry.Register(new BuildingDefinition(ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house"));

            var placedCount = 0;
            var stopwatch = Stopwatch.StartNew();
            for (var cz = 0; cz < 200 && placedCount < 150; cz += 2)
            {
                for (var cx = 0; cx < 200 && placedCount < 150; cx += 2)
                {
                    if (state.TryPlace(ContentId.Parse("bld.prototype.house"), new GridCoordinate(cx, cz), BuildingRotation.Deg0,
                            Varynth.Core.Simulation.Common.PlayerId.NewId(), registry, out _, out _))
                    {
                        placedCount++;
                    }
                }
            }
            stopwatch.Stop();

            UnityEngine.Debug.Log($"[PERF] Synthetic large map: placed {placedCount} buildings in {stopwatch.ElapsedMilliseconds} ms.");

            Object.Destroy(terrainGo);

            Assert.GreaterOrEqual(placedCount, 150, "A fully-buildable synthetic map should easily support 150 non-overlapping placements.");
            Assert.Less(stopwatch.ElapsedMilliseconds, 2000, "150 placements against a synthetic map should be fast -- no obvious runtime collapse.");

            yield return null;
        }
    }
}
