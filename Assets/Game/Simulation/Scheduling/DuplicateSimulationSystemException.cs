using System;

namespace Varynth.Core.Simulation.Scheduling
{
    /// <summary>
    /// Thrown by SimulationScheduler.Register when the SimulationSystemId is already
    /// registered. Registration never silently overwrites an existing entry.
    /// </summary>
    public sealed class DuplicateSimulationSystemException : InvalidOperationException
    {
        public SimulationSystemId Id { get; }

        public DuplicateSimulationSystemException(SimulationSystemId id)
            : base($"A simulation system with id '{id}' is already registered.")
        {
            Id = id;
        }
    }
}
