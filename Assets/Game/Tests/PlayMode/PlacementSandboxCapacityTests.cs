using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Presentation.Placement;
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
    // Candidate origins are discovered by scanning ArchipelagoPlacementState.
    // ValidatePlacementAt around the island center rather than hardcoded -- the
    // noise-perturbed coastline means a hand-picked coordinate can land on Water/
    // NotBuildable even well inside the island's nominal radius, so this mirrors the
    // project's "diagnose the real state, don't guess" practice instead of encoding a
    // brittle assumed layout.
    public class PlacementSandboxCapacityTests
    {
        private const string SceneName = "WorldPrototype";
        private const int ScanRadiusCells = 60;

        [UnityTest]
        public IEnumerator SandboxIsland_FitsAllThreeBuildingTypesMixedWithRotationAndRemoval()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var controller = Object.FindFirstObjectByType<PlacementController>();
            Assert.IsNotNull(controller, "PlacementController missing from scene");

            var houseId = ContentId.Parse("bld.prototype.house");
            var productionId = ContentId.Parse("bld.prototype.production_block");
            var publicId = ContentId.Parse("bld.prototype.public_building");
            var owner = Varynth.Core.Simulation.Common.PlayerId.NewId();

            var houses = PlaceAsManyAsPossible(controller, houseId, owner, 5);
            Assert.AreEqual(5, houses.Count, "Expected at least 5 houses to fit on the enlarged sandbox island.");

            var productionBlocks = PlaceAsManyAsPossible(controller, productionId, owner, 5);
            Assert.AreEqual(5, productionBlocks.Count, "Expected at least 5 production blocks to fit alongside the houses.");

            var publicBuildings = PlaceAsManyAsPossible(controller, publicId, owner, 3);
            Assert.AreEqual(3, publicBuildings.Count, "Expected at least 3 public buildings to fit alongside the other types.");

            // Rotation: a 90-degree-rotated house placed in whatever space remains.
            var rotatedHouses = PlaceAsManyAsPossible(controller, houseId, owner, 1, BuildingRotation.Deg90);
            Assert.AreEqual(1, rotatedHouses.Count, "Expected room for one more, rotated house.");
            Assert.AreEqual(BuildingRotation.Deg90, rotatedHouses[0].Rotation);

            // Invalid placement: one of the already-placed houses' cells is occupied.
            var overlapOrigin = houses[0].Origin;
            var overlapPlaced = controller.State.TryPlace(houseId, overlapOrigin, BuildingRotation.Deg0, owner, controller.Registry,
                out _, out var overlapValidation);
            Assert.IsFalse(overlapPlaced, "Placing on top of an existing building must be rejected.");
            Assert.IsTrue((overlapValidation.Issues & PlacementIssue.AlreadyOccupied) != 0);

            // Removal frees the cell for re-placement.
            var toRemove = rotatedHouses[0];
            var removed = controller.State.TryRemove(toRemove.Id, out var removedInstance);
            Assert.IsTrue(removed);
            Assert.AreEqual(toRemove.Id, removedInstance.Id);

            var replaced = controller.State.TryPlace(houseId, toRemove.Origin, BuildingRotation.Deg0, owner, controller.Registry,
                out _, out var replacedValidation);
            Assert.IsTrue(replaced, $"Cell should be free again after removal. Issues: {replacedValidation.Issues}");
        }

        private static List<BuildingInstance> PlaceAsManyAsPossible(
            PlacementController controller, ContentId definitionId, Varynth.Core.Simulation.Common.PlayerId owner,
            int count, BuildingRotation rotation = BuildingRotation.Deg0)
        {
            var placed = new List<BuildingInstance>();
            for (var cz = -ScanRadiusCells; cz <= ScanRadiusCells && placed.Count < count; cz++)
            {
                for (var cx = -ScanRadiusCells; cx <= ScanRadiusCells && placed.Count < count; cx++)
                {
                    var origin = new GridCoordinate(cx, cz);
                    if (controller.State.TryPlace(definitionId, origin, rotation, owner, controller.Registry,
                            out var instance, out _))
                    {
                        placed.Add(instance);
                    }
                }
            }

            return placed;
        }
    }
}
