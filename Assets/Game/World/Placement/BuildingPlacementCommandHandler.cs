using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;

namespace Varynth.World.Placement
{
    /// <summary>
    /// The one type that knows about both PlaceBuildingCommand/RemoveBuildingCommand
    /// and ArchipelagoPlacementState, translating one into calls on the other.
    /// ArchipelagoPlacementState's own public API never takes a command type -- this
    /// handler is the sole seam, so the state stays reusable for AI/replay/
    /// host-authoritative co-op/save-restore without any command-boundary coupling.
    /// </summary>
    public sealed class BuildingPlacementCommandHandler
    {
        private readonly ArchipelagoPlacementState _state;
        private readonly ContentRegistry<BuildingDefinition> _registry;

        public BuildingPlacementCommandHandler(ArchipelagoPlacementState state, ContentRegistry<BuildingDefinition> registry)
        {
            _state = state;
            _registry = registry;
        }

        public bool Handle(PlaceBuildingCommand command, out BuildingInstance instance, out PlacementValidationResult validation)
        {
            return _state.TryPlace(command.BuildingId, command.Cell, command.Rotation, command.IssuedBy, _registry, out instance, out validation);
        }

        public bool Handle(RemoveBuildingCommand command, out BuildingInstance removed)
        {
            return _state.TryRemove(command.TargetInstanceId, out removed);
        }
    }
}
