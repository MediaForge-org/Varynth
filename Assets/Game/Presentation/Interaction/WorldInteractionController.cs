using System.Collections.Generic;
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
    /// -> GridCellHighlight. Also owns the debug-grid (G), surface/buildability
    /// overlay (F2) and resource-candidate overlay (F3) visibility toggles -- input
    /// reading belongs here in Presentation, never inside a Varynth.World component.
    /// Multi-island (Phase 2B): builds one UnityTerrainHeightSource per registered
    /// terrain, wrapped in a CompositeWorldHeightSource, and a WorldPointer over all
    /// registered terrain colliders -- hover/highlight logic itself is unchanged,
    /// since it already only depended on the IWorldHeightSource/WorldPointer
    /// abstractions.
    /// </summary>
    public sealed class WorldInteractionController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private UnityEngine.Terrain[] _terrains;
        [SerializeField] private TerrainCollider[] _terrainColliders;
        [SerializeField] private float _cellSize = 4f;
        [SerializeField] private Vector2 _gridOrigin = Vector2.zero;
        [SerializeField] private GridCellHighlight _highlight;
        [SerializeField] private GridDisplay _gridDisplay;
        [SerializeField] private GridDisplay[] _surfaceOverlayDisplays;
        [SerializeField] private ResourceCandidateMarkers _resourceMarkers;
        [SerializeField] private Key _toggleGridKey = Key.G;
        [SerializeField] private Key _toggleSurfaceOverlayKey = Key.F2;
        [SerializeField] private Key _toggleResourceOverlayKey = Key.F3;

        private WorldGrid _grid;
        private IWorldHeightSource _heightSource;
        private WorldPointer _pointer;
        private bool _gridVisible = true;
        private bool _surfaceOverlayVisible;
        private bool _resourceOverlayVisible;

        public WorldGrid Grid => _grid;

        private void Awake()
        {
            _grid = new WorldGrid(_cellSize, _gridOrigin);

            var terrains = _terrains ?? System.Array.Empty<UnityEngine.Terrain>();
            var terrainSources = new List<UnityTerrainHeightSource>(terrains.Length);
            foreach (var terrain in terrains)
            {
                if (terrain != null)
                {
                    terrainSources.Add(new UnityTerrainHeightSource(terrain));
                }
            }
            _heightSource = new CompositeWorldHeightSource(terrainSources);

            var colliders = _terrainColliders ?? System.Array.Empty<TerrainCollider>();
            var colliderList = new List<Collider>(colliders.Length);
            foreach (var collider in colliders)
            {
                if (collider != null)
                {
                    colliderList.Add(collider);
                }
            }
            _pointer = new WorldPointer(_grid, colliderList);
        }

        private void Update()
        {
            UpdateHover();
            UpdateGridToggle();
            UpdateSurfaceOverlayToggle();
            UpdateResourceOverlayToggle();
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

        private void UpdateSurfaceOverlayToggle()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null || _surfaceOverlayDisplays == null)
            {
                return;
            }

            if (keyboard[_toggleSurfaceOverlayKey].wasPressedThisFrame)
            {
                _surfaceOverlayVisible = !_surfaceOverlayVisible;
                foreach (var display in _surfaceOverlayDisplays)
                {
                    if (display != null)
                    {
                        display.SetVisible(_surfaceOverlayVisible);
                    }
                }
            }
        }

        private void UpdateResourceOverlayToggle()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null || _resourceMarkers == null)
            {
                return;
            }

            if (keyboard[_toggleResourceOverlayKey].wasPressedThisFrame)
            {
                _resourceOverlayVisible = !_resourceOverlayVisible;
                _resourceMarkers.SetVisible(_resourceOverlayVisible);
            }
        }
    }
}
