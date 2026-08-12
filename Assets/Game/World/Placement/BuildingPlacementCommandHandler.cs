using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;
using Varynth.World.Roads;

namespace Varynth.World.Placement
{
    /// <summary>
    /// The one type that knows about both PlaceBuildingCommand/RemoveBuildingCommand/
    /// PlaceBuildingBatchCommand and ArchipelagoPlacementState, translating one into
    /// calls on the other. ArchipelagoPlacementState's own public API never takes a
    /// command type -- this handler is the sole seam, so the state stays reusable for
    /// AI/replay/host-authoritative co-op/save-restore without any command-boundary
    /// coupling. Optionally carries the same IRoadOccupancyQuery instance the
    /// Presentation-side ghost preview uses, so preview and final command application
    /// always evaluate the identical Building-over-Road rule against the identical
    /// live road state (never a stale/diverged check).
    /// </summary>
    public sealed class BuildingPlacementCommandHandler
    {
        private readonly ArchipelagoPlacementState _state;
        private readonly ContentRegistry<BuildingDefinition> _registry;
        private readonly IRoadOccupancyQuery _roadOccupancy;

        public BuildingPlacementCommandHandler(
            ArchipelagoPlacementState state,
            ContentRegistry<BuildingDefinition> registry,
            IRoadOccupancyQuery roadOccupancy = null)
        {
            _state = state;
            _registry = registry;
            _roadOccupancy = roadOccupancy;
        }

        public bool Handle(PlaceBuildingCommand command, out BuildingInstance instance, out PlacementValidationResult validation)
        {
            return _state.TryPlace(command.BuildingId, command.Cell, command.Rotation, command.IssuedBy, _registry, out instance, out validation, _roadOccupancy);
        }

        public bool Handle(RemoveBuildingCommand command, out BuildingInstance removed)
        {
            return _state.TryRemove(command.TargetInstanceId, out removed);
        }

        /// <summary>
        /// Places every origin in the batch's exact order, reusing the already-proven
        /// single-placement path (ArchipelagoPlacementState.TryPlace) per origin --
        /// not a new World-state mutation primitive. Partial-invalid policy: place
        /// what's valid, skip what isn't (documented decision -- matches classic
        /// city-builder drag UX; an all-or-nothing policy would make drag placement
        /// nearly unusable on real patchy prototype terrain). Deterministic: TryPlace
        /// already assigns sequential BuildingInstanceIds strictly in call order, so
        /// identical Origins order always yields identical instance-id assignment.
        /// </summary>
        public void Handle(
            PlaceBuildingBatchCommand command,
            out IReadOnlyList<BuildingInstance> placed,
            out IReadOnlyList<(GridCoordinate Origin, PlacementValidationResult Validation)> rejected)
        {
            var placedList = new List<BuildingInstance>(command.Origins.Count);
            var rejectedList = new List<(GridCoordinate, PlacementValidationResult)>();

            foreach (var origin in command.Origins)
            {
                if (_state.TryPlace(command.BuildingId, origin, command.Rotation, command.IssuedBy, _registry, out var instance, out var validation, _roadOccupancy))
                {
                    placedList.Add(instance);
                }
                else
                {
                    rejectedList.Add((origin, validation));
                }
            }

            placed = placedList;
            rejected = rejectedList;
        }
    }
}
