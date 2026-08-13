using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.World.Roads;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>Rich, structured road-command result -- mirrors BuildingCommandResult's reasoning.</summary>
    public readonly struct RoadCommandResult
    {
        public SimulationCommandTicket Ticket { get; }
        public GameTick ProcessedAtTick { get; }
        public SimulationCommandOutcome Outcome { get; }
        public RoadPlacementValidationResult Validation { get; }
        public IReadOnlyList<RoadSegmentId> CreatedSegmentIds { get; }
        public RoadSegmentId RemovedSegmentId { get; }

        public RoadCommandResult(
            SimulationCommandTicket ticket,
            GameTick processedAtTick,
            SimulationCommandOutcome outcome,
            RoadPlacementValidationResult validation,
            IReadOnlyList<RoadSegmentId> createdSegmentIds,
            RoadSegmentId removedSegmentId)
        {
            Ticket = ticket;
            ProcessedAtTick = processedAtTick;
            Outcome = outcome;
            Validation = validation;
            CreatedSegmentIds = createdSegmentIds;
            RemovedSegmentId = removedSegmentId;
        }
    }
}
