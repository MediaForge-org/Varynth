using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.World.Placement;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Rich, structured building-command result (Phase 2E point 10) -- carries the
    /// real PlacementValidationResult/PlacementIssue detail directly. Safe to expose
    /// on ISimulation itself (not just ManagedSimulation): PlacementValidationResult
    /// is, after the Phase 2E move, itself an engine-free Core.Simulation type, so a
    /// future NativeSimulationBridge implementing ISimulation can produce the same
    /// real contract, not a Managed-only escape hatch.
    /// </summary>
    public readonly struct BuildingCommandResult
    {
        public SimulationCommandTicket Ticket { get; }
        public GameTick ProcessedAtTick { get; }
        public SimulationCommandOutcome Outcome { get; }
        public PlacementValidationResult Validation { get; }
        public BuildingInstanceId CreatedInstanceId { get; }

        public BuildingCommandResult(
            SimulationCommandTicket ticket,
            GameTick processedAtTick,
            SimulationCommandOutcome outcome,
            PlacementValidationResult validation,
            BuildingInstanceId createdInstanceId)
        {
            Ticket = ticket;
            ProcessedAtTick = processedAtTick;
            Outcome = outcome;
            Validation = validation;
            CreatedInstanceId = createdInstanceId;
        }
    }
}
