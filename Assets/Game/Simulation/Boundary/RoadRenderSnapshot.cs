using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Everything needed to render/reconstruct one road segment. Presentation builds
    /// a disposable RoadGraph replica from this list (via RoadGraph.AddSegment, which
    /// already derives node connectivity masks itself) rather than ever touching the
    /// live authoritative RoadGraph directly (Phase 2E point 3) -- so no separate
    /// node/topology snapshot type is needed.
    /// </summary>
    public readonly struct RoadRenderSnapshot
    {
        public RoadSegmentId SegmentId { get; }
        public ContentId DefinitionId { get; }
        public IslandId Island { get; }
        public GridCoordinate From { get; }
        public GridCoordinate To { get; }
        public RoadDirection Direction { get; }
        public PlayerId Owner { get; }

        public RoadRenderSnapshot(RoadSegmentId segmentId, ContentId definitionId, IslandId island, GridCoordinate from, GridCoordinate to, RoadDirection direction, PlayerId owner)
        {
            SegmentId = segmentId;
            DefinitionId = definitionId;
            Island = island;
            From = from;
            To = to;
            Direction = direction;
            Owner = owner;
        }
    }
}
