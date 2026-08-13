using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Pure per-cell surface classification over one island's cell-bounds rectangle.
    /// Computed once at generation time (Editor scene build), not per hover-frame.
    /// ResourceCandidate is intentionally NOT set here -- it's a second, separate
    /// pass (ResourceCandidateGenerator) over the resulting map.
    /// </summary>
    public static class SurfaceMapGenerator
    {
        public static IslandSurfaceMap Generate(
            WorldGrid grid,
            IWorldHeightSource heights,
            RectInt cellBounds,
            SurfaceClassificationConfig config)
        {
            var origin = new GridCoordinate(cellBounds.xMin, cellBounds.yMin);
            var map = new IslandSurfaceMap(origin, cellBounds.width, cellBounds.height);

            for (var cz = cellBounds.yMin; cz < cellBounds.yMax; cz++)
            {
                for (var cx = cellBounds.xMin; cx < cellBounds.xMax; cx++)
                {
                    var cell = new GridCoordinate(cx, cz);
                    map.SetFlags(cell, ClassifyCell(grid, heights, cell, config));
                }
            }

            return map;
        }

        private static SurfaceCellFlags ClassifyCell(
            WorldGrid grid,
            IWorldHeightSource heights,
            GridCoordinate cell,
            SurfaceClassificationConfig config)
        {
            var center = grid.CellToWorldCenter(cell);

            if (!heights.TryGetHeight(center.X, center.Z, out var height) || height <= config.SeaLevelWorldY)
            {
                return SurfaceCellFlags.Water;
            }

            if (height <= config.SeaLevelWorldY + config.CoastBandHeight)
            {
                return SurfaceCellFlags.Coast;
            }

            var flags = SurfaceCellFlags.Land;
            var slopeDegrees = SlopeEstimator.EstimateSlopeDegrees(heights, grid, cell);

            if (slopeDegrees >= config.SlopeThresholdDegrees)
            {
                flags |= SurfaceCellFlags.RockOrSteep;
            }
            else
            {
                flags |= SurfaceCellFlags.Buildable | SurfaceCellFlags.VegetationCandidate;
            }

            return flags;
        }
    }
}
