using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Simulation.Boundary;
using Varynth.Core.Simulation.Building;
using Varynth.Presentation;
using Varynth.World.Placement;

namespace Varynth.Tests.PlayMode
{
    // Regression coverage for the 0.2.0 "Bau-Sandbox-Fläche vergrößern" follow-up:
    // TestIsland_Large's real buildable area must be large enough that all 3
    // prototype building types fit side by side, in multiple copies, with rotation,
    // occupancy rejection and removal all exercisable on one island -- not just
    // architecturally (see PlacementPerformanceTests' synthetic stress test), but on
    // the real generated archipelago content.
    //
    // Candidate origins are discovered by scanning ISimulationPlacementQueries.
    // ValidateBuildingPlacement around the island center rather than hardcoded -- the
    // noise-perturbed coastline means a hand-picked coordinate can land on Water/
    // NotBuildable even well inside the island's nominal radius, so this mirrors the
    // project's "diagnose the real state, don't guess" practice instead of encoding a
    // brittle assumed layout.
    //
    // Phase 2E: routes every mutation through ISimulation.Submit + AdvanceTicks(1) +
    // ConsumeBuildingResults (one tick per attempt -- correctness over speed here,
    // since each placement must be visible to the next scan step) instead of a
    // direct ArchipelagoPlacementState call.
    public class PlacementSandboxCapacityTests
    {
        private const string SceneName = "WorldPrototype";
        private const int ScanRadiusCells = 60;

        private readonly struct PlacedRecord
        {
            public readonly BuildingInstanceId Id;
            public readonly GridCoordinate Origin;
            public readonly BuildingRotation Rotation;

            public PlacedRecord(BuildingInstanceId id, GridCoordinate origin, BuildingRotation rotation)
            {
                Id = id;
                Origin = origin;
                Rotation = rotation;
            }
        }

        [UnityTest]
        public IEnumerator SandboxIsland_FitsAllThreeBuildingTypesMixedWithRotationAndRemoval()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            Assert.IsNotNull(driver, "UnitySimulationDriver missing from scene");
            var simulation = driver.Simulation;

            var houseId = ContentId.Parse("bld.prototype.house");
            var productionId = ContentId.Parse("bld.prototype.production_block");
            var publicId = ContentId.Parse("bld.prototype.public_building");

            var houses = PlaceAsManyAsPossible(simulation, houseId, 5);
            Assert.AreEqual(5, houses.Count, "Expected at least 5 houses to fit on the enlarged sandbox island.");

            var productionBlocks = PlaceAsManyAsPossible(simulation, productionId, 5);
            Assert.AreEqual(5, productionBlocks.Count, "Expected at least 5 production blocks to fit alongside the houses.");

            var publicBuildings = PlaceAsManyAsPossible(simulation, publicId, 3);
            Assert.AreEqual(3, publicBuildings.Count, "Expected at least 3 public buildings to fit alongside the other types.");

            // Rotation: a 90-degree-rotated house placed in whatever space remains.
            var rotatedHouses = PlaceAsManyAsPossible(simulation, houseId, 1, BuildingRotation.Deg90);
            Assert.AreEqual(1, rotatedHouses.Count, "Expected room for one more, rotated house.");
            Assert.AreEqual(BuildingRotation.Deg90, rotatedHouses[0].Rotation);

            // Invalid placement: one of the already-placed houses' cells is occupied.
            var overlapOrigin = houses[0].Origin;
            simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, houseId, overlapOrigin, BuildingRotation.Deg0));
            simulation.AdvanceTicks(1);
            var overlapResults = simulation.ConsumeBuildingResults();
            Assert.AreEqual(1, overlapResults.Count);
            Assert.AreEqual(SimulationCommandOutcome.Rejected, overlapResults[0].Outcome, "Placing on top of an existing building must be rejected.");
            Assert.IsTrue((overlapResults[0].Validation.Issues & PlacementIssue.AlreadyOccupied) != 0);

            // Removal frees the cell for re-placement.
            var toRemove = rotatedHouses[0];
            simulation.Submit(new RemoveBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, toRemove.Id));
            simulation.AdvanceTicks(1);
            var removeResults = simulation.ConsumeBuildingResults();
            Assert.AreEqual(1, removeResults.Count);
            Assert.AreEqual(SimulationCommandOutcome.Accepted, removeResults[0].Outcome);
            Assert.AreEqual(toRemove.Id, removeResults[0].CreatedInstanceId);

            simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, houseId, toRemove.Origin, BuildingRotation.Deg0));
            simulation.AdvanceTicks(1);
            var replaceResults = simulation.ConsumeBuildingResults();
            Assert.AreEqual(1, replaceResults.Count);
            Assert.AreEqual(SimulationCommandOutcome.Accepted, replaceResults[0].Outcome, $"Cell should be free again after removal. Issues: {replaceResults[0].Validation.Issues}");
        }

        private static List<PlacedRecord> PlaceAsManyAsPossible(
            ISimulation simulation, ContentId definitionId, int count, BuildingRotation rotation = BuildingRotation.Deg0)
        {
            var placed = new List<PlacedRecord>();
            for (var cz = -ScanRadiusCells; cz <= ScanRadiusCells && placed.Count < count; cz++)
            {
                for (var cx = -ScanRadiusCells; cx <= ScanRadiusCells && placed.Count < count; cx++)
                {
                    var origin = new GridCoordinate(cx, cz);
                    simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, definitionId, origin, rotation));
                    simulation.AdvanceTicks(1);
                    var results = simulation.ConsumeBuildingResults();
                    if (results.Count == 1 && results[0].Outcome == SimulationCommandOutcome.Accepted)
                    {
                        placed.Add(new PlacedRecord(results[0].CreatedInstanceId, origin, rotation));
                    }
                }
            }

            return placed;
        }
    }
}
