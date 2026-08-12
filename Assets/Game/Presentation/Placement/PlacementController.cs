using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.Data.Loading;
using Varynth.Presentation.Interaction;
using Varynth.Presentation.Visualization;
using Varynth.World.Placement;
using Varynth.World.Roads;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// The one MonoBehaviour that owns building selection, ghost preview, rotation,
    /// single/drag placement, and removal. Reads input via the same Keyboard/Mouse
    /// device-polling idiom as WorldInteractionController/StrategyCameraController
    /// (no .inputactions asset). Reuses WorldInteractionController's already-built
    /// WorldGrid/IWorldHeightSource/hover cell instead of re-raycasting. World state
    /// (ArchipelagoPlacementState) is built here from runtime-safe data
    /// (IslandSurfaceRuntimeData + Terrain), never from any Varynth.Tooling.Editor
    /// type. Tool activation/cancellation and Player Placement Grid visibility are
    /// arbitrated through ConstructionToolCoordinator, never by reaching into
    /// RoadPlacementController directly (Phase 2D).
    /// </summary>
    public sealed class PlacementController : MonoBehaviour, ConstructionToolCoordinator.IConstructionTool
    {
        private enum PlacementMode
        {
            Idle,
            Placing
        }

        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private IslandSurfaceRuntimeData[] _islandSurfaceData;
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
        private ArchipelagoPlacementState _state;
        private ContentRegistry<BuildingDefinition> _registry;
        private BuildingPlacementCommandHandler _commandHandler;
        private IRoadOccupancyQuery _roadOccupancy;
        private PlayerId _localPlayerId;
        private ContentId _selectedDefinitionId;
        private BuildingRotation _rotation = BuildingRotation.Deg0;
        private int _hoveredIslandIndex = -1;
        private readonly Dictionary<BuildingInstanceId, GameObject> _instanceGameObjects = new Dictionary<BuildingInstanceId, GameObject>();
        private (GridCoordinate cell, BuildingRotation rotation, ContentId definitionId, bool isValid)? _lastGhostState;
        private GridCoordinate? _dragStartCell;
        private GridCoordinate? _lastDragEnd;

        public ArchipelagoPlacementState State => _state;
        public ContentRegistry<BuildingDefinition> Registry => _registry;
        public bool IsPlacing => _mode == PlacementMode.Placing;

        public void Configure(ConstructionToolCoordinator coordinator)
        {
            _coordinator = coordinator;
            _coordinator.RegisterBuildingTool(this);
        }

        private void Awake()
        {
            _state = new ArchipelagoPlacementState(_worldInteraction.Grid);

            var terrains = _worldInteraction.Terrains ?? Array.Empty<UnityEngine.Terrain>();
            var surfaceData = _islandSurfaceData ?? Array.Empty<IslandSurfaceRuntimeData>();
            var islandCount = Mathf.Min(terrains.Length, surfaceData.Length);
            for (var i = 0; i < islandCount; i++)
            {
                if (terrains[i] != null && surfaceData[i] != null)
                {
                    _state.AddIsland(surfaceData[i], terrains[i]);
                }
            }

            var contentRoot = Path.Combine(Application.streamingAssetsPath, "Content", "Buildings");
            _registry = BuildingContentBootstrap.LoadRegistry(contentRoot);
            _localPlayerId = PlayerId.NewId();

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

            // Cross-wiring happens in Start (after every Awake ran) -- the two
            // world-state systems never reference each other directly; only the
            // small read-only query interface is composed here, by the one place
            // that legitimately knows about both. The same instance is used for
            // both the ghost-preview validation call and every command-application
            // TryPlace call, so preview and final placement never diverge.
            var roadController = FindFirstObjectByType<Varynth.Presentation.Roads.RoadPlacementController>();
            _roadOccupancy = roadController != null ? roadController.State : null;
            _commandHandler = new BuildingPlacementCommandHandler(_state, _registry, _roadOccupancy);
        }

        private void Update()
        {
            UpdateBuildingSelectionHotkeys();
            UpdateRotation();
            UpdateHoveredIslandAndGrids();
            UpdateGhostOrDragPreview();
            UpdatePlaceOrDragOrCancel();
            UpdateRemoval();
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
            if (hoveredCell.HasValue && _state.TryFindIslandIndex(hoveredCell.Value, out var index))
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
            if (_ghost == null)
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
            var validation = _state.ValidatePlacementAt(_selectedDefinitionId, cell, _rotation, _registry, _roadOccupancy);

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
            if (_dragPreview == null)
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
                var validation = _state.ValidatePlacementAt(_selectedDefinitionId, origin, _rotation, _registry, _roadOccupancy);
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
            var endCell = _worldInteraction.HoveredCell ?? _dragStartCell.Value;
            var effectiveWidth = _rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = _rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
            var origins = BuildingRepeatPlanner.PlanOrigins(_dragStartCell.Value, endCell, effectiveWidth, effectiveLength);

            var batch = new PlaceBuildingBatchCommand(_localPlayerId, GameTick.Zero, _selectedDefinitionId, _rotation, origins);
            _commandHandler.Handle(batch, out var placed, out _);
            foreach (var instance in placed)
            {
                SpawnPresentationForInstance(instance);
            }

            _dragStartCell = null;
            _lastDragEnd = null;
            if (_dragPreview != null) _dragPreview.Hide();
            _lastGhostState = null; // occupancy changed -- force ghost re-evaluation
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
            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue)
            {
                return;
            }

            var command = new PlaceBuildingCommand(_localPlayerId, GameTick.Zero, _selectedDefinitionId, hoveredCell.Value, _rotation);
            if (_commandHandler.Handle(command, out var instance, out _))
            {
                SpawnPresentationForInstance(instance);
                _lastGhostState = null; // occupancy changed under the cursor -- force re-evaluation
            }
        }

        private void SpawnPresentationForInstance(BuildingInstance instance)
        {
            if (!_registry.TryGet(instance.DefinitionId, out var definition))
            {
                return;
            }

            Mesh mesh = null;
            Material material = null;
            if (_visualCatalog != null)
            {
                _visualCatalog.TryGetVisual(definition.PrototypeVisualId, out mesh, out material);
            }

            var go = new GameObject($"Building_{instance.Id}");
            if (_placedBuildingsRoot != null)
            {
                go.transform.SetParent(_placedBuildingsRoot, false);
            }

            var meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;
            var meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = material;

            var effectiveWidth = instance.Rotation.SwapsWidthAndLength() ? definition.FootprintLength : definition.FootprintWidth;
            var effectiveLength = instance.Rotation.SwapsWidthAndLength() ? definition.FootprintWidth : definition.FootprintLength;
            var (position, rotation, scale) = PlacementPresentationMath.ComputeBuildingTransform(
                instance.Origin, effectiveWidth, effectiveLength, instance.Rotation, _worldInteraction.Grid, _worldInteraction.HeightSource);
            go.transform.SetPositionAndRotation(position, rotation);
            go.transform.localScale = scale;

            _instanceGameObjects[instance.Id] = go;
        }

        // Removal only when no construction tool is active at all (adjustment 5,
        // extended in Phase 2D to the shared coordinator's None mode) -- never while
        // actively placing/routing, so Delete can't accidentally remove an existing
        // building mid-placement. Safe to check independently of
        // RoadPlacementController's own Delete handling: a cell is either building-
        // or road-occupied, never both.
        private void UpdateRemoval()
        {
            if (_mode != PlacementMode.Idle
                || (_coordinator != null && _coordinator.ActiveMode != ConstructionToolCoordinator.ConstructionToolMode.None))
            {
                return;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null || !keyboard[_removeKey].wasPressedThisFrame)
            {
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue || !_state.TryGetOccupantAt(hoveredCell.Value, out var occupant))
            {
                return;
            }

            var command = new RemoveBuildingCommand(_localPlayerId, GameTick.Zero, occupant);
            if (_commandHandler.Handle(command, out var removed))
            {
                if (_instanceGameObjects.TryGetValue(removed.Id, out var go))
                {
                    if (go != null)
                    {
                        Destroy(go);
                    }

                    _instanceGameObjects.Remove(removed.Id);
                }
            }
        }
    }
}
