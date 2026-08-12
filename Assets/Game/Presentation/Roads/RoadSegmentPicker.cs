using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Roads;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// Disambiguates which road segment at a busy node (a hovered GridCoordinate
    /// alone is ambiguous once a node has more than one connection) the cursor is
    /// closest to, using the real continuous world hit position. Purely a
    /// Presentation-side picking rule -- the world position is used transiently each
    /// frame for disambiguation, never stored as world/gameplay state.
    /// </summary>
    public static class RoadSegmentPicker
    {
        public static bool TryFindNearestIncidentSegment(GridCoordinate hoveredCell, Vector3 worldHitPosition, RoadGraph graph, WorldGrid grid, out RoadSegmentId nearest)
        {
            nearest = RoadSegmentId.None;

            if (!graph.TryGetNode(hoveredCell, out var node))
            {
                return false;
            }

            var hit2D = new Vector2(worldHitPosition.x, worldHitPosition.z);
            var nodeCenter = grid.CellToWorldCenter(hoveredCell);

            var found = false;
            var bestDistance = float.MaxValue;
            var bestId = default(RoadSegmentId);

            for (var d = 0; d < 8; d++)
            {
                var direction = (RoadDirection)d;
                if ((node.ConnectedDirectionsMask & (1 << d)) == 0)
                {
                    continue;
                }

                var (dx, dz) = direction.ToDelta();
                var neighbor = new GridCoordinate(hoveredCell.X + dx, hoveredCell.Z + dz);
                if (!graph.TryGetSegmentBetween(hoveredCell, neighbor, out var segmentId))
                {
                    continue;
                }

                var neighborCenter = grid.CellToWorldCenter(neighbor);
                var midpoint = Vector2.Lerp(nodeCenter, neighborCenter, 0.5f);
                var distance = Vector2.Distance(hit2D, midpoint);

                if (!found || distance < bestDistance || (Mathf.Approximately(distance, bestDistance) && segmentId.Value < bestId.Value))
                {
                    found = true;
                    bestDistance = distance;
                    bestId = segmentId;
                }
            }

            nearest = bestId;
            return found;
        }
    }
}
