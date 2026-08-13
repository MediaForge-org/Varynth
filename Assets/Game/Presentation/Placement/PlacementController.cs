using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Boundary;
using Varynth.Core.Simulation.Building;
using Varynth.Data.Loading;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Visualization;
using Varynth.World.Placement;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// The one MonoBehaviour that owns building selection, ghost preview, rotation,
    /// single/drag placement, and removal. Reads input via the same Keyboard/Mouse
    /// device-polling idiom as WorldInteractionController/StrategyCameraController
    /// (no .inputactions asset). Reuses WorldInteractionController's already-built
    /// WorldGrid/IWorldHeightSource/hover cell instead of re-raycasting.
    ///
    /// Phase 2E: no longer owns/constructs ArchipelagoPlacementState itself -- the
    /// single authoritative instance lives inside ManagedSimulation, owned by
    /// UnitySimulationDriver (found here in Start(), same idiom already used for
    /// ConstructionToolCoordinatorHost). Ghost/drag preview still validates locally
    /// via ISimulationPlacementQueries (read-only, never mutates). Confirmed
    /// placement/removal goes through ISimulation.Submit(...) -- the command is
    /// queued, not applied synchronously; the resulting GameObject appears once
    /// UnitySimulationDriver's next tick(s) run and this controller's snapshot diff
    /// notices the new/removed BuildingRenderSnapshot entry. Still reads
    /// ContentRegistry&lt;BuildingDefinition&gt; directly -- content Definitions are
    /// read-only reference data, not authoritative simulation state, so Presentation
    /// reading them directly does not cross the simulation boundary.
    /// </summary>
    public sealed class PlacementController : MonoBehaviour, ConstructionToolCoordinator.IConstructionTool
    {
        private enum PlacementMode
        {
            Idle,
            Placing
        }

        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private PlacementGhostDisplay _ghost;
        [SerializeField] private DragPreviewDisplay _dragPreview;
        [SerializeField] private PrototypeBuildingVisualCatalog _visualCatalog;
        [SerializeField] private Transform _placedBuildingsRoot;
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _productionBlockButton;
        [SerializeField] private Button _publicBuildingButton;
        [SerializeField] private Key _rotateKey = Key.R;
        [SerializeField] private Key _removeKey = Key.Delete;

        private ConstructionToolCoordinator _coordinator;
        private PlacementMode _mode = PlacementMode.Idle;
        private ContentRegistry<BuildingDefinition> _registry;
        private ISimulation _simulation;
        private ISimulationPlacementQueries _placementQueries;
        private ContentId _selectedDefinitionId;
        private BuildingRotation _rotation = BuildingRotation.Deg0;
        private int _hoveredIslandIndex = -1;
        private readonly Dictionary<BuildingInstanceId, GameObject> _instanceGameObjects = new Dictionary<BuildingInstanceId, GameObject>();
        private (GridCoordinate cell, BuildingRotation rotation, ContentId definitionId, bool isValid)? _lastGhostState;
        private GridCoordinate? _dragStartCell;
        private GridCoordinate? _lastDragEnd;
        private int _lastAppliedBuildingStateVersion = int.MinValue;

        public ContentRegistry<BuildingDefinition> Registry => _registry;
        public bool IsPlacing => _mode == PlacementMode.Placing;

        public void Configure(ConstructionToolCoordinator coordinator)
        {
            _coordinator = coordinator;
            _coordinator.RegisterBuildingTool(this);
        }

        private void Awake()
        {
            var contentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Buildings");
            _registry = BuildingContentBootstrap.LoadRegistry(contentRoot);

            if (_houseButton != null) _houseButton.onClick.AddListener(() => SelectBuilding("bld.prototype.house"));
            if (_productionBlockButton != null) _productionBlockButton.onClick.AddListener(() => SelectBuilding("bld.prototype.production_block"));
            if (_publicBuildingButton != null) _publicBuildingButton.onClick.AddListener(() => SelectBuilding("bld.prototype.public_building"));

            if (_ghost != null) _ghost.Hide();
            if (_dragPreview != null) _dragPreview.Hide();
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

            // Cross-wiring happens in Start (after every Awake ran). Phase 2E: both
            // PlacementController and RoadPlacementController now only ever reach for
            // the shared UnitySimulationDriver -- never for each other directly.
            var driver = FindFirstObjectByType<UnitySimulationDriver>();
            if (driver != null)
            {
                _simulation = driver.Simulation;
                _placementQueries = driver.Simulation;
            }
        }

        private void Update()
        {
            SyncPresentationWithSnapshot();
            UpdateBuildingSelectionHotkeys();
            UpdateRotation();
            UpdateHoveredIslandAndGrids();
            UpdateGhostOrDragPreview();
            UpdatePlaceOrDragOrCancel();
            UpdateRemoval();
        }

        /// <summary>
        /// Reconciles spawned GameObjects against the latest BuildingRenderSnapshot
        /// list -- the sole place GameObjects are spawned/destroyed. Gated on
        /// BuildingStateVersion (Phase 2E point 5), not Tick, so a tick with no real
        /// building change costs only the version-compare + snapshot-reference read.
        /// GameObjects are a cached rendering of the snapshot, never the source of
        /// truth (Phase 2E point 26) -- removal below no longer destroys anything
        /// itself, it only submits a command and waits for this method to notice.
        /// </summary>
        private void SyncPresentationWithSnapshot()
        {
            if (_simulation == null)
            {
                return;
            }

            var snapshot = _simulation.GetSnapshot();
            if (snapshot.BuildingStateVersion == _lastAppliedBuildingStateVersion)
            {
                return;
            }

            _lastAppliedBuildingStateVersion = snapshot.BuildingStateVersion;
            _lastGhostState = null; // occupancy may have changed -- force ghost re-evaluation

            var seen = new HashSet<BuildingInstanceId>();
            foreach (var entry in snapshot.Buildings)
            {
                seen.Add(entry.InstanceId);
                if (!_instanceGameObjects.ContainsKey(entry.InstanceId))
                {
                    SpawnPresentationForSnapshotEntry(entry);
                }
            }

            List<BuildingInstanceId> toRemove = null;
            foreach (var kvp in _instanceGameObjects)
            {
                if (!seen.Contains(kvp.Key))
                {
                    (toRemove ??= new List<BuildingInstanceId>()).Add(kvp.Key);
                }
            }

            if (toRemove != null)
            {
                foreach (var id in toRemove)
                {
                    if (_instanceGameObjects.TryGetValue(id, out var go) && go != null)
                    {
                        Destroy(go);
                    }

                    _instanceGameObjects.Remove(id);
                }
            }
        }

        private void UpdateBuildingSelectionHotkeys()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.digit1Key.wasPressedThisFrame) SelectBuilding("bld.prototype.house");
            else if (keyboard.digit2Key.wasPressedThisFrame) SelectBuilding("bld.prototype.production_block");
            else if (keyboard.digit3Key.wasPressedThisFrame) SelectBuilding("bld.prototype.public_building");
        }

        public void SelectBuilding(string definitionIdText)
        {
            if (!ContentId.TryParse(definitionIdText, out var id) || !_registry.TryGet(id, out _))
            {
                return;
            }

            _selectedDefinitionId = id;
            _rotation = BuildingRotation.Deg0;
            _coordinator?.RequestActivate(ConstructionToolCoordinator.ConstructionToolMode.Building);
            _mode = PlacementMode.Placing;
            _lastGhostState = null;
            _dragStartCell = null;
            _lastDragEnd = null;
            // Force the next UpdateHoveredIslandAndGrids() call to re-evaluate and
            // re-issue RequestPlacementGridVisibility even if the real hovered island
            // happens to equal this field's stale value from a previous session --
            // the coordinator already force-hid everything in RequestActivate above,
            // so this cache must not skip re-showing the correct one.
            _hoveredIslandIndex = -2;
        }

        private void UpdateRotation()
        {
            if (_mode != PlacementMode.Placing)
            {
                return;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !keyboard[_rotateKey].wasPressedThisFrame)
            {
                return;
            }

            _rotation = _rotation.Next();
        }

        // One Player Placement Grid per island (adjustment 2): only the currently
        // hovered island's grid is ever visible, all others stay hidden, and outside
        // Placing mode every grid is hidden -- independent of the debug Grid (G key).
        // Visibility is requested through the shared ConstructionToolCoordinator
        // (Phase 2D) rather than toggled directly here.
        private void UpdateHoveredIslandAndGrids()
        {
            if (_mode != PlacementMode.Placing)
            {
                if (_hoveredIslandIndex != -1)
                {
                    _coordinator?.RequestPlacementGridVisibility(-1);
                    _hoveredIslandIndex = -1;
                }

                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            var newIndex = -1;
            if (hoveredCell.HasValue && _placementQueries != null && _placementQueries.TryFindIslandIndex(hoveredCell.Value, out var index))
            {
                newIndex = index;
            }

            if (newIndex == _hoveredIslandIndex)
            {
                return;
            }

            _hoveredIslandIndex = newIndex;
            _coordinator?.RequestPlacementGridVisibility(_hoveredIslandIndex);
        }

        // Branches only on definition.PlacementBehavior, never on id/name (Phase 2D).
        private void UpdateGhostOrDragPreview()
        {
            if (_mode != PlacementMode.Placing || _hoveredIslandIndex == -1 || !_registry.TryGet(_selectedDefinitionId, out var definition))
            {
                _ghost?.Hide();
                _dragPreview?.Hide();
                _lastGhostState = null;
                return;
            }

            if (definition.PlacementBehavior == BuildingPlacementBehavior.DragRepeat && _dragStartCell.HasValue)
            {
                _ghost?.Hide();
                UpdateDragPreview(definition);
                return;
            }

            _dragPreview?.Hide();
            UpdateSingleGhost(definition);
        }

        private void UpdateSingleGhost(BuildingDefinition definition)
        {
            if (_ghost == null || _placementQueries == null)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                _ghost.Hide();
                _lastGhostState = null;
                return;
            }

            var cell = hoveredCell.Value;
            var validation = _placementQueries.ValidateBuildingPlacement(_selectedDefinitionId, cell, _rotation);

            var key = (cell, _rotation, _selectedDefinitionId, validation.IsValid);
            if (_lastGhostState.HasValue && _lastGhostState.Value.Equals(key))
            {
                return; // no meaningful change -- no per-frame mesh rebuild
            }

            _lastGhostState = key;

            var effectiveWidth = _rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = _rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
            var cells = BuildingFootprint.GetOccupiedCells(cell, definition.FootprintWidth, definition.FootprintLength, _rotation);

            Mesh mesh = null;
            if (_visualCatalog != null)
            {
                _visualCatalog.TryGetVisual(definition.PrototypeVisualId, out mesh, out _);
            }

            var (position, rotation, scale) = PlacementPresentationMath.ComputeBuildingTransform(
                cell, effectiveWidth, effectiveLength, _rotation, _worldInteraction.Grid, _worldInteraction.HeightSource);

            _ghost.Show(cells, _worldInteraction.Grid, _worldInteraction.HeightSource, mesh, position, rotation, scale, validation.IsValid);
        }

        private void UpdateDragPreview(BuildingDefinition definition)
        {
            if (_dragPreview == null || _placementQueries == null)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                return;
            }

            if (_lastDragEnd.HasValue && _lastDragEnd.Value.Equals(hoveredCell.Value))
            {
                return; // no meaningful change -- no per-frame mesh rebuild
            }

            _lastDragEnd = hoveredCell.Value;

            var effectiveWidth = _rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = _rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
            var origins = BuildingRepeatPlanner.PlanOrigins(_dragStartCell.Value, hoveredCell.Value, effectiveWidth, effectiveLength);

            var plans = new List<(GridCoordinate, bool)>(origins.Count);
            foreach (var origin in origins)
            {
                var validation = _placementQueries.ValidateBuildingPlacement(_selectedDefinitionId, origin, _rotation);
                plans.Add((origin, validation.IsValid));
            }

            _dragPreview.Show(plans, definition, _rotation, _worldInteraction.Grid, _worldInteraction.HeightSource);
        }

        private void UpdatePlaceOrDragOrCancel()
        {
            if (_mode != PlacementMode.Placing)
            {
                return;
            }

            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            if (keyboard == null || mouse == null)
            {
                return;
            }

            if (keyboard.escapeKey.wasPressedThisFrame || mouse.rightButton.wasPressedThisFrame)
            {
                _coordinator?.RequestDeactivate();
                return;
            }

            if (!_registry.TryGet(_selectedDefinitionId, out var definition))
            {
                return;
            }

            if (definition.PlacementBehavior == BuildingPlacementBehavior.Single)
            {
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    TryPlaceAtHoveredCell();
                }

                return;
            }

            // DragRepeat: press sets the start cell, release commits the whole batch
            // -- LMB drag is never 50 separate simulated clicks. No early return after
            // setting the start: a plain click's press and release can land in the
            // same input-processed frame (e.g. a fast click, or simulated input in
            // tests), and that must still commit a single (zero-length-drag) building,
            // not silently swallow the release.
            if (mouse.leftButton.wasPressedThisFrame && !_dragStartCell.HasValue)
            {
                var hoveredCell = _worldInteraction.HoveredCell;
                if (hoveredCell.HasValue)
                {
                    _dragStartCell = hoveredCell;
                    _lastDragEnd = null;
                }
            }

            if (mouse.leftButton.wasReleasedThisFrame && _dragStartCell.HasValue)
            {
                CommitDrag(definition);
            }
        }

        private void CommitDrag(BuildingDefinition definition)
        {
            if (_simulation != null)
            {
                var endCell = _worldInteraction.HoveredCell ?? _dragStartCell.Value;
                var effectiveWidth = _rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
                var effectiveLength = _rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
                var origins = BuildingRepeatPlanner.PlanOrigins(_dragStartCell.Value, endCell, effectiveWidth, effectiveLength);

                var batch = new PlaceBuildingBatchCommand(_simulation.LocalPlayerId, _simulation.CurrentTick, _selectedDefinitionId, _rotation, origins);
                _simulation.Submit(batch);
            }

            _dragStartCell = null;
            _lastDragEnd = null;
            if (_dragPreview != null) _dragPreview.Hide();
        }

        public void CancelTool()
        {
            _mode = PlacementMode.Idle;
            if (_ghost != null) _ghost.Hide();
            if (_dragPreview != null) _dragPreview.Hide();
            _lastGhostState = null;
            _dragStartCell = null;
            _lastDragEnd = null;
            _hoveredIslandIndex = -1;
        }

        private void TryPlaceAtHoveredCell()
        {
            if (_simulation == null)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                return;
            }

            var command = new PlaceBuildingCommand(_simulation.LocalPlayerId, _simulation.CurrentTick, _selectedDefinitionId, hoveredCell.Value, _rotation);
            _simulation.Submit(command);
        }

        private void SpawnPresentationForSnapshotEntry(BuildingRenderSnapshot entry)
        {
            if (!_registry.TryGet(entry.DefinitionId, out var definition))
            {
                return;
            }

            Mesh mesh = null;
            Material material = null;
            if (_visualCatalog != null)
            {
                _visualCatalog.TryGetVisual(definition.PrototypeVisualId, out mesh, out material);
            }

            var go = new GameObject($"Building_{entry.InstanceId}");
            if (_placedBuildingsRoot != null)
            {
                go.transform.SetParent(_placedBuildingsRoot, false);
            }

            var meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
            var meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = material;

            var effectiveWidth = entry.Rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = entry.Rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
            var (position, rotation, scale) = PlacementPresentationMath.ComputeBuildingTransform(
                entry.Origin, effectiveWidth, effectiveLength, entry.Rotation, _worldInteraction.Grid, _worldInteraction.HeightSource);
            go.transform.SetPositionAndRotation(position, rotation);
            go.transform.localScale = scale;

            _instanceGameObjects[entry.InstanceId] = go;
        }

        // Removal only when no construction tool is active at all (adjustment 5,
        // extended in Phase 2D to the shared coordinator's None mode) -- never while
        // actively placing/routing, so Delete can't accidentally remove an existing
        // building mid-placement. Safe to check independently of
        // RoadPlacementController's own Delete handling: a cell is either building-
        // or road-occupied, never both. Phase 2E: only submits the command -- the
        // GameObject disappears via SyncPresentationWithSnapshot once the removal
        // actually lands, never destroyed directly here.
        private void UpdateRemoval()
        {
            if (_mode != PlacementMode.Idle
                || (_coordinator != null && _coordinator.ActiveMode != ConstructionToolCoordinator.ConstructionToolMode.None)
                || _simulation == null || _placementQueries == null)
            {
                return;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !keyboard[_removeKey].wasPressedThisFrame)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue || !_placementQueries.TryGetOccupantAt(hoveredCell.Value, out var occupant))
            {
                return;
            }

            var command = new RemoveBuildingCommand(_simulation.LocalPlayerId, _simulation.CurrentTick, occupant);
            _simulation.Submit(command);
        }
    }
}
