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
