using System.Collections.Generic;
using UnityEngine;
using Varynth.World.Terrain;

namespace Varynth.World.Grid
{
    /// <summary>
    /// Pure mesh-construction logic for the debug build grid. Produces a single
    /// LineList-topology mesh covering a bounded cell area, built once (not per
    /// frame) and not one GameObject per cell/line. Each vertex is height-sampled
    /// so the grid visually hugs terrain relief instead of floating/clipping.
    /// </summary>
    public static class GridMeshBuilder
    {
        public static Mesh Build(WorldGrid grid, IWorldHeightSource heights, RectInt cellBounds, float heightOffset = 0.05f)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            var xMin = cellBounds.xMin;
            var xMax = cellBounds.xMax;
            var yMin = cellBounds.yMin;
            var yMax = cellBounds.yMax;

            // Vertical grid lines (constant X), sampled at every row boundary.
            for (var gx = xMin; gx <= xMax; gx++)
            {
                var worldX = grid.Origin.X + gx * grid.CellSize;
                var lineStart = vertices.Count;

                for (var gz = yMin; gz <= yMax; gz++)
                {
                    var worldZ = grid.Origin.Z + gz * grid.CellSize;
                    vertices.Add(SamplePoint(heights, worldX, worldZ, heightOffset));
                }

                AppendLineSegments(indices, lineStart, yMax - yMin + 1);
            }

            // Horizontal grid lines (constant Z), sampled at every column boundary.
            for (var gz = yMin; gz <= yMax; gz++)
            {
                var worldZ = grid.Origin.Z + gz * grid.CellSize;
                var lineStart = vertices.Count;

                for (var gx = xMin; gx <= xMax; gx++)
                {
                    var worldX = grid.Origin.X + gx * grid.CellSize;
                    vertices.Add(SamplePoint(heights, worldX, worldZ, heightOffset));
                }

                AppendLineSegments(indices, lineStart, xMax - xMin + 1);
            }

            var mesh = new Mesh { name = "DebugGridMesh" };
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            mesh.RecalculateBounds();
            return mesh;
        }

        private static Vector3 SamplePoint(IWorldHeightSource heights, float worldX, float worldZ, float heightOffset)
        {
            var y = heights.TryGetHeight(worldX, worldZ, out var height) ? height : 0f;
            return new Vector3(worldX, y + heightOffset, worldZ);
        }

        private static void AppendLineSegments(List<int> indices, int firstVertexIndex, int pointCount)
        {
            for (var i = 0; i < pointCount - 1; i++)
            {
                indices.Add(firstVertexIndex + i);
                indices.Add(firstVertexIndex + i + 1);
            }
        }
    }
}
