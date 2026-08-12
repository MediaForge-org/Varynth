using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class BuildingRoadConnectionQueryTests
    {
        private GameObject _islandGo;
        private UnityEngine.Terrain _island;
        private WorldGrid _grid;
        private ContentRegistry<RoadDefinition> _registry;
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");
        private static readonly BuildingDefinition House =
            new BuildingDefinition(ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house");

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, Vector2.zero);
            _island = CreateFlatTerrain("Island", new Vector3(0f, -15f, 0f), 40f, out _islandGo);
            _registry = new ContentRegistry<RoadDefinition>();
            _registry.Register(new RoadDefinition(RoadId, LocalizationKey.Parse("road.name"), "road"));
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_islandGo);
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

        private RoadNetworkState BuildRoadState()
        {
            var state = new RoadNetworkState(_grid);
            state.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(0, 0), 20, 20), _island);
            return state;
        }

        // A 2x2 house at origin (0,0) occupies cells (0,0)-(1,1).
        private static BuildingInstance HouseAt(GridCoordinate origin, BuildingRotation rotation = BuildingRotation.Deg0)
        {
            var cells = BuildingFootprint.GetOccupiedCells(origin, House.FootprintWidth, House.FootprintLength, rotation);
            return new BuildingInstance(BuildingInstanceId.FromRaw(1), House.Id, origin, rotation, PlayerId.None, cells);
        }

        [Test]
        public void RoadDirectlyAdjacentToEdge_IsConnected()
        {
            var roads = BuildRoadState();
            // Road segment touching the house's east edge (cell (2,0) is adjacent to (1,0)).
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(2, 0), new GridCoordinate(3, 0) }, PlayerId.None, _registry, null, out _, out _);

            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsTrue(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));
        }

        [Test]
        public void RoadOneCellGap_IsNotConnected()
        {
            var roads = BuildRoadState();
            // Gap of one empty cell between the house's edge (x=1) and the road (x=3).
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(3, 0), new GridCoordinate(4, 0) }, PlayerId.None, _registry, null, out _, out _);

            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsFalse(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));
        }

        [Test]
        public void RoadTouchingOnlyDiagonalCorner_IsNotConnected()
        {
            var roads = BuildRoadState();
            // Cell (2,2) touches the house footprint (0,0)-(1,1) only at the corner (1,1)-(2,2) diagonal.
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(2, 2), new GridCoordinate(3, 3) }, PlayerId.None, _registry, null, out _, out _);

            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsFalse(BuildingRoadConnectionQuery.IsConnected(instance, House, roads), "Corner-only diagonal touch must not count as connected.");
        }

        [Test]
        public void RotatedFootprint_CorrectEdgeChecked()
        {
            var roads = BuildRoadState();
            // House is 2x2 -- rotation doesn't change its cell set, but verifies the
            // query still works correctly when rotation is passed through.
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(2, 0), new GridCoordinate(3, 0) }, PlayerId.None, _registry, null, out _, out _);

            var instance = HouseAt(new GridCoordinate(0, 0), BuildingRotation.Deg90);
            Assert.IsTrue(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));
        }

        [Test]
        public void RoadRemoved_NoLongerConnected()
        {
            var roads = BuildRoadState();
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(2, 0), new GridCoordinate(3, 0) }, PlayerId.None, _registry, null, out var created, out _);

            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsTrue(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));

            roads.TryRemoveSegment(created[0].Id, out _);
            Assert.IsFalse(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));
        }

        [Test]
        public void DifferentIsland_NeverConnected()
        {
            var roads = new RoadNetworkState(_grid);
            var otherGo = new GameObject("Other");
            var otherIsland = CreateFlatTerrain("OtherIsland", new Vector3(500f, -15f, 500f), 40f, out otherGo);
            roads.AddIsland(CreateAllBuildableRuntimeData(new GridCoordinate(150, 150), 10, 10), otherIsland);
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(152, 150), new GridCoordinate(153, 150) }, PlayerId.None, _registry, null, out _, out _);

            // House at origin (0,0) is on a completely different island footprint --
            // no island registered there at all in this fixture's RoadNetworkState.
            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsFalse(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));

            Object.DestroyImmediate(otherGo);
        }
    }
}
