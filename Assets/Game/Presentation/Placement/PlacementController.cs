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

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// The one MonoBehaviour that owns building selection, ghost preview, rotation,
    /// placement, and removal. Reads input via the same Keyboard/Mouse device-polling
    /// idiom as WorldInteractionController/StrategyCameraController (no .inputactions
    /// asset). Reuses WorldInteractionController's already-built WorldGrid/
    /// IWorldHeightSource/hover cell instead of re-raycasting. World state
    /// (ArchipelagoPlacementState) is built here from runtime-safe data
    /// (IslandSurfaceRuntimeData + Terrain), never from any Varynth.Tooling.Editor type.
    /// </summary>
    public sealed class PlacementController : MonoBehaviour
    {
        private enum PlacementMode
        {
            Idle,
            Placing
        }

        [SerializeField] private WorldInteractionController _worldInteraction;
        [SerializeField] private IslandSurfaceRuntimeData[] _islandSurfaceData;
        [SerializeField] private GridDisplay[] _placementGrids;
        [SerializeField] private PlacementGhostDisplay _ghost;
        [SerializeField] private PrototypeBuildingVisualCatalog _visualCatalog;
        [SerializeField] private Transform _placedBuildingsRoot;
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _productionBlockButton;
        [SerializeField] private Button _publicBuildingButton;
        [SerializeField] private Key _rotateKey = Key.R;
        [SerializeField] private Key _removeKey = Key.Delete;

        private PlacementMode _mode = PlacementMode.Idle;
        private ArchipelagoPlacementState _state;
        private ContentRegistry<BuildingDefinition> _registry;
        private BuildingPlacementCommandHandler _commandHandler;
        private PlayerId _localPlayerId;
        private ContentId _selectedDefinitionId;
        private BuildingRotation _rotation = BuildingRotation.Deg0;
        private int _hoveredIslandIndex = -1;
        private readonly Dictionary<BuildingInstanceId, GameObject> _instanceGameObjects = new Dictionary<BuildingInstanceId, GameObject>();
        private (GridCoordinate cell, BuildingRotation rotation, ContentId definitionId, bool isValid)? _lastGhostState;

        public ArchipelagoPlacementState State => _state;
        public ContentRegistry<BuildingDefinition> Registry => _registry;
        public bool IsPlacing => _mode == PlacementMode.Placing;

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

            _commandHandler = new BuildingPlacementCommandHandler(_state, _registry);
            _localPlayerId = PlayerId.NewId();

            if (_houseButton != null) _houseButton.onClick.AddListener(() => SelectBuilding("bld.prototype.house"));
            if (_productionBlockButton != null) _productionBlockButton.onClick.AddListener(() => SelectBuilding("bld.prototype.production_block"));
            if (_publicBuildingButton != null) _publicBuildingButton.onClick.AddListener(() => SelectBuilding("bld.prototype.public_building"));

            HideAllPlacementGrids();
            if (_ghost != null) _ghost.Hide();
        }

        private void Update()
        {
            UpdateBuildingSelectionHotkeys();
            UpdateRotation();
            UpdateHoveredIslandAndGrids();
            UpdateGhost();
            UpdatePlaceOrCancel();
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
            _mode = PlacementMode.Placing;
            _lastGhostState = null;
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
        private void UpdateHoveredIslandAndGrids()
        {
            if (_mode != PlacementMode.Placing)
            {
                if (_hoveredIslandIndex != -1)
                {
                    HideAllPlacementGrids();
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
            if (_placementGrids == null)
            {
                return;
            }

            for (var i = 0; i < _placementGrids.Length; i++)
            {
                if (_placementGrids[i] != null)
                {
                    _placementGrids[i].SetVisible(i == _hoveredIslandIndex);
                }
            }
        }

        private void HideAllPlacementGrids()
        {
            if (_placementGrids == null)
            {
                return;
            }

            foreach (var grid in _placementGrids)
            {
                if (grid != null)
                {
                    grid.SetVisible(false);
                }
            }
        }

        private void UpdateGhost()
        {
            if (_ghost == null)
            {
                return;
            }

            if (_mode != PlacementMode.Placing || _hoveredIslandIndex == -1)
            {
                _ghost.Hide();
                _lastGhostState = null;
                return;
            }

            var hoveredCell = _worldInteraction.HoveredCell;
            if (!hoveredCell.HasValue || !_registry.TryGet(_selectedDefinitionId, out var definition))
            {
                _ghost.Hide();
                _lastGhostState = null;
                return;
            }

            var cell = hoveredCell.Value;
            var validation = _state.ValidatePlacementAt(_selectedDefinitionId, cell, _rotation, _registry);

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

        private void UpdatePlaceOrCancel()
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
                CancelPlacement();
                return;
            }

            if (mouse.leftButton.wasPressedThisFrame)
            {
                TryPlaceAtHoveredCell();
            }
        }

        private void CancelPlacement()
        {
            _mode = PlacementMode.Idle;
            if (_ghost != null) _ghost.Hide();
            _lastGhostState = null;
            HideAllPlacementGrids();
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

        // Removal only in Idle (adjustment 5) -- never while actively placing, so
        // Delete can't accidentally remove an existing building mid-placement.
        private void UpdateRemoval()
        {
            if (_mode != PlacementMode.Idle)
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
