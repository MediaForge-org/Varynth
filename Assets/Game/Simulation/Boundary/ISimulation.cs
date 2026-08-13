using System.Collections.Generic;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// The engine-free authoritative-simulation contract (Phase 2E). Contains no
    /// UnityEngine type anywhere -- enforced structurally by living in
    /// Varynth.Core.Simulation (noEngineReferences: true), not just by convention.
    /// Deliberately minimal: no Initialize/Reset/Dispose. Construction of a concrete
    /// implementation (ManagedSimulation, later possibly NativeSimulationBridge) IS
    /// initialization; no scenario-restart requirement exists yet (tests just
    /// construct a fresh instance); nothing here holds an unmanaged/disposable
    /// resource.
    /// </summary>
    public interface ISimulation
    {
        GameTick CurrentTick { get; }

        /// <summary>The local player's identity, for constructing commands' IssuedBy field. Generated once at session-init time by the caller, never inside a tick.</summary>
        PlayerId LocalPlayerId { get; }

        /// <summary>
        /// Queues a command; does not apply it synchronously. Internally scheduled
        /// for the next tick by default (Phase 2E point 6) -- see CommandEnvelope.
        /// </summary>
        SimulationCommandTicket Submit(ISimulationCommand command);

        /// <summary>
        /// Runs tickCount discrete ticks. Each tick applies only the commands
        /// targeting it (in submit order), then runs registered simulation systems,
        /// then advances the clock -- never "apply everything once, then run
        /// tickCount empty ticks" (Phase 2E point 6).
        /// </summary>
        void AdvanceTicks(int tickCount);

        /// <summary>Latest available snapshot. Never mutates after being returned (Phase 2E point 4).</summary>
        SimulationSnapshot GetSnapshot();

        /// <summary>Generic acknowledgement for any command type (drains and clears the internal buffer).</summary>
        IReadOnlyList<SimulationCommandResult> ConsumeResults();

        /// <summary>Rich, structured building-command detail (drains and clears the internal buffer).</summary>
        IReadOnlyList<BuildingCommandResult> ConsumeBuildingResults();

        /// <summary>Rich, structured road-command detail (drains and clears the internal buffer).</summary>
        IReadOnlyList<RoadCommandResult> ConsumeRoadResults();
    }
}
