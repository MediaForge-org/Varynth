using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Road;
using Varynth.World.Placement;

namespace Varynth.World.Roads
{
    /// <summary>
    /// The one type that knows about both BuildRoadCommand/RemoveRoadCommand and
    /// RoadNetworkState, translating one into calls on the other -- mirrors
    /// BuildingPlacementCommandHandler exactly. Reuses the existing
    /// Varynth.World -&gt; Varynth.Core.Simulation asmdef edge (present since Phase 2C),
    /// no new edge needed.
    /// </summary>
    public sealed class RoadCommandHandler
    {
        private readonly RoadNetworkState _state;
        private readonly ContentRegistry<RoadDefinition> _registry;
        private readonly IBuildingOccupancyQuery _buildingOccupancy;

        public RoadCommandHandler(RoadNetworkState state, ContentRegistry<RoadDefinition> registry, IBuildingOccupancyQuery buildingOccupancy = null)
        {
            _state = state;
            _registry = registry;
            _buildingOccupancy = buildingOccupancy;
        }

        public bool Handle(BuildRoadCommand command, out System.Collections.Generic.IReadOnlyList<RoadSegment> created, out RoadPlacementValidationResult validation)
        {
            return _state.TryBuildPath(command.RoadDefinitionId, command.OrderedPath, command.IssuedBy, _registry, _buildingOccupancy, out created, out validation);
        }

        public bool Handle(RemoveRoadCommand command, out RoadSegment removed)
        {
            return _state.TryRemoveSegment(command.TargetSegment, out removed);
        }
    }
}
