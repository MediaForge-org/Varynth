using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// The real building-placement ghost: a terrain-following footprint outline PLUS
    /// an upright building-shape preview, both reused across the whole session (never
    /// Instantiate/Destroy per frame) and updated together on Show(). The footprint
    /// quads conform to terrain per cell (same primitive GridCellHighlight/
    /// SurfaceOverlayMeshBuilder already use); the building preview stays
    /// horizontal/upright -- only yaw-rotated by BuildingRotation, never tilted to a
    /// terrain normal, per explicit instruction. Valid/invalid is visible on both parts.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class PlacementGhostDisplay : MonoBehaviour
    {
        [SerializeField] private Material _footprintValidMaterial;
        [SerializeField] private Material _footprintInvalidMaterial;
        [SerializeField] private Transform _buildingPreviewTransform;
        [SerializeField] private MeshFilter _buildingPreviewMeshFilter;
        [SerializeField] private MeshRenderer _buildingPreviewMeshRenderer;
        [SerializeField] private Material _buildingValidMaterial;
        [SerializeField] private Material _buildingInvalidMaterial;

        private MeshFilter _footprintMeshFilter;
        private MeshRenderer _footprintMeshRenderer;

        private void Awake()
        {
            EnsureComponents();
        }

        public void Show(
            IReadOnlyList<GridCoordinate> footprintCells,
            WorldGrid grid,
            IWorldHeightSource heights,
            Mesh buildingMesh,
            Vector3 buildingPosition,
            Quaternion buildingRotation,
            Vector3 buildingScale,
            bool isValid)
        {
            EnsureComponents();

            var footprintMesh = BuildFootprintMesh(grid, heights, footprintCells);
            _footprintMeshFilter.sharedMesh = footprintMesh;
            _footprintMeshRenderer.sharedMaterial = isValid ? _footprintValidMaterial : _footprintInvalidMaterial;
            _footprintMeshRenderer.enabled = true;

            if (_buildingPreviewMeshFilter != null)
            {
                _buildingPreviewMeshFilter.sharedMesh = buildingMesh;
                _buildingPreviewMeshRenderer.sharedMaterial = isValid ? _buildingValidMaterial : _buildingInvalidMaterial;
                _buildingPreviewMeshRenderer.enabled = true;

                // Upright only: no pitch/roll from terrain normal, only yaw from BuildingRotation
                // (baked into buildingRotation by the caller via PlacementPresentationMath).
                _buildingPreviewTransform.SetPositionAndRotation(buildingPosition, buildingRotation);
                _buildingPreviewTransform.localScale = buildingScale;
            }
        }

        public void Hide()
        {
            EnsureComponents();
            _footprintMeshRenderer.enabled = false;
            if (_buildingPreviewMeshRenderer != null)
            {
                _buildingPreviewMeshRenderer.enabled = false;
            }
        }

        private static Mesh BuildFootprintMesh(WorldGrid grid, IWorldHeightSource heights, IReadOnlyList<GridCoordinate> cells)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            foreach (var cell in cells)
            {
                var quadMesh = GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, 0.05f);
                var baseIndex = vertices.Count;
                vertices.AddRange(quadMesh.vertices);
                foreach (var index in quadMesh.triangles)
                {
                    triangles.Add(baseIndex + index);
                }

                Object.Destroy(quadMesh);
            }

            var mesh = new Mesh { name = "PlacementGhostFootprint" };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        private void EnsureComponents()
        {
            if (_footprintMeshFilter == null)
            {
                _footprintMeshFilter = GetComponent<MeshFilter>();
            }

            if (_footprintMeshRenderer == null)
            {
                _footprintMeshRenderer = GetComponent<MeshRenderer>();
            }
        }
    }
}
