using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Varynth.Core.Common;
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
        // Starts hidden (0.2.2 hotfix): real runtime diagnosis (instrumented render-
        // state logging + a real captured screenshot) proved the reported "fine grid
        // stays visible on the island with no tool active" bug was this Developer
        // Debug Grid, not the Player Placement Grid (which was already correctly
        // hidden in every tested state). Defaulting true meant the grid was visible
        // the instant the scene loaded, before any G press -- the exact symptom
        // reported. G still fully, exclusively controls this field/grid; only the
        // starting value changes.
        private bool _gridVisible;
        private bool _surfaceOverlayVisible;
        private bool _resourceOverlayVisible;

        public WorldGrid Grid => _grid;
        public IWorldHeightSource HeightSource => _heightSource;
        public WorldPointer Pointer => _pointer;
        public UnityEngine.Terrain[] Terrains => _terrains;

        /// <summary>
        /// The grid cell currently under the cursor, if any -- computed once per
        /// frame in UpdateHover(). Other Presentation components (PlacementController)
        /// read this instead of re-raycasting, avoiding duplicate WorldPointer setup.
        /// </summary>
        public GridCoordinate? HoveredCell { get; private set; }

        /// <summary>
        /// The real continuous world-space hit position under the cursor, if any --
        /// used by RoadPlacementController's removal picking to disambiguate which
        /// segment at a busy node the cursor is closest to (a snapped GridCoordinate
        /// alone is ambiguous at junctions). Purely a transient Presentation-side
        /// value, never stored in any world/gameplay state.
        /// </summary>
        public Vector3? HoveredWorldPosition { get; private set; }

        private void Awake()
        {
            _grid = new WorldGrid(_cellSize, (_gridOrigin.x, _gridOrigin.y));

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

            // Verified-hidden runtime baseline (0.2.2 hotfix), independent of whatever
            // the saved scene asset's renderer.enabled default happens to be -- mirrors
            // the same principle ConstructionToolCoordinatorHost already applies to the
            // Player Placement Grids, applied here to the Debug Grid.
            if (_gridDisplay != null)
            {
                _gridDisplay.SetVisible(_gridVisible);
            }
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
                HoveredCell = cell;
                HoveredWorldPosition = worldPosition;
                _highlight.SetCell(cell, _grid, _heightSource);
            }
            else
            {
                HoveredCell = null;
                HoveredWorldPosition = null;
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
