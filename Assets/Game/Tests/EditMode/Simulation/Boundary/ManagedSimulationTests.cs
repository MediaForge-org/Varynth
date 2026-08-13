using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Boundary;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Common;
using Varynth.Core.Simulation.Road;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.Simulation.Boundary
{
    // Headless ManagedSimulation tests (Phase 2E, prompt section 38): constructed
    // entirely from plain SimulationWorldData -- no Unity Scene, Camera, Renderer,
    // GameObject, MonoBehaviour, or Terrain anywhere. Mirrors
    // ArchipelagoPlacementStateTests/RoadCommandHandlerTests' island-construction
    // helpers rather than inventing a third pattern.
    public class ManagedSimulationTests
    {
        private static readonly ContentId HouseId = ContentId.Parse("bld.prototype.house");
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        private static SimulationIslandData BuildIslandData(string name, int originX, int originZ, int width, int height)
        {
            var flags = new SurfaceCellFlags[width * height];
            for (var i = 0; i < flags.Length; i++) flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            var cellHeights = new float[width * height];
            return new SimulationIslandData(IslandId.FromName(name), name, originX, originZ, width, height, flags, cellHeights);
        }

        private static ManagedSimulation BuildSimulation(int islandCount = 1, int width = 20, int height = 20)
        {
            var islands = new List<SimulationIslandData>();
            for (var i = 0; i < islandCount; i++)
            {
                islands.Add(BuildIslandData($"Island{i}", i * 200, 0, width, height));
            }
            var worldData = new SimulationWorldData(4f, islands);

            var buildingRegistry = new ContentRegistry<BuildingDefinition>();
            buildingRegistry.Register(new BuildingDefinition(HouseId, LocalizationKey.Parse("bld.house.name"), 2, 2, "house"));

            var roadRegistry = new ContentRegistry<RoadDefinition>();
            roadRegistry.Register(new RoadDefinition(RoadId, LocalizationKey.Parse("road.name"), "road"));

            return new ManagedSimulation(worldData, buildingRegistry, roadRegistry, PlayerId.NewId());
        }

        [Test]
        public void Constructible_WithoutAnyUnityScene()
        {
            var sim = BuildSimulation();
            Assert.IsNotNull(sim);
            Assert.AreEqual(0UL, sim.CurrentTick.Value);
        }

        [Test]
        public void EmptyTicks_1000_StableNoErrors()
        {
            var sim = BuildSimulation();
            sim.AdvanceTicks(1000);
            Assert.AreEqual(1000UL, sim.CurrentTick.Value);
            Assert.AreEqual(0, sim.GetSnapshot().Buildings.Count);
            Assert.AreEqual(0, sim.GetSnapshot().Roads.Count);
        }

        [Test]
        public void PlaceBuildingCommand_AfterTick_AppearsInSnapshot()
        {
            var sim = BuildSimulation();
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(2, 2), BuildingRotation.Deg0));
            sim.AdvanceTicks(1);

            var snapshot = sim.GetSnapshot();
            Assert.AreEqual(1, snapshot.Buildings.Count);
            Assert.AreEqual(HouseId, snapshot.Buildings[0].DefinitionId);
            Assert.AreEqual(new GridCoordinate(2, 2), snapshot.Buildings[0].Origin);
        }

        [Test]
        public void PlaceBuildingBatchCommand_AfterTick_AllExpectedBuildingsPresent()
        {
            var sim = BuildSimulation();
            var origins = new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0), new GridCoordinate(4, 0) };
            sim.Submit(new PlaceBuildingBatchCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, BuildingRotation.Deg0, origins));
            sim.AdvanceTicks(1);

            Assert.AreEqual(3, sim.GetSnapshot().Buildings.Count);
        }

        [Test]
        public void RemoveBuildingCommand_AfterTick_SnapshotUpdated()
        {
            var sim = BuildSimulation();
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(2, 2), BuildingRotation.Deg0));
            sim.AdvanceTicks(1);
            var placed = sim.GetSnapshot().Buildings[0];

            sim.Submit(new RemoveBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, placed.InstanceId));
            sim.AdvanceTicks(1);

            Assert.AreEqual(0, sim.GetSnapshot().Buildings.Count);
        }

        [Test]
        public void BuildRoadCommand_AfterTick_SegmentsAppearInSnapshot()
        {
            var sim = BuildSimulation();
            sim.Submit(new BuildRoadCommand(sim.LocalPlayerId, sim.CurrentTick, RoadId,
                new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0), new GridCoordinate(2, 0) }));
            sim.AdvanceTicks(1);

            Assert.AreEqual(2, sim.GetSnapshot().Roads.Count);
        }

        [Test]
        public void RemoveRoadCommand_AfterTick_SnapshotUpdated()
        {
            var sim = BuildSimulation();
            sim.Submit(new BuildRoadCommand(sim.LocalPlayerId, sim.CurrentTick, RoadId,
                new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }));
            sim.AdvanceTicks(1);
            var segmentId = sim.GetSnapshot().Roads[0].SegmentId;

            sim.Submit(new RemoveRoadCommand(sim.LocalPlayerId, sim.CurrentTick, segmentId));
            sim.AdvanceTicks(1);

            Assert.AreEqual(0, sim.GetSnapshot().Roads.Count);
        }

        [Test]
        public void ConsumeBuildingResults_ReportsAcceptedAndRejectedWithIssues()
        {
            var sim = BuildSimulation();
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(2, 2), BuildingRotation.Deg0));
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(2, 2), BuildingRotation.Deg0));
            sim.AdvanceTicks(1);

            var results = sim.ConsumeBuildingResults();
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(SimulationCommandOutcome.Accepted, results[0].Outcome);
            Assert.AreEqual(SimulationCommandOutcome.Rejected, results[1].Outcome);
            Assert.IsTrue((results[1].Validation.Issues & PlacementIssue.AlreadyOccupied) != 0);

            // Draining clears the buffer.
            Assert.AreEqual(0, sim.ConsumeBuildingResults().Count);
        }

        [Test]
        public void Submit_CommandsBetweenTicks_AppliedOnlyOnTheirTargetTick_NotAllAtOnce()
        {
            // Phase 2E point 6: AdvanceTicks(n) must not apply everything queued
            // before running n ticks -- a command submitted mid-sequence must land on
            // the tick immediately following its own Submit() call, not earlier.
            var sim = BuildSimulation();

            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            sim.AdvanceTicks(1);
            Assert.AreEqual(1, sim.GetSnapshot().Buildings.Count, "First command should have landed after its own tick.");

            // No command queued for this tick -- must stay unchanged.
            sim.AdvanceTicks(1);
            Assert.AreEqual(1, sim.GetSnapshot().Buildings.Count, "An empty tick must not spontaneously apply anything.");

            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(4, 0), BuildingRotation.Deg0));
            // Not yet advanced -- must not be visible yet.
            Assert.AreEqual(1, sim.GetSnapshot().Buildings.Count, "A queued-but-not-yet-ticked command must not be visible in the snapshot.");

            sim.AdvanceTicks(1);
            Assert.AreEqual(2, sim.GetSnapshot().Buildings.Count, "Second command should land only once its own target tick runs.");
        }

        [Test]
        public void Determinism_SameCommandsSameOrder_ProducesSameStateDigest()
        {
            ulong RunAndDigest()
            {
                var sim = BuildSimulation();
                sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
                sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(4, 0), BuildingRotation.Deg0));
                sim.AdvanceTicks(1);
                sim.Submit(new BuildRoadCommand(sim.LocalPlayerId, sim.CurrentTick, RoadId, new[] { new GridCoordinate(0, 4), new GridCoordinate(1, 4) }));
                sim.AdvanceTicks(5);
                return sim.ComputeStateDigest();
            }

            var digestA = RunAndDigest();
            var digestB = RunAndDigest();

            Assert.AreEqual(digestA, digestB, "Identical initial state + identical ordered commands + identical tick count must produce an identical state digest.");
        }

        [Test]
        public void StateDigest_DiffersWhenIdSequenceDiffersEvenIfVisuallyIdentical()
        {
            // Sim A: place then remove then re-place -- consumes an extra instance id.
            var simA = BuildSimulation();
            simA.Submit(new PlaceBuildingCommand(simA.LocalPlayerId, simA.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simA.AdvanceTicks(1);
            var firstId = simA.GetSnapshot().Buildings[0].InstanceId;
            simA.Submit(new RemoveBuildingCommand(simA.LocalPlayerId, simA.CurrentTick, firstId));
            simA.AdvanceTicks(1);
            simA.Submit(new PlaceBuildingCommand(simA.LocalPlayerId, simA.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simA.AdvanceTicks(1);

            // Sim B: places the same single building directly, same visual end state.
            var simB = BuildSimulation();
            simB.Submit(new PlaceBuildingCommand(simB.LocalPlayerId, simB.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            simB.AdvanceTicks(1);

            Assert.AreEqual(simA.GetSnapshot().Buildings.Count, simB.GetSnapshot().Buildings.Count, "Sanity: both end up with exactly one visible building.");
            Assert.AreNotEqual(simA.ComputeStateDigest(), simB.ComputeStateDigest(),
                "Sim A consumed an extra BuildingInstanceId via its place/remove/re-place sequence -- the digest must reflect that even though the visible snapshot looks identical.");
        }

        [Test]
        public void Snapshot_HandedOutReference_NeverMutatesAfterLaterTicks()
        {
            var sim = BuildSimulation();
            sim.AdvanceTicks(1);
            var earlySnapshot = sim.GetSnapshot();
            var earlyTick = earlySnapshot.Tick;
            var earlyBuildingsRef = earlySnapshot.Buildings;

            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(0, 0), BuildingRotation.Deg0));
            sim.AdvanceTicks(10);

            Assert.AreEqual(earlyTick, earlySnapshot.Tick, "A previously-returned snapshot's own Tick must never change.");
            Assert.AreEqual(0, earlySnapshot.Buildings.Count, "A previously-returned snapshot's Buildings list must never later contain a building added afterward.");
            Assert.AreSame(earlyBuildingsRef, earlySnapshot.Buildings, "The list reference itself must not be swapped out from under an already-returned snapshot.");

            var laterSnapshot = sim.GetSnapshot();
            Assert.AreEqual(1, laterSnapshot.Buildings.Count, "The latest snapshot must reflect the new building.");
            Assert.AreNotEqual(earlyTick.Value, laterSnapshot.Tick.Value);
        }

        [Test]
        public void Snapshot_TickAlwaysAdvances_EvenWhenNoStateChanged()
        {
            var sim = BuildSimulation();
            sim.AdvanceTicks(3);
            var s1 = sim.GetSnapshot();
            Assert.AreEqual(3UL, s1.Tick.Value);
            Assert.AreEqual(0, s1.BuildingStateVersion);

            sim.AdvanceTicks(2); // still nothing placed
            var s2 = sim.GetSnapshot();
            Assert.AreEqual(5UL, s2.Tick.Value, "Tick must advance every AdvanceTicks call regardless of whether state changed.");
            Assert.AreEqual(s1.BuildingStateVersion, s2.BuildingStateVersion, "StateVersion must stay unchanged when nothing was placed/removed.");
        }

        [Test]
        public void MultiIsland_PlacementsAreIndependent()
        {
            var sim = BuildSimulation(islandCount: 2);
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(2, 2), BuildingRotation.Deg0));
            sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(202, 2), BuildingRotation.Deg0));
            sim.AdvanceTicks(1);

            var snapshot = sim.GetSnapshot();
            Assert.AreEqual(2, snapshot.Buildings.Count);
            Assert.AreNotEqual(snapshot.Buildings[0].Island, snapshot.Buildings[1].Island, "Buildings on different islands must report different IslandId values.");
        }
    }
}
