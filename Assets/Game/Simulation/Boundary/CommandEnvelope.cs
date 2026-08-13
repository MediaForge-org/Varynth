using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Internal per-command scheduling wrapper (Phase 2E point 6). Submit() assigns
    /// TargetTick = CurrentTick.Add(1) by default -- a command queued between ticks
    /// takes effect at the NEXT tick, never "whenever the batch of AdvanceTicks(n)
    /// happens to get around to it". AdvanceTicks(n) applies, on each of its n
    /// iterations, only the envelopes whose TargetTick equals the tick about to run,
    /// in ascending SubmitSequence order -- never Dictionary iteration, never Unity
    /// Update ordering. This also directly sets up future lockstep-style scheduling
    /// (a remote command could target a few ticks ahead for latency buffering).
    /// </summary>
    internal readonly struct CommandEnvelope
    {
        public readonly GameTick TargetTick;
        public readonly ulong SubmitSequence;
        public readonly SimulationCommandTicket Ticket;
        public readonly ISimulationCommand Command;

        public CommandEnvelope(GameTick targetTick, ulong submitSequence, SimulationCommandTicket ticket, ISimulationCommand command)
        {
            TargetTick = targetTick;
            SubmitSequence = submitSequence;
            Ticket = ticket;
            Command = command;
        }
    }
}
