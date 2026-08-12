using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Plain gameplay-state data for one road segment -- no GameObject/Transform
    /// reference, mirrors BuildingInstance. No stored CostUnits: cost is always
    /// derived on demand via Direction.CostUnits() (RoadDirection.cs), so there is no
    /// redundant/mutable copy that could ever drift from Direction. Owner is stored
    /// additively (mirrors BuildingInstance.Owner) for future host-authoritative
    /// co-op -- costs nothing today since it already flows through
    /// BuildRoadCommand.IssuedBy, and would be expensive to retrofit later.
    /// </summary>
    public sealed class RoadSegment
    {
        public RoadSegmentId Id { get; }
        public ContentId DefinitionId { get; }
        public GridCoordinate From { get; }
        public GridCoordinate To { get; }
        public RoadDirection Direction { get; }
        public PlayerId Owner { get; }

        public RoadSegment(RoadSegmentId id, ContentId definitionId, GridCoordinate from, GridCoordinate to, RoadDirection direction, PlayerId owner)
        {
            Id = id;
            DefinitionId = definitionId;
            From = from;
            To = to;
            Direction = direction;
            Owner = owner;
        }
    }
}
