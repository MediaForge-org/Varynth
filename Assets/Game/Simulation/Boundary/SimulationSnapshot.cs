using System.Collections.Generic;
using Varynth.Core.Simulation.Clock;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Bundled, renderable simulation state as of one tick (Phase 2E point 12/15).
    /// Tick advances every tick unconditionally; BuildingStateVersion/RoadStateVersion
    /// only increment when that domain's data actually changed (point 5) -- Presentation
    /// gates its diff/rebuild work on the *Version fields, not on Tick, so an unchanged
    /// tick costs nothing beyond the snapshot reference check itself.
    ///
    /// Once handed out, a SimulationSnapshot instance never mutates (point 4):
    /// ManagedSimulation double-buffers its backing Lists and only ever writes into
    /// whichever buffer is NOT referenced by the most recently returned snapshot.
    /// </summary>
    public sealed class SimulationSnapshot
    {
        public GameTick Tick { get; }
        public int BuildingStateVersion { get; }
        public int RoadStateVersion { get; }
        public IReadOnlyList<BuildingRenderSnapshot> Buildings { get; }
        public IReadOnlyList<RoadRenderSnapshot> Roads { get; }

        public SimulationSnapshot(
            GameTick tick,
            int buildingStateVersion,
            int roadStateVersion,
            IReadOnlyList<BuildingRenderSnapshot> buildings,
            IReadOnlyList<RoadRenderSnapshot> roads)
        {
            Tick = tick;
            BuildingStateVersion = buildingStateVersion;
            RoadStateVersion = roadStateVersion;
            Buildings = buildings;
            Roads = roads;
        }
    }
}
