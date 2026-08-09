using Varynth.Core.Simulation.Clock;

namespace Varynth.Core.Simulation.Context
{
    /// <summary>
    /// Small, immutable per-tick context handed to a simulation system. Deliberately
    /// kept to exactly these two fields -- no region/world reference yet, no service
    /// locator. A future region-aware context can be added additively without
    /// replacing this one, same pattern as Phase 1B's additive extension of Phase 1A.
    /// </summary>
    public readonly struct SimulationContext
    {
        public GameTick Tick { get; }

        public SimulationLevel Level { get; }

        public SimulationContext(GameTick tick, SimulationLevel level)
        {
            Tick = tick;
            Level = level;
        }
    }
}
