using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Pure mesh construction for one Buildability-overlay category, aggregated
    /// across every island into a single mesh (not one mesh per island, not one
    /// GameObject per cell). Reuses GridCellMeshBuilder's already-verified per-cell
    /// quad geometry/winding rather than re-deriving it.
    ///
    /// The stock "Universal Render Pipeline/Unlit" shader was checked against its
    /// actual installed source (UnlitForwardPass.hlsl) and does not read a per-vertex
    /// COLOR attribute at all -- so this deliberately builds one mesh per category
    /// with a flat material color each, the same proven pattern already used for
    /// DebugGrid/CellHighlight, instead of a single vertex-colored mesh.
    /// </summary>
    public static class SurfaceOverlayMeshBuilder
    {
        public readonly struct IslandSurfaceSource
        {
            public readonly IslandSurfaceMap Map;
            public readonly RectInt CellBounds;
            public readonly IWorldHeightSource Heights;

            public IslandSurfaceSource(IslandSurfaceMap map, RectInt cellBounds, IWorldHeightSource heights)
            {
                Map = map;
                CellBounds = cellBounds;
                Heights = heights;
            }
        }

        public static Mesh BuildCategoryMesh(
            WorldGrid grid,
            IReadOnlyList<IslandSurfaceSource> islands,
            SurfaceCellFlags requiredFlag,
            float heightOffset)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            foreach (var island in islands)
            {
                AppendIslandCells(grid, island, requiredFlag, heightOffset, vertices, triangles);
            }

            var mesh = new Mesh { name = $"SurfaceOverlay_{requiredFlag}" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        /// <summary>
        /// Thin cell-boundary LINE mesh over every qualifying cell -- the Player
        /// Placement Grid's own geometry, deliberately NOT the filled-quad
        /// BuildCategoryMesh above (that one stays reserved for the F2 debug
        /// Buildable/Coast/RockOrSteep overlay, which is a developer tool, not
        /// player-facing UX). A large solid translucent fill covering an entire
        /// island reads as flooding/overpainted water at a glance -- a classic
        /// build-grid look (fine per-cell line outlines, terrain still fully
        /// visible through the gaps) is the correct player-facing affordance. Still
        /// one merged mesh per island, no GameObject/mesh per cell, so future
        /// chunked/streaming subdivision remains a purely additive change.
        /// </summary>
        public static Mesh BuildCategoryLineMesh(
            WorldGrid grid,
            IReadOnlyList<IslandSurfaceSource> islands,
            SurfaceCellFlags requiredFlag,
            float heightOffset)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            foreach (var island in islands)
            {
                AppendIslandCellOutlines(grid, island, requiredFlag, heightOffset, vertices, indices);
            }

            var mesh = new Mesh { name = $"SurfaceOverlayLines_{requiredFlag}" };
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            mesh.RecalculateBounds();
            return mesh;
        }

        private static void AppendIslandCells(
            WorldGrid grid,
            IslandSurfaceSource island,
            SurfaceCellFlags requiredFlag,
            float heightOffset,
            List<Vector3> vertices,
            List<int> triangles)
        {
            var bounds = island.CellBounds;

            for (var cz = bounds.yMin; cz < bounds.yMax; cz++)
            {
                for (var cx = bounds.xMin; cx < bounds.xMax; cx++)
                {
                    var cell = new GridCoordinate(cx, cz);
                    if (!island.Map.TryGetFlags(cell, out var flags) || (flags & requiredFlag) == 0)
                    {
                        continue;
                    }

                    AppendCellQuad(grid, island.Heights, cell, heightOffset, vertices, triangles);
                }
            }
        }

        private static void AppendIslandCellOutlines(
            WorldGrid grid,
            IslandSurfaceSource island,
            SurfaceCellFlags requiredFlag,
            float heightOffset,
            List<Vector3> vertices,
            List<int> indices)
        {
            var bounds = island.CellBounds;

            for (var cz = bounds.yMin; cz < bounds.yMax; cz++)
            {
                for (var cx = bounds.xMin; cx < bounds.xMax; cx++)
                {
                    var cell = new GridCoordinate(cx, cz);
                    if (!island.Map.TryGetFlags(cell, out var flags) || (flags & requiredFlag) == 0)
                    {
                        continue;
                    }

                    AppendCellOutline(grid, island.Heights, cell, heightOffset, vertices, indices);
                }
            }
        }

        private static void AppendCellOutline(
            WorldGrid grid,
            IWorldHeightSource heights,
            GridCoordinate cell,
            float heightOffset,
            List<Vector3> vertices,
            List<int> indices)
        {
            var quadMesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset);
            var corners = quadMesh.vertices; // order: p00, p10, p01, p11 (see GridCellMeshBuilder)

            var baseIndex = vertices.Count;
            vertices.AddRange(corners);

            // 4 edges of the cell quad: p00-p10, p10-p11, p11-p01, p01-p00.
            indices.Add(baseIndex + 0); indices.Add(baseIndex + 1);
            indices.Add(baseIndex + 1); indices.Add(baseIndex + 3);
            indices.Add(baseIndex + 3); indices.Add(baseIndex + 2);
            indices.Add(baseIndex + 2); indices.Add(baseIndex + 0);

            Object.DestroyImmediate(quadMesh);
        }

        private static void AppendCellQuad(
            WorldGrid grid,
            IWorldHeightSource heights,
            GridCoordinate cell,
            float heightOffset,
            List<Vector3> vertices,
            List<int> triangles)
        {
            var quadMesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset);

            var baseIndex = vertices.Count;
            vertices.AddRange(quadMesh.vertices);
            foreach (var index in quadMesh.triangles)
            {
                triangles.Add(baseIndex + index);
            }

            Object.DestroyImmediate(quadMesh);
        }
    }
}
