using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Building
{
    /// <summary>
    /// The first real (non-test) ISimulationCommand implementation. Deliberately
    /// carries only deterministic gameplay data (matches the shape of the existing
    /// test-only TestCommand): no Mouse/Camera/GameObject/Transform/Material/UI --
    /// every field type here already lives in an engine-reference-free assembly.
    /// Immutable, constructed by Presentation, applied against World state via
    /// BuildingPlacementCommandHandler (not wired into ISimulationSystem/
    /// SimulationScheduler yet -- no command-dispatch pipeline exists in this package).
    /// </summary>
    public sealed class PlaceBuildingCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }
        public ContentId BuildingId { get; }
        public GridCoordinate Cell { get; }
        public BuildingRotation Rotation { get; }

        public PlaceBuildingCommand(
            PlayerId issuedBy,
            GameTick issuedAtTick,
            ContentId buildingId,
            GridCoordinate cell,
            BuildingRotation rotation)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
            BuildingId = buildingId;
            Cell = cell;
            Rotation = rotation;
        }
    }
}
