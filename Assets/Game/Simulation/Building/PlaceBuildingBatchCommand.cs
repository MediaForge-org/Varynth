using System.Collections.Generic;
using System.Linq;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Building
{
    /// <summary>
    /// One command carrying a whole drag-repeat batch of placements -- never 50
    /// separate PlaceBuildingCommand Handle() calls. Origins are explicit and
    /// pre-ordered (not a Start/End pair): the repeat shape (line vs. rectangle,
    /// spacing, order) is already fully resolved by BuildingRepeatPlanner in
    /// Presentation before this command is constructed, so the command itself stays
    /// dumb, replayable data. Deterministic ID-assignment order downstream depends on
    /// Origins being iterated in exactly this order.
    /// </summary>
    public sealed class PlaceBuildingBatchCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }
        public ContentId BuildingId { get; }
        public BuildingRotation Rotation { get; }

        /// <summary>
        /// Defensively copied at construction -- mutating the caller's original list
        /// afterward can never retroactively change this command's data.
        /// </summary>
        public IReadOnlyList<GridCoordinate> Origins { get; }

        public PlaceBuildingBatchCommand(
            PlayerId issuedBy,
            GameTick issuedAtTick,
            ContentId buildingId,
            BuildingRotation rotation,
            IReadOnlyList<GridCoordinate> origins)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
            BuildingId = buildingId;
            Rotation = rotation;
            Origins = (origins ?? System.Array.Empty<GridCoordinate>()).ToArray();
        }
    }
}
