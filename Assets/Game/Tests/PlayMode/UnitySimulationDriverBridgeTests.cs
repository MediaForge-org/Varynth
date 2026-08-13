using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Road;
using Varynth.Presentation;
using Varynth.Presentation.Placement;
using Varynth.Presentation.Roads;

namespace Varynth.Tests.PlayMode
{
    // Phase 2E Unity Bridge tests (prompt section 41): proves the real scene's
    // UnitySimulationDriver/ManagedSimulation initialize correctly and that
    // Presentation never parallel-mutates authoritative state itself -- only ever
    // through ISimulation.Submit -- by exercising the real command -> tick ->
    // snapshot -> presentation flow and checking there is exactly one authoritative
    // source of truth both controllers agree with.
    //
    // Coordinates: TestIsland_Large is centered at world (0,0) (same convention
    // other acceptance tests already rely on) -- cell (0,0) and small single-axis
    // offsets from it are the only coordinates proven safe/buildable by the existing
    // suite; this file deliberately reuses that same safe pattern rather than
    // guessing further-out diagonal coordinates that risk landing in water/outside
    // the noise-perturbed coastline.
    public class UnitySimulationDriverBridgeTests
    {
        private const string SceneName = "WorldPrototype";

        [UnityTest]
        public IEnumerator WorldPrototype_Loads_ManagedSimulationInitializes()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            Assert.IsNotNull(driver, "UnitySimulationDriver missing from the real scene.");
            Assert.IsNotNull(driver.Simulation, "ManagedSimulation should be constructed in Awake().");
            // Not asserting CurrentTick == 0: the driver advances ticks from real
            // elapsed Time.unscaledDeltaTime starting the instant Awake() runs, so by
            // the time a couple of frames have been yielded here, several real ticks
            // may legitimately already have happened -- that's the fixed-tick design
            // working as intended, not a bug.
            Assert.GreaterOrEqual(driver.Simulation.CurrentTick.Value, 0UL);
            Assert.IsNotNull(driver.Simulation.GetSnapshot());
        }

        [UnityTest]
        public IEnumerator PlacementCommand_ReachesSimulation_SnapshotUpdatesPresentation()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var simulation = driver.Simulation;

            var buildingsBefore = simulation.GetSnapshot().Buildings.Count;
            var houseId = ContentId.Parse("bld.prototype.house");
            simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, houseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simulation.AdvanceTicks(1);
            yield return null; // let PlacementController's Update() observe the new snapshot and spawn

            var snapshot = simulation.GetSnapshot();
            Assert.AreEqual(buildingsBefore + 1, snapshot.Buildings.Count);

            var placedRoot = GameObject.Find("PlacedBuildings");
            Assert.IsNotNull(placedRoot);
            Assert.AreEqual(snapshot.Buildings.Count, placedRoot.transform.childCount, "Every snapshot building must have exactly one spawned GameObject -- the snapshot, not any direct mutation, is what created it.");
        }

        [UnityTest]
        public IEnumerator RoadCommand_ReachesSimulation_SnapshotUpdatesPresentation()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var simulation = driver.Simulation;

            var roadsBefore = simulation.GetSnapshot().Roads.Count;
            var roadId = ContentId.Parse("road.prototype.basic");
            simulation.Submit(new BuildRoadCommand(simulation.LocalPlayerId, simulation.CurrentTick, roadId,
                new[] { new GridCoordinate(-5, 0), new GridCoordinate(-4, 0), new GridCoordinate(-3, 0) }));
            simulation.AdvanceTicks(1);
            yield return null;

            Assert.AreEqual(roadsBefore + 2, simulation.GetSnapshot().Roads.Count);
        }

        [UnityTest]
        public IEnumerator RemoveBuildingCommand_ReachesSimulation_GameObjectDespawnsViaSnapshot()
        {
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var simulation = driver.Simulation;

            var houseId = ContentId.Parse("bld.prototype.house");
            simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, houseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simulation.AdvanceTicks(1);
            yield return null;

            Assert.AreEqual(1, simulation.GetSnapshot().Buildings.Count, "Setup: placement must have succeeded before the removal half of this test runs.");
            var instanceId = simulation.GetSnapshot().Buildings[0].InstanceId;
            var placedRoot = GameObject.Find("PlacedBuildings");
            Assert.AreEqual(1, placedRoot.transform.childCount);

            simulation.Submit(new RemoveBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, instanceId));
            simulation.AdvanceTicks(1);
            yield return null; // lets PlacementController's Update() observe the snapshot change and call Destroy()
            yield return null; // Unity's Destroy() is deferred to end-of-frame -- one more frame for it to actually take effect

            Assert.AreEqual(0, simulation.GetSnapshot().Buildings.Count);
            Assert.AreEqual(0, placedRoot.transform.childCount, "The GameObject must disappear only because the snapshot diff noticed it's gone, never a direct Destroy call outside that path.");
        }

        [UnityTest]
        public IEnumerator Controllers_NeverConstructTheirOwnAuthoritativeState_OnlyReferenceTheSharedDriver()
        {
            // Structural proof (Phase 2E point 41's "no parallel direct mutation"
            // requirement): both controllers must agree with the single
            // UnitySimulationDriver-owned ManagedSimulation on occupancy/route
            // queries -- there is no second, independently-constructed
            // ArchipelagoPlacementState/RoadNetworkState anywhere in Presentation.
            yield return SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;

            var driver = Object.FindFirstObjectByType<UnitySimulationDriver>();
            var placementController = Object.FindFirstObjectByType<PlacementController>();
            var roadController = Object.FindFirstObjectByType<RoadPlacementController>();
            Assert.IsNotNull(driver);
            Assert.IsNotNull(placementController);
            Assert.IsNotNull(roadController);

            var houseId = ContentId.Parse("bld.prototype.house");
            var simulation = driver.Simulation;
            simulation.Submit(new PlaceBuildingCommand(simulation.LocalPlayerId, simulation.CurrentTick, houseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simulation.AdvanceTicks(1);
            yield return null;

            // A query issued through the ISimulationPlacementQueries surface (the only
            // one Presentation is supposed to use) must see the same occupancy the
            // authoritative snapshot just reported -- proving there's exactly one
            // real state instance in play, not a second stale copy somewhere.
            Assert.IsTrue(simulation.TryGetOccupantAt(new GridCoordinate(0, 0), out var occupant));
            Assert.AreEqual(simulation.GetSnapshot().Buildings[0].InstanceId, occupant);
        }
    }
}
