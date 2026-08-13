using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Pure mesh construction for one island's road network -- same shape as
    /// SurfaceOverlayMeshBuilder (aggregated single mesh, no GameObject per cell/
    /// segment). Every emitted vertex is height-sampled at its own real world X/Z
    /// via IWorldHeightSource, and both segment strips and junction patches are
    /// tessellated finer than the project's real Terrain heightmap sample spacing
    /// (RoadVisualConfig.MaxTessellationSpacing, applied along BOTH a segment's
    /// length and its width) so the road's own piecewise-linear surface tracks real
    /// terrain relief closely enough that terrain never pokes up through the road
    /// mesh between samples. Junction geometry is a generic two-ring octagon fan per
    /// node, emitted identically regardless of connected-direction count (1-8) -- no
    /// per-degree special-casing that could break above degree 4. A stub segment's
    /// own cross-section endpoints at the node are, by construction, exactly the two
    /// *outer*-ring octagon vertices at the directions perpendicular to its travel
    /// direction, so the strip and the patch always share bit-identical seam
    /// vertices (no gap, no overlap) for every one of the 8 directions, orthogonal or
    /// diagonal alike. Gameplay graph stays discrete; only this visual layer is
    /// shaped to read as continuous ("keine freie Spline-Simulation").
    /// </summary>
    public static class RoadMeshBuilder
    {
        private static readonly RoadDirection[] OctagonDirections =
        {
            RoadDirection.N, RoadDirection.NE, RoadDirection.E, RoadDirection.SE,
            RoadDirection.S, RoadDirection.SW, RoadDirection.W, RoadDirection.NW
        };

        public static Mesh BuildIslandMesh(WorldGrid grid, RoadGraph graph, RoadDefinition definition, IWorldHeightSource heights, float heightOffset)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var halfWidth = Mathf.Max(0.5f, definition.LogicalWidthCells * grid.CellSize * 0.5f);

            foreach (var segment in graph.Segments)
            {
                AppendSegmentQuadStrip(grid, heights, segment.From, segment.To, halfWidth, heightOffset, vertices, triangles);
            }

            foreach (var node in graph.Nodes)
            {
                AppendJunctionPatch(grid, heights, node, halfWidth, heightOffset, vertices, triangles);
            }

            var mesh = new Mesh { name = "RoadNetworkMesh" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        /// <summary>
        /// Builds one segment's road surface as a full length x width grid of quads
        /// (not just a 2-column, sparse-length strip) -- shared by BuildIslandMesh and
        /// RoadPreviewDisplay so both use identical tessellation/offset behavior.
        /// Sample counts are derived from RoadVisualConfig.MaxTessellationSpacing, so
        /// a long segment or a wide road automatically gets more subdivisions rather
        /// than a fixed constant that only happened to work at one specific scale.
        /// </summary>
        public static void AppendSegmentQuadStrip(
            WorldGrid grid, IWorldHeightSource heights, GridCoordinate from, GridCoordinate to, float halfWidth, float heightOffset,
            List<Vector3> vertices, List<int> triangles)
        {
            var fromCenterTuple = grid.CellToWorldCenter(from);
            var toCenterTuple = grid.CellToWorldCenter(to);
            var fromCenter = new Vector2(fromCenterTuple.X, fromCenterTuple.Z);
            var toCenter = new Vector2(toCenterTuple.X, toCenterTuple.Z);
            var length = Vector2.Distance(fromCenter, toCenter);
            var direction2D = (toCenter - fromCenter).normalized;
            var perpendicularUnit = new Vector2(-direction2D.y, direction2D.x);
            var width = halfWidth * 2f;

            var lengthSteps = Mathf.Max(2, Mathf.CeilToInt(length / RoadVisualConfig.MaxTessellationSpacing) + 1);
            var widthSteps = Mathf.Max(2, Mathf.CeilToInt(width / RoadVisualConfig.MaxTessellationSpacing) + 1);

            // grid[i][j]: i = along length (0=from, lengthSteps-1=to), j = across
            // width (0 = -halfWidth "right", widthSteps-1 = +halfWidth "left" -- same
            // "right"/"left" convention the original 2-column strip used, so the two
            // edge columns land on exactly the same world positions as before).
            var indexGrid = new int[lengthSteps, widthSteps];
            for (var i = 0; i < lengthSteps; i++)
            {
                var t = i / (float)(lengthSteps - 1);
                var center2D = Vector2.Lerp(fromCenter, toCenter, t);

                for (var j = 0; j < widthSteps; j++)
                {
                    var v = -halfWidth + j * (width / (widthSteps - 1));
                    var point2D = center2D + perpendicularUnit * v;
                    indexGrid[i, j] = AppendVertex(heights, point2D, heightOffset, vertices);
                }
            }

            for (var i = 1; i < lengthSteps; i++)
            {
                for (var j = 0; j < widthSteps - 1; j++)
                {
                    var prevLow = indexGrid[i - 1, j];
                    var prevHigh = indexGrid[i - 1, j + 1];
                    var curLow = indexGrid[i, j];
                    var curHigh = indexGrid[i, j + 1];

                    // Winding verified empirically (RoadMeshBuilderTests) to face +Y.
                    triangles.Add(prevHigh); triangles.Add(curLow); triangles.Add(prevLow);
                    triangles.Add(prevHigh); triangles.Add(curHigh); triangles.Add(curLow);
                }
            }
        }

        private static void AppendJunctionPatch(
            WorldGrid grid, IWorldHeightSource heights, RoadNode node, float halfWidth, float heightOffset,
            List<Vector3> vertices, List<int> triangles)
        {
            var center2DTuple = grid.CellToWorldCenter(node.Cell);
            var center2D = new Vector2(center2DTuple.X, center2DTuple.Z);

            var centerIndex = vertices.Count;
            vertices.Add(SamplePoint(heights, center2D, heightOffset));

            // Two-ring fan (center -> inner ring -> outer ring) rather than a single
            // center-to-outer-ring fan: at typical halfWidth (~2 m) a single 8-triangle
            // fan can still leave each triangle wider than RoadVisualConfig's terrain-
            // safe spacing, and it reads visually as a distinct flat "polygon" dropped
            // between the segment strips. The extra inner ring keeps every triangle
            // edge below that spacing and blends the patch into the surrounding strips
            // more smoothly. The OUTER ring uses exactly the original single-ring
            // formula (center + halfWidth*unit(direction)) so it still shares bit-
            // identical seam vertices with segment stub endpoints -- only the interior
            // subdivision is new.
            var innerRadius = halfWidth * 0.5f;
            var innerIndices = new int[OctagonDirections.Length];
            var outerIndices = new int[OctagonDirections.Length];
            for (var i = 0; i < OctagonDirections.Length; i++)
            {
                var (dx, dz) = OctagonDirections[i].ToDelta();
                var unit = new Vector2(dx, dz).normalized;
                innerIndices[i] = AppendVertex(heights, center2D + unit * innerRadius, heightOffset, vertices);
                outerIndices[i] = AppendVertex(heights, center2D + unit * halfWidth, heightOffset, vertices);
            }

            // Winding verified empirically (RoadMeshBuilderTests) to face +Y, same
            // convention as AppendSegmentQuadStrip.
            for (var i = 0; i < OctagonDirections.Length; i++)
            {
                var next = (i + 1) % OctagonDirections.Length;

                // Center -> inner ring fan.
                triangles.Add(centerIndex);
                triangles.Add(innerIndices[i]);
                triangles.Add(innerIndices[next]);

                // Inner ring -> outer ring strip.
                triangles.Add(innerIndices[next]);
                triangles.Add(innerIndices[i]);
                triangles.Add(outerIndices[i]);

                triangles.Add(innerIndices[next]);
                triangles.Add(outerIndices[i]);
                triangles.Add(outerIndices[next]);
            }
        }

        private static int AppendVertex(IWorldHeightSource heights, Vector2 world2D, float heightOffset, List<Vector3> vertices)
        {
            vertices.Add(SamplePoint(heights, world2D, heightOffset));
            return vertices.Count - 1;
        }

        private static Vector3 SamplePoint(IWorldHeightSource heights, Vector2 world2D, float heightOffset)
        {
            var y = heights.TryGetHeight(world2D.x, world2D.y, out var height) ? height : 0f;
            return new Vector3(world2D.x, y + heightOffset, world2D.y);
        }
    }
}
