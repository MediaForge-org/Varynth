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

        [Test]
        public void Handle_Batch_FullyValid_PlacesAllInDeterministicIdOrder()
        {
            var origins = new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0), new GridCoordinate(4, 0) };
            var command = new PlaceBuildingBatchCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, origins);

            _handler.Handle(command, out var placed, out var rejected);

            Assert.AreEqual(3, placed.Count);
            Assert.AreEqual(0, rejected.Count);
            // Instance-id order must match Origins order exactly (deterministic).
            for (var i = 0; i < placed.Count; i++)
            {
                Assert.AreEqual(origins[i], placed[i].Origin);
            }
            Assert.Less(placed[0].Id.Value, placed[1].Id.Value);
            Assert.Less(placed[1].Id.Value, placed[2].Id.Value);
        }

        [Test]
        public void Handle_Batch_PartiallyInvalid_PlacesValidSkipsInvalid()
        {
            // First place a real building at (2,0) so the batch's second origin collides.
            _handler.Handle(new PlaceBuildingCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 0), BuildingRotation.Deg0), out _, out _);

            var origins = new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0), new GridCoordinate(4, 0) };
            var command = new PlaceBuildingBatchCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, origins);

            _handler.Handle(command, out var placed, out var rejected);

            Assert.AreEqual(2, placed.Count, "The valid origins should place; the collided one should be skipped, not veto the whole batch.");
            Assert.AreEqual(1, rejected.Count);
            Assert.AreEqual(new GridCoordinate(2, 0), rejected[0].Origin);
        }
    }
}
