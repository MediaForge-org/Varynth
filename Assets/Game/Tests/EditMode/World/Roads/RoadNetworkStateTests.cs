using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Roads
{
    // Mirrors ArchipelagoPlacementStateTests' fixture shape exactly -- built purely
    // from IslandSurfaceRuntimeData + UnityEngine.Terrain, never any
    // Varynth.Tooling.Editor type.
    public class RoadNetworkStateTests
    {
        private GameObject _islandAGo;
        private GameObject _islandBGo;
        private UnityEngine.Terrain _islandA;
        private UnityEngine.Terrain _islandB;
        private WorldGrid _grid;
        private ContentRegistry<RoadDefinition> _registry;

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, Vector2.zero);
            _islandA = CreateFlatTerrain("IslandA", new Vector3(0f, -15f, 0f), 40f, out _islandAGo);
            _islandB = CreateFlatTerrain("IslandB", new Vector3(200f, -15f, 200f), 40f, out _islandBGo);

            var roadDefinition = new RoadDefinition(ContentId.Parse("road.prototype.basic"), LocalizationKey.Parse("road.name"), "road");
            _registry = new ContentRegistry<RoadDefinition>();
            _registry.Register(roadDefinition);
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
                heights[y, x] = 0.5f;
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

        private RoadNetworkState BuildState()
        {
            var state = new RoadNetworkState(_grid);
            state.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(0, 0), 10, 10), _islandA);
            state.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(50, 50), 10, 10), _islandB);
            return state;
        }

        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        [Test]
        public void BuildPath_OnIslandA_Succeeds()
        {
            var state = BuildState();
            var path = new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0), new GridCoordinate(2, 0) };

            var built = state.TryBuildPath(RoadId, path, PlayerId.None, _registry, null, out var created, out var validation);

            Assert.IsTrue(built, $"issues: {validation.Issues}");
            Assert.AreEqual(2, created.Count);
        }

        [Test]
        public void BuildPath_OnIslandB_Succeeds_IndependentlyOfIslandA()
        {
            var state = BuildState();
            state.TryBuildPath(RoadId, new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }, PlayerId.None, _registry, null, out _, out _);

            var builtB = state.TryBuildPath(RoadId, new[] { new GridCoordinate(52, 52), new GridCoordinate(53, 52) }, PlayerId.None, _registry, null, out var createdB, out var validationB);

            Assert.IsTrue(builtB, $"issues: {validationB.Issues}");
            Assert.AreEqual(1, createdB.Count);
        }

        [Test]
        public void FindRoute_AcrossDifferentIslands_Rejected()
        {
            var state = BuildState();

            var found = state.TryFindRoute(RoadId, new GridCoordinate(2, 2), new GridCoordinate(52, 52), _registry, null, out _);

            Assert.IsFalse(found, "A route request spanning two islands must be rejected before A* even runs.");
        }

        [Test]
        public void RemoveSegment_FreesCell_ForRoadOccupancyQuery()
        {
            var state = BuildState();
            state.TryBuildPath(RoadId, new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }, PlayerId.None, _registry, null, out var created, out _);

            Assert.IsTrue(state.IsCellRoadOccupied(new GridCoordinate(0, 0)));

            var removed = state.TryRemoveSegment(created[0].Id, out var removedSegment);

            Assert.IsTrue(removed);
            Assert.AreEqual(created[0].Id, removedSegment.Id);
            Assert.IsFalse(state.IsCellRoadOccupied(new GridCoordinate(0, 0)));
        }

        [Test]
        public void ConsumeDirtyCells_ReflectsRealChanges()
        {
            var state = BuildState();
            state.TryBuildPath(RoadId, new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }, PlayerId.None, _registry, null, out _, out _);

            var dirty = state.ConsumeDirtyCells(0);
            Assert.Greater(dirty.Count, 0);

            var consumedAgain = state.ConsumeDirtyCells(0);
            Assert.AreEqual(0, consumedAgain.Count);
        }
    }
}
