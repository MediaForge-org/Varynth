using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.Core.Simulation.Road;
using Varynth.Data.Loading;
using Varynth.Presentation.Interaction;
using Varynth.World.Placement;
using Varynth.World.Roads;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// Own small Idle/Routing state, mirrors PlacementController's Idle/Placing --
    /// kept as its own class (not merged into PlacementController), same separation-
    /// of-concerns reasoning already applied to WorldInteractionController vs.
    /// PlacementController. Tool activation/cancellation and Player Placement Grid
    /// visibility are arbitrated centrally through ConstructionToolCoordinator, never
    /// by reaching into PlacementController directly.
    /// </summary>
    public sealed class RoadPlacementController : MonoBehaviour, ConstructionToolCoordinator.IConstructionTool
    {
        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private Varynth.World.Placement.IslandSurfaceRuntimeData[] _islandSurfaceData;
        [SerializeField] private RoadNetworkDisplay[] _networkDisplays;
        [SerializeField] private RoadPreviewDisplay _preview;
        [SerializeField] private Button _roadToolButton;
        [SerializeField] private Key _removeKey = Key.Delete;
        [SerializeField] private Key _selectToolKey = Key.Digit4;

        private ConstructionToolCoordinator _coordinator;
        private RoadNetworkState _state;
        private ContentRegistry<RoadDefinition> _registry;
        private RoadCommandHandler _commandHandler;
        private PlayerId _localPlayerId;
        private ContentId _selectedRoadId;
        private GridCoordinate? _startCell;
        private IReadOnlyList<GridCoordinate> _currentPath;
        private bool _currentPathValid;
        private GridCoordinate? _lastPreviewedEnd;
        private int _hoveredIslandIndex = -1;
        private IBuildingOccupancyQuery _buildingOccupancy;

        public RoadNetworkState State => _state;
        public ContentRegistry<RoadDefinition> Registry => _registry;
        public bool IsActive => _coordinator != null && _coordinator.ActiveMode == ConstructionToolCoordinator.ConstructionToolMode.Road;
        public IReadOnlyList<GridCoordinate> PreviewedPath => _currentPath;

        public void Configure(ConstructionToolCoordinator coordinator)
        {
            _coordinator = coordinator;
            _coordinator.RegisterRoadTool(this);
        }

        private void Awake()
        {
            _state = new RoadNetworkState(_worldInteraction.Grid);

            var terrains = _worldInteraction.Terrains ?? Array.Empty<UnityEngine.Terrain>();
            var surfaceData = _islandSurfaceData ?? Array.Empty<Varynth.World.Placement.IslandSurfaceRuntimeData>();
            var islandCount = Mathf.Min(terrains.Length, surfaceData.Length);
            for (var i = 0; i < islandCount; i++)
            {
                if (terrains[i] != null && surfaceData[i] != null)
                {
                    _state.AddIsland(surfaceData[i], terrains[i]);
                }
            }

            var contentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Roads");
            _registry = RoadContentBootstrap.LoadRegistry(contentRoot);
            _localPlayerId = PlayerId.NewId();

            if (_registry.TryGet(ContentId.Parse("road.prototype.basic"), out _))
            {
                _selectedRoadId = ContentId.Parse("road.prototype.basic");
            }

            if (_roadToolButton != null) _roadToolButton.onClick.AddListener(SelectRoadTool);

            if (_preview != null) _preview.Hide();
        }

        private void Start()
        {
            if (_coordinator == null)
            {
                var host = FindFirstObjectByType<ConstructionToolCoordinatorHost>();
                if (host != null)
                {
                    Configure(host.Coordinator);
                }
            }

            // Cross-wiring happens in Start (after every Awake ran) -- the two
            // world-state systems never reference each other directly; only the
            // small read-only query interface is composed here, by the one place
            // that legitimately knows about both.
            var buildingController = FindFirstObjectByType<Varynth.Presentation.Placement.PlacementController>();
            _buildingOccupancy = buildingController != null ? buildingController.State : null;
            _commandHandler = new RoadCommandHandler(_state, _registry, _buildingOccupancy);
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard[_selectToolKey].wasPressedThisFrame)
            {
                SelectRoadTool();
            }

            // Checked unconditionally (gated internally on "no tool active") so
            // hovering an existing road segment can be removed even when the Road
            // tool itself isn't the currently selected construction tool.
            UpdateRemoval();

            if (!IsActive)
            {
                return;
            }

            UpdateHoveredIslandAndGrid();
            UpdatePreview();
            UpdateConfirmOrCancel();
        }

        public void SelectRoadTool()
        {
            _coordinator.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Road);
            // Force the next UpdateHoveredIslandAndGrid() call to re-evaluate and
            // re-issue RequestPlacementGridVisibility even if the real hovered island
            // happens to equal this field's stale value from a previous session --
            // -2 is used (not -1) since -1 is itself a legitimate "no island
            // hovered" value this field can hold.
            _hoveredIslandIndex = -2;
        }

        public void CancelTool()
        {
            _startCell = null;
            _currentPath = null;
            _lastPreviewedEnd = null;
            if (_preview != null) _preview.Hide();
            _hoveredIslandIndex = -1;
        }

        private void UpdateHoveredIslandAndGrid()
        {
            var hoveredCell = _worldInteraction.HoveredCell;
            var newIndex = -1;
            if (hoveredCell.HasValue && _state.TryFindIslandIndex(hoveredCell.Value, out var index))
            {
                newIndex = index;
            }

            if (newIndex == _hoveredIslandIndex)
            {
                return;
            }

            _hoveredIslandIndex = newIndex;
            _coordinator.RequestPlacementGridVisibility(_hoveredIslandIndex);
        }

        private void UpdatePreview()
        {
            if (_preview == null || !_startCell.HasValue)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                return;
            }

            if (_lastPreviewedEnd.HasValue && _lastPreviewedEnd.Value.Equals(hoveredCell.Value))
            {
                return; // no meaningful change -- no per-frame route recompute
            }

            _lastPreviewedEnd = hoveredCell.Value;

            _currentPathValid = _state.TryFindRoute(_selectedRoadId, _startCell.Value, hoveredCell.Value, _registry, _buildingOccupancy, out _currentPath);
            _registry.TryGet(_selectedRoadId, out var definition);
            _preview.Show(_currentPath, definition, _worldInteraction.Grid, _worldInteraction.HeightSource, _currentPathValid);
        }

        private void UpdateConfirmOrCancel()
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            if (keyboard == null || mouse == null)
            {
                return;
            }

            if (keyboard.escapeKey.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame)
            {
                if (_startCell.HasValue)
                {
                    _startCell = null;
                    _currentPath = null;
                    _lastPreviewedEnd = null;
                    if (_preview != null) _preview.Hide();
                }
                else
                {
                    _coordinator.RequestDeactivate();
                }

                return;
            }

            if (!mouse.leftButton.wasPressedThisFrame)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                return;
            }

            if (!_startCell.HasValue)
            {
                _startCell = hoveredCell.Value;
                _lastPreviewedEnd = null;
                return;
            }

            if (_currentPathValid && _currentPath != null && _currentPath.Count > 0)
            {
                var command = new BuildRoadCommand(_localPlayerId, GameTick.Zero, _selectedRoadId, _currentPath);
                if (_commandHandler.Handle(command, out _, out _))
                {
                    RoadRuntimeMeshRefresh.RefreshAffectedIslands(_state, _networkDisplays, _registry, _selectedRoadId, _worldInteraction.Grid);
                }
            }

            _startCell = null;
            _currentPath = null;
            _lastPreviewedEnd = null;
            if (_preview != null) _preview.Hide();
        }

        // Removal only when no construction tool is active at all (mirrors the
        // 0.2.0 "Delete only in Idle" rule for buildings) -- never mid-route-preview.
        // Safe to check independently of PlacementController's own Delete handling:
        // a cell is either building-occupied or road-occupied, never both (both
        // validators cross-check the other), so only one of the two ever actually
        // matches a given hovered cell.
        private void UpdateRemoval()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null || !keyboard[_removeKey].wasPressedThisFrame)
            {
                return;
            }

            TryRemoveHoveredSegment();
        }

        private void TryRemoveHoveredSegment()
        {
            if (_coordinator.ActiveMode != ConstructionToolCoordinator.ConstructionToolMode.None)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            var hoveredWorld = _worldInteraction.HoveredWorldPosition;
            if (!hoveredCell.HasValue || !hoveredWorld.HasValue || !_state.TryFindIslandIndex(hoveredCell.Value, out var islandIndex))
            {
                return;
            }

            var graph = _state.GetGraph(islandIndex);
            if (!RoadSegmentPicker.TryFindNearestIncidentSegment(hoveredCell.Value, hoveredWorld.Value, graph, _worldInteraction.Grid, out var segmentId))
            {
                return;
            }

            var command = new RemoveRoadCommand(_localPlayerId, GameTick.Zero, segmentId);
            if (_commandHandler.Handle(command, out _))
            {
                RoadRuntimeMeshRefresh.RefreshAffectedIslands(_state, _networkDisplays, _registry, _selectedRoadId, _worldInteraction.Grid);
            }
        }
    }
}
