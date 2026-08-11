using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Placement
{
    // Adjustment 6 regression guard: BuildingPlacementCommandHandler is the ONLY type
    // exercised here with command types. ArchipelagoPlacementStateTests (separate
    // fixture) proves the state itself works with zero ISimulationCommand usage.
    public class BuildingPlacementCommandHandlerTests
    {
        private GameObject _islandGo;
        private UnityEngine.Terrain _island;
        private ContentRegistry<BuildingDefinition> _registry;
        private ArchipelagoPlacementState _state;
        private BuildingPlacementCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var data = new TerrainData { heightmapResolution = 33, size = new Vector3(40f, 40f, 40f) };
            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = 0.5f;
            data.SetHeights(0, 0, heights);

            _islandGo = new GameObject("Island");
            _island = _islandGo.AddComponent<UnityEngine.Terrain>();
            _island.terrainData = data;
            _islandGo.transform.position = new Vector3(0f, -15f, 0f);

            var grid = new WorldGrid(4f, Vector2.zero);
            var surfaceData = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[100];
            for (var i = 0; i < flags.Length; i++) flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            surfaceData.SetData(0, 0, 10, 10, flags);

            _state = new ArchipelagoPlacementState(grid);
            _state.AddIsland(surfaceData, _island);

            var definition = new BuildingDefinition(ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house");
            _registry = new ContentRegistry<BuildingDefinition>();
            _registry.Register(definition);

            _handler = new BuildingPlacementCommandHandler(_state, _registry);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_islandGo);
        }

        [Test]
        public void Handle_PlaceBuildingCommand_PlacesInstance()
        {
            var command = new PlaceBuildingCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0);

            var handled = _handler.Handle(command, out var instance, out var validation);

            Assert.IsTrue(handled, $"issues: {validation.Issues}");
            Assert.AreEqual(ContentId.Parse("bld.prototype.house"), instance.DefinitionId);
        }

        [Test]
        public void Handle_RemoveBuildingCommand_RemovesInstance()
        {
            var placeCommand = new PlaceBuildingCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0);
            _handler.Handle(placeCommand, out var placed, out _);

            var removeCommand = new RemoveBuildingCommand(PlayerId.NewId(), GameTick.Zero, placed.Id);
            var handled = _handler.Handle(removeCommand, out var removed);

            Assert.IsTrue(handled);
            Assert.AreEqual(placed.Id, removed.Id);
        }
    }
}
