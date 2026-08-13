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
using Varynth.Core.Simulation.Building;
using Varynth.Presentation;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

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
            //
            // Phase 2E: routes through the real ISimulation boundary (Submit + one
            // AdvanceTicks call applying every queued attempt in one batch, then
            // ConsumeBuildingResults) instead of a direct ArchipelagoPlacementState
            // call -- this is now the only legitimate way to mutate the real scene's
            // authoritative state from Presentation/tests.
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            Assert.IsNotNull(driver);
            var simulation = driver.Simulation;

            var houseId = ContentId.Parse("bld.prototype.house");
            var attempts = 0;
            var grid = new WorldGrid(4f, (0f, 0f));
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
                        simulation.Submit(new PlaceBuildingCommand(
                            simulation.LocalPlayerId, simulation.CurrentTick, houseId, new GridCoordinate(cx, cz), BuildingRotation.Deg0));
                    }
                }
            }

            simulation.AdvanceTicks(1); // every attempt above targeted this same next tick -- applied together, in submit order
            var results = simulation.ConsumeBuildingResults();
            var placedCount = 0;
            foreach (var result in results)
            {
                if (result.Outcome == Varynth.Core.Simulation.Boundary.SimulationCommandOutcome.Accepted)
                {
                    placedCount++;
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
            //
            // Phase 2E: ArchipelagoPlacementState is now fully engine-free -- no
            // Terrain/GameObject/ScriptableObject needed at all, a genuine headless
            // stress test.
            var grid = new WorldGrid(4f, (0f, 0f));
            var state = new ArchipelagoPlacementState(grid);

            var flags = new SurfaceCellFlags[200 * 200];
            for (var i = 0; i < flags.Length; i++) flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            var cellHeights = new float[200 * 200];

            var islandData = new SimulationIslandData(IslandId.FromName("SyntheticStressTestIsland"), "SyntheticStressTestIsland", 0, 0, 200, 200, flags, cellHeights);
            var heights = new DenseGridHeightSource(grid, 0, 0, 200, 200, cellHeights);
            state.AddIsland(islandData, heights);

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

            Assert.GreaterOrEqual(placedCount, 150, "A fully-buildable synthetic map should easily support 150 non-overlapping placements.");
            Assert.Less(stopwatch.ElapsedMilliseconds, 2000, "150 placements against a synthetic map should be fast -- no obvious runtime collapse.");

            yield return null;
        }
    }
}
