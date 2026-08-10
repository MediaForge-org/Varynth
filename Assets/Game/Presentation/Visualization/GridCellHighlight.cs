using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Visualization
{
    /// <summary>
    /// Owns a single highlight mesh built from the four world-space corners of the
    /// hovered grid cell (height-sampled against the real terrain surface via
    /// GridCellMeshBuilder), so the highlight follows terrain relief instead of being
    /// a flat plane cutting through hills. Only rebuilds when the cell under the
    /// cursor actually changes -- no per-frame rebuild. No input read here -- driven
    /// externally by WorldInteractionController.
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class GridCellHighlight : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _heightOffset = 0.06f;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private GridCoordinate? _currentCell;

        private void Awake()
        {
            EnsureComponents();
        }

        public void SetCell(GridCoordinate cell, WorldGrid grid, IWorldHeightSource heightSource)
        {
            EnsureComponents();

            if (_currentCell.HasValue && _currentCell.Value == cell)
            {
                return;
            }

            _currentCell = cell;

            var mesh = GridCellMeshBuilder.BuildCellQuad(grid, heightSource, cell, _heightOffset);
            _meshFilter.sharedMesh = mesh;
            _meshRenderer.enabled = true;
        }

        public void Hide()
        {
            EnsureComponents();

            if (!_currentCell.HasValue)
            {
                return;
            }

            _currentCell = null;
            _meshRenderer.enabled = false;
        }

        private void EnsureComponents()
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }

            if (_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
                if (_material != null)
                {
                    _meshRenderer.sharedMaterial = _material;
                }
            }
        }
    }
}
