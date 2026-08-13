using System.Diagnostics;
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
    // Scale/GC sanity for the Simulation Boundary itself (Phase 2E prompt sections
    // 39/40) -- not a real economy test. Proves the boundary doesn't structurally
    // force thousands of per-object bridge calls or obvious hotpath allocation
    // growth. Real elapsed times documented honestly, no invented FPS numbers.
    public class ManagedSimulationScaleTests
    {
        private static readonly ContentId HouseId = ContentId.Parse("bld.prototype.house");
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        private static ManagedSimulation BuildLargeSimulation(int width, int height)
        {
            var flags = new SurfaceCellFlags[width * height];
            for (var i = 0; i < flags.Length; i++) flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            var cellHeights = new float[width * height];
            var islandData = new SimulationIslandData(IslandId.FromName("ScaleTestIsland"), "ScaleTestIsland", 0, 0, width, height, flags, cellHeights);
            var worldData = new SimulationWorldData(4f, new[] { islandData });

            var buildingRegistry = new ContentRegistry<BuildingDefinition>();
            buildingRegistry.Register(new BuildingDefinition(HouseId, LocalizationKey.Parse("bld.house.name"), 2, 2, "house"));
            var roadRegistry = new ContentRegistry<RoadDefinition>();
            roadRegistry.Register(new RoadDefinition(RoadId, LocalizationKey.Parse("road.name"), "road"));

            return new ManagedSimulation(worldData, buildingRegistry, roadRegistry, PlayerId.NewId());
        }

        [Test]
        public void TenThousandBuildings_PlusSeveralThousandRoadSegments_SnapshotGenerationAndTicksStayReasonable()
        {
            var sim = BuildLargeSimulation(width: 400, height: 400);

            // ~10,000 non-overlapping 2x2 houses on a 400x400 grid (spaced every 4 cells -> 100x100 = 10,000).
            var placeStopwatch = Stopwatch.StartNew();
            for (var cz = 0; cz < 400 && cz < 400; cz += 4)
            {
                for (var cx = 0; cx < 400; cx += 4)
                {
                    sim.Submit(new PlaceBuildingCommand(sim.LocalPlayerId, sim.CurrentTick, HouseId, new GridCoordinate(cx, cz), BuildingRotation.Deg0));
                }
            }
            sim.AdvanceTicks(1);
            placeStopwatch.Stop();

            var placedCount = sim.GetSnapshot().Buildings.Count;
            UnityEngine.Debug.Log($"[PERF] ManagedSimulation: placed {placedCount} buildings in {placeStopwatch.ElapsedMilliseconds} ms (one batched tick).");
            Assert.GreaterOrEqual(placedCount, 9000);

            // A few thousand road segments: 20 long rows of ~200 segments each. The
            // synthetic house grid above places a 2x2 house every 4 cells, so it only
            // ever occupies Z rows where (z mod 4) is 0 or 1 -- Z rows at (z mod 4)==2
            // are never touched by any house's footprint, at any X, so picking only
            // those Z values guarantees zero collision with the 10,000 houses above.
            var roadStopwatch = Stopwatch.StartNew();
            for (var row = 0; row < 20; row++)
            {
                var z = row * 4 + 2;
                var path = new GridCoordinate[200];
                for (var x = 0; x < 200; x++) path[x] = new GridCoordinate(x, z);
                sim.Submit(new BuildRoadCommand(sim.LocalPlayerId, sim.CurrentTick, RoadId, path));
            }
            sim.AdvanceTicks(1);
            roadStopwatch.Stop();

            var roadCount = sim.GetSnapshot().Roads.Count;
            UnityEngine.Debug.Log($"[PERF] ManagedSimulation: built {roadCount} road segments in {roadStopwatch.ElapsedMilliseconds} ms (one batched tick).");
            Assert.GreaterOrEqual(roadCount, 3000);

            // Repeated snapshot access (no new state change) must stay cheap -- same
            // reference, no rebuild.
            var snapshotStopwatch = Stopwatch.StartNew();
            var firstSnapshot = sim.GetSnapshot();
            for (var i = 0; i < 1000; i++)
            {
                sim.AdvanceTicks(1);
            }
            var lastSnapshot = sim.GetSnapshot();
            snapshotStopwatch.Stop();
            UnityEngine.Debug.Log($"[PERF] ManagedSimulation: 1000 empty ticks after {placedCount} buildings / {roadCount} segments in {snapshotStopwatch.ElapsedMilliseconds} ms.");

            Assert.AreSame(firstSnapshot.Buildings, lastSnapshot.Buildings, "1000 ticks with zero placement/removal must not rebuild the buildings buffer.");
            Assert.AreSame(firstSnapshot.Roads, lastSnapshot.Roads, "1000 ticks with zero placement/removal must not rebuild the roads buffer.");
            Assert.Less(snapshotStopwatch.ElapsedMilliseconds, 2000, "1000 empty ticks over a large populated world should stay fast -- the boundary must not force per-object work on every tick regardless of state change.");
        }

        [Test]
        public void RepeatedEmptyTicks_DoNotObviouslyGrowAllocationPerCall()
        {
            var sim = BuildLargeSimulation(width: 50, height: 50);
            sim.AdvanceTicks(1); // warm up (JIT, first snapshot allocation)

            var before = System.GC.GetAllocatedBytesForCurrentThread();
            for (var i = 0; i < 5000; i++)
            {
                sim.AdvanceTicks(1);
            }
            var after = System.GC.GetAllocatedBytesForCurrentThread();

            var totalAllocated = after - before;
            var perTick = totalAllocated / 5000.0;
            UnityEngine.Debug.Log($"[PERF] ManagedSimulation: {totalAllocated} bytes allocated across 5000 empty ticks ({perTick:F1} bytes/tick).");

            // Not zero-GC dogma (Phase 2E point 40 explicitly disclaims that) -- just a
            // sanity bound against an obvious "allocates a big object graph every
            // single tick regardless of state change" hotpath bug.
            Assert.Less(perTick, 512.0, "Empty ticks should not allocate a large object graph per call.");
        }
    }
}
