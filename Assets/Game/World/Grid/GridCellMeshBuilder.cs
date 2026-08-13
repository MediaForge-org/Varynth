using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Terrain;

namespace Varynth.World.Grid
{
    /// <summary>
    /// Pure mesh-construction logic for a single grid-cell highlight quad. Builds
    /// world-space geometry from the four cell corners, height-sampled against the
    /// real terrain surface, so the highlight actually follows terrain relief instead
    /// of being a flat plane cutting through hills. No MonoBehaviour, no per-frame
    /// rebuild concerns -- the caller decides when to rebuild (only on cell change).
    /// </summary>
    public static class GridCellMeshBuilder
    {
        public static Mesh BuildCellQuad(WorldGrid grid, IWorldHeightSource heights, GridCoordinate cell, float heightOffset)
        {
            var x0 = grid.Origin.X + cell.X * grid.CellSize;
            var x1 = x0 + grid.CellSize;
            var z0 = grid.Origin.Z + cell.Z * grid.CellSize;
            var z1 = z0 + grid.CellSize;

            var p00 = SamplePoint(heights, x0, z0, heightOffset);
            var p10 = SamplePoint(heights, x1, z0, heightOffset);
            var p01 = SamplePoint(heights, x0, z1, heightOffset);
            var p11 = SamplePoint(heights, x1, z1, heightOffset);

            var vertices = new List<Vector3> { p00, p10, p01, p11 };
            // Winding chosen so the recalculated normal faces +Y (verified by
            // GridCellMeshBuilderTests.BuildCellQuad_NormalPointsUpward).
            var triangles = new[] { 0, 2, 3, 0, 3, 1 };

            var mesh = new Mesh { name = "GridCellHighlightMesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        private static Vector3 SamplePoint(IWorldHeightSource heights, float worldX, float worldZ, float heightOffset)
        {
            var y = heights.TryGetHeight(worldX, worldZ, out var height) ? height : 0f;
            return new Vector3(worldX, y + heightOffset, worldZ);
        }
    }
}
