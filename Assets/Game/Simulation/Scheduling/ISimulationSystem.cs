using Varynth.Core.Simulation.Context;

namespace Varynth.Core.Simulation.Scheduling
{
    /// <summary>
    /// A registrable, orderable, tickable simulation system. No assumption anywhere
    /// that an implementation is a MonoBehaviour. Concrete systems (production,
    /// population, logistics, trade, ...) are NOT implemented in Phase 1C -- this is
    /// only the infrastructure they will later plug into.
    /// </summary>
    public interface ISimulationSystem
    {
        SimulationSystemId Id { get; }

        /// <summary>
        /// Explicit sort key for deterministic scheduling. Ties are broken by Id
        /// (ordinal string compare) -- never by registration order.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Which SimulationLevels this system participates in. Implementers should
        /// return a cached/constant value, not allocate on each access.
        /// </summary>
        SimulationLevelMask SupportedLevels { get; }

        void Tick(SimulationContext context);
    }
}
