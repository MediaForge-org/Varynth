using System;
using System.Collections.Generic;
using Varynth.Core.Diagnostics;
using Varynth.Core.Simulation.Context;

namespace Varynth.Core.Simulation.Scheduling
{
    /// <summary>
    /// Manages registered simulation systems, establishes a deterministic execution
    /// order, and runs them for a tick. Single-threaded by design (Phase 1C) -- no
    /// Job System/ECS/threads/async. Parallelization is a later decision based on real
    /// profiling data, not assumed here.
    /// </summary>
    public sealed class SimulationScheduler
    {
        private readonly List<ISimulationSystem> _systems = new List<ISimulationSystem>();
        private readonly HashSet<SimulationSystemId> _registeredIds = new HashSet<SimulationSystemId>();
        private readonly IVarynthLogger _logger;

        public SimulationScheduler(IVarynthLogger logger = null)
        {
            _logger = logger ?? NullLogger.Instance;
        }

        public IReadOnlyList<ISimulationSystem> Systems => _systems;

        public void Register(ISimulationSystem system)
        {
            if (system == null)
            {
                throw new ArgumentNullException(nameof(system));
            }

            if (!system.SupportedLevels.IsValid())
            {
                throw new ArgumentException(
                    $"System '{system.Id}' has an invalid SupportedLevels mask: {system.SupportedLevels}.",
                    nameof(system));
            }

            if (!_registeredIds.Add(system.Id))
            {
                throw new DuplicateSimulationSystemException(system.Id);
            }

            _systems.Add(system);
            _systems.Sort(CompareSystems);
        }

        /// <summary>
        /// Runs one tick. A system that throws is logged, then the fault is rethrown
        /// wrapped in SimulationSystemException -- the rest of this call's remaining
        /// systems do NOT run. A faulting system may have already left runtime state
        /// partially mutated; continuing on top of that risks an inconsistent state
        /// (and, later, a host/client divergence in co-op).
        /// </summary>
        public void RunTick(SimulationContext context)
        {
            var requiredMask = context.Level.ToMask();

            foreach (var system in _systems)
            {
                if ((system.SupportedLevels & requiredMask) == 0)
                {
                    continue;
                }

                try
                {
                    system.Tick(context);
                }
                catch (Exception ex)
                {
                    _logger.Error(
                        $"Simulation system '{system.Id}' threw during tick {context.Tick}: {ex.Message}",
                        system.Id.ToString());
                    throw new SimulationSystemException(system.Id, context.Tick, ex);
                }
            }
        }

        private static int CompareSystems(ISimulationSystem a, ISimulationSystem b)
        {
            var orderCompare = a.Order.CompareTo(b.Order);
            if (orderCompare != 0)
            {
                return orderCompare;
            }

            return string.CompareOrdinal(a.Id.ToString(), b.Id.ToString());
        }
    }
}
