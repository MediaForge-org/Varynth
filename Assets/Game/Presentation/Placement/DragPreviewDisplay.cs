using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// Multi-ghost drag/repeat preview -- exactly two persistent MeshFilter/
    /// MeshRenderer pairs total (one "all-valid" group, one "any-invalid" group; a
    /// planned building is fully valid or fully invalid as a unit), never one
    /// GameObject/Mesh per planned origin. Reuses the same merged-mesh-append
    /// pattern already proven by PlacementGhostDisplay/SurfaceOverlayMeshBuilder.
    /// Rebuilt only when the planned-origin list actually changes (drag start fixed,
    /// end cell changes on mouse-move frames -- the same "rebuild only on real
    /// change" discipline as the single-building ghost, just triggered more often
    /// during an active drag).
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class DragPreviewDisplay : MonoBehaviour
    {
        [SerializeField] private Material _validMaterial;
        [SerializeField] private Transform _invalidGroupTransform;
        [SerializeField] private MeshFilter _invalidMeshFilter;
        [SerializeField] private MeshRenderer _invalidMeshRenderer;
        [SerializeField] private Material _invalidMaterial;

        private MeshFilter _validMeshFilter;
        private MeshRenderer _validMeshRenderer;

        private void Awake()
        {
            EnsureComponents();
        }

        public void Show(
            IReadOnlyList<(GridCoordinate Origin, bool IsValid)> plans,
            BuildingDefinition definition,
            BuildingRotation rotation,
            WorldGrid grid,
            IWorldHeightSource heights)
        {
            EnsureComponents();

            var effectiveWidth = rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;

            var validVertices = new List<Vector3>();
            var validTriangles = new List<int>();
            var invalidVertices = new List<Vector3>();
            var invalidTriangles = new List<int>();

            foreach (var plan in plans)
            {
                var cells = BuildingFootprint.GetOccupiedCells(plan.Origin, definition.FootprintWidth, definition.FootprintLength, rotation);
                var vertices = plan.IsValid ? validVertices : invalidVertices;
                var triangles = plan.IsValid ? validTriangles : invalidTriangles;

                foreach (var cell in cells)
                {
                    AppendCellQuad(grid, heights, cell, 0.05f, vertices, triangles);
                }

                var (position, worldRotation, scale) = PlacementPresentationMath.ComputeBuildingTransform(
                    plan.Origin, effectiveWidth, effectiveLength, rotation, grid, heights);
                AppendBox(position, worldRotation, scale, vertices, triangles);
            }

            _validMeshFilter.sharedMesh = BuildMesh("DragPreviewValid", validVertices, validTriangles);
            _validMeshRenderer.sharedMaterial = _validMaterial;
            _validMeshRenderer.enabled = validVertices.Count > 0;

            if (_invalidMeshFilter != null)
            {
                _invalidMeshFilter.sharedMesh = BuildMesh("DragPreviewInvalid", invalidVertices, invalidTriangles);
                _invalidMeshRenderer.sharedMaterial = _invalidMaterial;
                _invalidMeshRenderer.enabled = invalidVertices.Count > 0;
            }
        }

        public void Hide()
        {
            EnsureComponents();
            _validMeshRenderer.enabled = false;
            if (_invalidMeshRenderer != null)
            {
                _invalidMeshRenderer.enabled = false;
            }
        }

        private static Mesh BuildMesh(string name, List<Vector3> vertices, List<int> triangles)
        {
            var mesh = new Mesh { name = name };
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        private static void AppendCellQuad(WorldGrid grid, IWorldHeightSource heights, GridCoordinate cell, float heightOffset, List<Vector3> vertices, List<int> triangles)
        {
            var quadMesh = Varynth.World.Grid.GridCellMeshBuilder.BuildCellQuad(grid, heights, cell, heightOffset);
            var baseIndex = vertices.Count;
            vertices.AddRange(quadMesh.vertices);
            foreach (var index in quadMesh.triangles)
            {
                triangles.Add(baseIndex + index);
            }

            Object.Destroy(quadMesh);
        }

        // Standard unit cube (centered at local origin, extents +/-0.5), transformed
        // by the same upright/yaw-only rotation math the single ghost's building
        // preview uses -- no per-instance GameObject/Transform, baked straight into
        // merged vertex data instead.
        private static readonly Vector3[] UnitCubeCorners =
        {
            new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f)
        };

        private static readonly int[] UnitCubeTriangles =
        {
            0, 2, 1, 0, 3, 2, // bottom
            4, 5, 6, 4, 6, 7, // top
            0, 1, 5, 0, 5, 4, // front
            1, 2, 6, 1, 6, 5, // right
            2, 3, 7, 2, 7, 6, // back
            3, 0, 4, 3, 4, 7  // left
        };

        private static void AppendBox(Vector3 position, Quaternion rotation, Vector3 scale, List<Vector3> vertices, List<int> triangles)
        {
            var baseIndex = vertices.Count;
            foreach (var corner in UnitCubeCorners)
            {
                var scaled = Vector3.Scale(corner, scale);
                vertices.Add(position + rotation * scaled);
            }

            foreach (var index in UnitCubeTriangles)
            {
                triangles.Add(baseIndex + index);
            }
        }

        private void EnsureComponents()
        {
            if (_validMeshFilter == null) _validMeshFilter = GetComponent<MeshFilter>();
            if (_validMeshRenderer == null) _validMeshRenderer = GetComponent<MeshRenderer>();
        }
    }
}
