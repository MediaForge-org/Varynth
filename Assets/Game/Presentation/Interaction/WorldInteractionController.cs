using UnityEngine;
using UnityEngine.InputSystem;
using Varynth.Presentation.Visualization;
using Varynth.World.Grid;
using Varynth.World.Interaction;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Interaction
{
    /// <summary>
    /// The one MonoBehaviour that wires input into the world each frame:
    /// Mouse.current.position -> Camera.ScreenPointToRay -> WorldPointer -> WorldGrid
    /// -> GridCellHighlight. Also owns the debug-grid visibility toggle (G) -- input
    /// reading belongs here in Presentation, never inside a Varynth.World component.
    /// </summary>
    public sealed class WorldInteractionController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private UnityEngine.Terrain _terrain;
        [SerializeField] private TerrainCollider _terrainCollider;
        [SerializeField] private float _cellSize = 4f;
        [SerializeField] private Vector2 _gridOrigin = Vector2.zero;
        [SerializeField] private GridCellHighlight _highlight;
        [SerializeField] private GridDisplay _gridDisplay;
        [SerializeField] private Key _toggleGridKey = Key.G;

        private WorldGrid _grid;
        private IWorldHeightSource _heightSource;
        private WorldPointer _pointer;
        private bool _gridVisible = true;

        public WorldGrid Grid => _grid;

        private void Awake()
        {
            _grid = new WorldGrid(_cellSize, _gridOrigin);
            _heightSource = new UnityTerrainHeightSource(_terrain);
            _pointer = new WorldPointer(_grid, _terrainCollider);
        }

        private void Update()
        {
            UpdateHover();
            UpdateGridToggle();
        }

        private void UpdateHover()
        {
            if (_camera == null || _highlight == null)
            {
                return;
            }

            var mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(mouse.position.ReadValue());
            if (_pointer.TryRaycast(ray, out var worldPosition))
            {
                var cell = _pointer.ToCell(worldPosition);
                _highlight.SetCell(cell, _grid, _heightSource);
            }
            else
            {
                _highlight.Hide();
            }
        }

        private void UpdateGridToggle()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null || _gridDisplay == null)
            {
                return;
            }

            if (keyboard[_toggleGridKey].wasPressedThisFrame)
            {
                _gridVisible = !_gridVisible;
                _gridDisplay.SetVisible(_gridVisible);
            }
        }
    }
}
