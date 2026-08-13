using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Boundary;
using Varynth.Core.Simulation.Road;
using Varynth.Data.Loading;
using Varynth.Presentation.Interaction;
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
    ///
    /// Phase 2E: no longer owns/constructs RoadNetworkState -- the single
    /// authoritative instance lives inside ManagedSimulation, owned by
    /// UnitySimulationDriver (found here in Start(), same idiom as
    /// ConstructionToolCoordinatorHost). Route preview reads through
    /// ISimulationRoadQueries (read-only). Confirmed build/remove goes through
    /// ISimulation.Submit(...) -- the mesh only updates once the command actually
    /// lands and RoadRuntimeMeshRefresh notices GetRoadStateVersion changed (never a
    /// live RoadGraph read -- see RoadRuntimeMeshRefresh).
    /// </summary>
    public sealed class RoadPlacementController : MonoBehaviour, ConstructionToolCoordinator.IConstructionTool
    {
        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private RoadNetworkDisplay[] _networkDisplays;
        [SerializeField] private RoadPreviewDisplay _preview;
        [SerializeField] private Button _roadToolButton;
        [SerializeField] private Key _removeKey = Key.Delete;
        [SerializeField] private Key _selectToolKey = Key.Digit4;

        private ConstructionToolCoordinator _coordinator;
        private ContentRegistry<RoadDefinition> _registry;
        private ISimulation _simulation;
        private ISimulationRoadQueries _roadQueries;
        private ContentId _selectedRoadId;
        private GridCoordinate? _startCell;
        private IReadOnlyList<GridCoordinate> _currentPath;
        private bool _currentPathValid;
        private GridCoordinate? _lastPreviewedEnd;
        private int _hoveredIslandIndex = -1;
        private int[] _lastAppliedMeshStateVersions = System.Array.Empty<int>();

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
            var contentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Roads");
            _registry = RoadContentBootstrap.LoadRegistry(contentRoot);

            if (_registry.TryGet(ContentId.Parse("road.prototype.basic"), out _))
            {
                _selectedRoadId = ContentId.Parse("road.prototype.basic");
            }

            if (_roadToolButton != null) _roadToolButton.onClick.AddListener(SelectRoadTool);

            if (_preview != null) _preview.Hide();

            _lastAppliedMeshStateVersions = new int[_networkDisplays?.Length ?? 0];
            for (var i = 0; i < _lastAppliedMeshStateVersions.Length; i++)
            {
                _lastAppliedMeshStateVersions[i] = int.MinValue;
            }
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

            var driver = FindFirstObjectByType<UnitySimulationDriver>();
            if (driver != null)
            {
                _simulation = driver.Simulation;
                _roadQueries = driver.Simulation;
            }
        }

        private void Update()
        {
            SyncMeshFromSnapshot();

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

        /// <summary>See RoadRuntimeMeshRefresh -- reconstructs a disposable RoadGraph replica from the snapshot per changed island, never reads a live authoritative graph.</summary>
        private void SyncMeshFromSnapshot()
        {
            if (_simulation == null || _roadQueries == null)
            {
                return;
            }

            var snapshot = _simulation.GetSnapshot();
            RoadRuntimeMeshRefresh.RefreshFromSnapshot(
                snapshot, _roadQueries, _lastAppliedMeshStateVersions, _networkDisplays, _registry, _selectedRoadId, _worldInteraction.Grid, _worldInteraction.HeightSource);
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
            if (hoveredCell.HasValue && _roadQueries.TryFindIslandIndex(hoveredCell.Value, out var index))
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
            if (_preview == null || !_startCell.HasValue || _roadQueries == null)
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

            _currentPathValid = _roadQueries.TryFindRoadRoute(_selectedRoadId, _startCell.Value, hoveredCell.Value, out _currentPath);
            _registry.TryGet(_selectedRoadId, out var definition);
            _preview.Show(_currentPath, definition, _worldInteraction.Grid, _worldInteraction.HeightSource, _currentPathValid);
        }

        private void UpdateConfirmOrCancel()
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            if (keyboard == null || mouse == null || _simulation == null)
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
                var command = new BuildRoadCommand(_simulation.LocalPlayerId, _simulation.CurrentTick, _selectedRoadId, _currentPath);
                _simulation.Submit(command);
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
            if (_simulation == null || _roadQueries == null || _coordinator.ActiveMode != ConstructionToolCoordinator.ConstructionToolMode.None)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            var hoveredWorld = _worldInteraction.HoveredWorldPosition;
            if (!hoveredCell.HasValue || !hoveredWorld.HasValue || !_roadQueries.TryFindIslandIndex(hoveredCell.Value, out var islandIndex))
            {
                return;
            }

            // Disambiguating which segment at a busy node the cursor is closest to
            // needs real graph connectivity -- reconstructed here as a disposable
            // replica from the current snapshot (Phase 2E point 3), never a live
            // authoritative RoadGraph reference.
            var islandId = _roadQueries.GetIslandId(islandIndex);
            var replica = new RoadGraph();
            foreach (var segment in _simulation.GetSnapshot().Roads)
            {
                if (segment.Island == islandId)
                {
                    replica.AddSegment(segment.SegmentId, segment.DefinitionId, segment.From, segment.To, segment.Direction, segment.Owner);
                }
            }

            if (!RoadSegmentPicker.TryFindNearestIncidentSegment(hoveredCell.Value, hoveredWorld.Value, replica, _worldInteraction.Grid, out var segmentId))
            {
                return;
            }

            var command = new RemoveRoadCommand(_simulation.LocalPlayerId, _simulation.CurrentTick, segmentId);
            _simulation.Submit(command);
        }
    }
}
