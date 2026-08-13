using Varynth.Core.Simulation.Clock;

namespace Varynth.Core.Simulation.Boundary
{
    public enum SimulationCommandOutcome
    {
        Accepted,
        Rejected
    }

    /// <summary>
    /// Coarse, structured, engine-free rejection reason (Phase 2E point 28: never a
    /// string). Domain-specific detail (which PlacementIssue/RoadPlacementIssue
    /// flags) is carried by BuildingCommandResult/RoadCommandResult instead -- both
    /// of which are themselves engine-free Core.Simulation types after the Phase 2E
    /// move, so exposing them directly on ISimulation does not leak a
    /// ManagedSimulation-specific or engine-specific type (point 10).
    /// </summary>
    public enum SimulationCommandRejectionReason
    {
        None = 0,
        ValidationFailed,
        UnknownCommandType,
        TargetNotFound
    }

    /// <summary>Generic acknowledgement for any command type, including hypothetical future ones not covered by the two rich typed result lists.</summary>
    public readonly struct SimulationCommandResult
    {
        public SimulationCommandTicket Ticket { get; }
        public GameTick ProcessedAtTick { get; }
        public SimulationCommandOutcome Outcome { get; }
        public SimulationCommandRejectionReason Reason { get; }

        public SimulationCommandResult(SimulationCommandTicket ticket, GameTick processedAtTick, SimulationCommandOutcome outcome, SimulationCommandRejectionReason reason)
        {
            Ticket = ticket;
            ProcessedAtTick = processedAtTick;
            Outcome = outcome;
            Reason = reason;
        }
    }
}
