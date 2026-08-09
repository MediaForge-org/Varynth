using System;
using Varynth.Core.Simulation.Clock;

namespace Varynth.Core.Simulation.Scheduling
{
    /// <summary>
    /// Wraps an exception thrown by a simulation system's Tick(). A faulting system
    /// may have already left runtime state partially mutated, so SimulationScheduler
    /// logs this, then rethrows it wrapped here to abort the rest of the current tick
    /// immediately -- systems ordered after the faulting one do not run that tick.
    /// </summary>
    public sealed class SimulationSystemException : Exception
    {
        public SimulationSystemId SystemId { get; }

        public GameTick Tick { get; }

        public SimulationSystemException(SimulationSystemId systemId, GameTick tick, Exception innerException)
            : base($"Simulation system '{systemId}' threw during tick {tick}.", innerException)
        {
            SystemId = systemId;
            Tick = tick;
        }
    }
}
