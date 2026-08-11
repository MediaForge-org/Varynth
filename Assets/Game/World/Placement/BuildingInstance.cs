using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Plain gameplay-state data for one placed building -- no GameObject/Transform
    /// reference. World state vs Presentation stays strictly separate: Presentation
    /// owns whatever GameObject visualizes this instance, but destroying that
    /// GameObject never touches this data; only ArchipelagoPlacementState.TryRemove
    /// does.
    /// </summary>
    public sealed class BuildingInstance
    {
        public BuildingInstanceId Id { get; }
        public ContentId DefinitionId { get; }
        public GridCoordinate Origin { get; }
        public BuildingRotation Rotation { get; }
        public PlayerId Owner { get; }

        /// <summary>
        /// The footprint cells actually occupied at placement time (already reflects
        /// the 90/270-degree width/length swap) -- stored so removal can release
        /// occupancy without needing to re-resolve the definition from a registry.
        /// </summary>
        public System.Collections.Generic.IReadOnlyList<GridCoordinate> OccupiedCells { get; }

        public BuildingInstance(
            BuildingInstanceId id,
            ContentId definitionId,
            GridCoordinate origin,
            BuildingRotation rotation,
            PlayerId owner,
            System.Collections.Generic.IReadOnlyList<GridCoordinate> occupiedCells)
        {
            Id = id;
            DefinitionId = definitionId;
            Origin = origin;
            Rotation = rotation;
            Owner = owner;
            OccupiedCells = occupiedCells;
        }
    }
}
