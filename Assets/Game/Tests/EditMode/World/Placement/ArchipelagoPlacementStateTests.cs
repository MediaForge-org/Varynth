using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Placement
{
    // Regression guard for adjustment 1: this fixture builds ArchipelagoPlacementState
    // purely from IslandSurfaceRuntimeData + UnityEngine.Terrain -- runtime-safe types,
    // never any Varynth.Tooling.Editor type (that assembly is Editor-only and would
    // not exist in a Player build). This test fixture deliberately does not reference
    // Varynth.Tooling.Editor at all.
    public class ArchipelagoPlacementStateTests
    {
        private GameObject _islandAGo;
        private GameObject _islandBGo;
        private UnityEngine.Terrain _islandA;
        private UnityEngine.Terrain _islandB;
        private WorldGrid _grid;
        private ContentRegistry<BuildingDefinition> _registry;

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, Vector2.zero);
            _islandA = CreateFlatTerrain("IslandA", new Vector3(0f, -15f, 0f), 40f, out _islandAGo);
            _islandB = CreateFlatTerrain("IslandB", new Vector3(200f, -15f, 200f), 40f, out _islandBGo);

            var houseDefinition = new BuildingDefinition(
                ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house");
            _registry = new ContentRegistry<BuildingDefinition>();
            _registry.Register(houseDefinition);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_islandAGo);
            Object.DestroyImmediate(_islandBGo);
        }

        private static UnityEngine.Terrain CreateFlatTerrain(string name, Vector3 position, float worldSize, out GameObject go)
        {
            var data = new TerrainData { heightmapResolution = 33, size = new Vector3(worldSize, 40f, worldSize) };
            var heights = new float[33, 33];
            for (var y = 0; y < 33; y++)
            for (var x = 0; x < 33; x++)
                heights[y, x] = 0.5f; // -> world height 5 (well above sea level 0)
            data.SetHeights(0, 0, heights);

            go = new GameObject(name);
            var terrain = go.AddComponent<UnityEngine.Terrain>();
            terrain.terrainData = data;
            go.transform.position = position;
            return terrain;
        }

        private static IslandSurfaceRuntimeData CreateAllBuildableRuntimeData(GridCoordinate origin, int width, int height)
        {
            var data = ScriptableObject.CreateInstance<IslandSurfaceRuntimeData>();
            var flags = new byte[width * height];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = (byte)(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);
            }
            data.SetData(origin.X, origin.Z, width, height, flags);
            return data;
        }

        private ArchipelagoPlacementState BuildState()
        {
            var state = new ArchipelagoPlacementState(_grid);
            state.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(0, 0), 10, 10), _islandA);
            state.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(50, 50), 10, 10), _islandB);
            return state;
        }

        [Test]
        public void TryPlace_OnIslandA_Succeeds()
        {
            var state = BuildState();

            var placed = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out var instance, out var validation);

            Assert.IsTrue(placed, $"Expected success, issues: {validation.Issues}");
            Assert.IsNotNull(instance);
        }

        [Test]
        public void TryPlace_OnIslandB_Succeeds_IndependentlyOfIslandA()
        {
            var state = BuildState();

            var placedA = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out _, out _);
            var placedB = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(52, 52), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out _, out var validationB);

            Assert.IsTrue(placedA);
            Assert.IsTrue(placedB, $"Island B placement should succeed independently, issues: {validationB.Issues}");
        }

        [Test]
        public void TryPlace_OpenWater_BetweenIslands_Rejected()
        {
            var state = BuildState();

            var placed = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(25, 25), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out _, out var validation);

            Assert.IsFalse(placed);
            Assert.IsTrue((validation.Issues & PlacementIssue.OutsideSurfaceMap) != 0);
        }

        [Test]
        public void TryPlace_SameFootprintTwice_SecondRejected()
        {
            var state = BuildState();
            state.TryPlace(ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0, PlayerId.NewId(), _registry, out _, out _);

            var placedAgain = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out _, out var validation);

            Assert.IsFalse(placedAgain);
            Assert.IsTrue((validation.Issues & PlacementIssue.AlreadyOccupied) != 0);
        }

        [Test]
        public void TryRemove_FreesOccupancy_AllowsRePlacement()
        {
            var state = BuildState();
            state.TryPlace(ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0, PlayerId.NewId(), _registry, out var instance, out _);

            var removed = state.TryRemove(instance.Id, out var removedInstance);
            var placedAgain = state.TryPlace(
                ContentId.Parse("bld.prototype.house"), new GridCoordinate(2, 2), BuildingRotation.Deg0,
                PlayerId.NewId(), _registry, out _, out var validation);

            Assert.IsTrue(removed);
            Assert.AreEqual(instance.Id, removedInstance.Id);
            Assert.IsTrue(placedAgain, $"Expected re-placement to succeed, issues: {validation.Issues}");
        }

        [Test]
        public void TryRemove_NonexistentId_ReturnsFalse()
        {
            var state = BuildState();

            var removed = state.TryRemove(BuildingInstanceId.FromRaw(999), out var removedInstance);

            Assert.IsFalse(removed);
            Assert.IsNull(removedInstance);
        }

        [Test]
        public void SequentialPlacements_AssignSequentialInstanceIds()
        {
            var state = BuildState();

            state.TryPlace(ContentId.Parse("bld.prototype.house"), new GridCoordinate(0, 0), BuildingRotation.Deg0, PlayerId.NewId(), _registry, out var first, out _);
            state.TryPlace(ContentId.Parse("bld.prototype.house"), new GridCoordinate(4, 0), BuildingRotation.Deg0, PlayerId.NewId(), _registry, out var second, out _);

            Assert.Greater(second.Id.Value, first.Id.Value);
        }
    }
}
