using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// Route preview before confirmation -- one merged mesh of the same tessellated
    /// segment strips RoadMeshBuilder uses for the finished road (no separate
    /// junction-patch geometry; a preview doesn't need final-quality joints), so the
    /// preview never shows the same terrain-poke-through undersampling artifact the
    /// finished road previously did. Because RoadRouter only ever returns a fully
    /// valid path (or none at all), the whole preview is valid or invalid as one
    /// unit -- one material, not per-segment. Rebuilt only when the computed route
    /// actually changes, never per frame/per mouse-move-without-change.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class RoadPreviewDisplay : MonoBehaviour
    {
        [SerializeField] private Material _validMaterial;
        [SerializeField] private Material _invalidMaterial;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            EnsureComponents();
        }

        public void Show(IReadOnlyList<GridCoordinate> path, RoadDefinition definition, WorldGrid grid, IWorldHeightSource heights, bool isValid)
        {
            EnsureComponents();

            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var halfWidth = Mathf.Max(0.5f, (definition?.LogicalWidthCells ?? 1) * grid.CellSize * 0.5f);

            if (path != null)
            {
                for (var i = 0; i < path.Count - 1; i++)
                {
                    RoadMeshBuilder.AppendSegmentQuadStrip(grid, heights, path[i], path[i + 1], halfWidth, RoadVisualConfig.RenderClearance, vertices, triangles);
                }
            }

            var mesh = new Mesh { name = "RoadPreview" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            _meshFilter.sharedMesh = mesh;
            _meshRenderer.sharedMaterial = isValid ? _validMaterial : _invalidMaterial;
            _meshRenderer.enabled = vertices.Count > 0;
        }

        public void Hide()
        {
            EnsureComponents();
            _meshRenderer.enabled = false;
        }

        private void EnsureComponents()
        {
            if (_meshFilter == null) _meshFilter = GetComponent<MeshFilter>();
            if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}
