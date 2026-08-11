using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Pure, testable local slope estimation: samples the cell center plus its four
    /// direct neighbors (N/E/S/W, one cell size away) and returns the steepest
    /// height-delta converted to degrees. Computed once at surface-map generation
    /// time, never per hover-frame.
    ///
    /// A missing sample (off the edge of this island's terrain -- e.g. a neighbor
    /// cell that falls into open water with no registered terrain) is excluded from
    /// the calculation rather than treated as height 0; a missing sample must never
    /// fabricate an artificial cliff. If the center itself has no data, or none of
    /// the four neighbors do, the result is a documented neutral 0 degrees rather
    /// than a fabricated extreme slope.
    /// </summary>
    public static class SlopeEstimator
    {
        public static float EstimateSlopeDegrees(IWorldHeightSource heights, WorldGrid grid, GridCoordinate cell)
        {
            var center = grid.CellToWorldCenter(cell);

            if (!heights.TryGetHeight(center.x, center.y, out var centerHeight))
            {
                return 0f;
            }

            var cellSize = grid.CellSize;
            var maxDelta = 0f;
            var validSamples = 0;

            if (heights.TryGetHeight(center.x + cellSize, center.y, out var eastHeight))
            {
                maxDelta = Mathf.Max(maxDelta, Mathf.Abs(eastHeight - centerHeight));
                validSamples++;
            }

            if (heights.TryGetHeight(center.x - cellSize, center.y, out var westHeight))
            {
                maxDelta = Mathf.Max(maxDelta, Mathf.Abs(westHeight - centerHeight));
                validSamples++;
            }

            if (heights.TryGetHeight(center.x, center.y + cellSize, out var northHeight))
            {
                maxDelta = Mathf.Max(maxDelta, Mathf.Abs(northHeight - centerHeight));
                validSamples++;
            }

            if (heights.TryGetHeight(center.x, center.y - cellSize, out var southHeight))
            {
                maxDelta = Mathf.Max(maxDelta, Mathf.Abs(southHeight - centerHeight));
                validSamples++;
            }

            if (validSamples == 0)
            {
                return 0f;
            }

            return Mathf.Atan(maxDelta / cellSize) * Mathf.Rad2Deg;
        }
    }
}
