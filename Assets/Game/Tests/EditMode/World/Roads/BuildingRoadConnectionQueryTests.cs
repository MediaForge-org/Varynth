using NUnit.Framework;
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
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Roads
{
    // Phase 2E: fully headless -- no Terrain/GameObject/ScriptableObject needed.
    public class BuildingRoadConnectionQueryTests
    {
        private WorldGrid _grid;
        private ContentRegistry<RoadDefinition> _registry;
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");
        private static readonly BuildingDefinition House =
            new BuildingDefinition(ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house");

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, (0f, 0f));
            _registry = new ContentRegistry<RoadDefinition>();
            _registry.Register(new RoadDefinition(RoadId, LocalizationKey.Parse("road.name"), "road"));
        }

        private static SimulationIslandData CreateAllBuildableIslandData(string name, GridCoordinate origin, int width, int height)
        {
            var flags = new SurfaceCellFlags[width * height];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            }
            var cellHeights = new float[width * height];
            return new SimulationIslandData(IslandId.FromName(name), name, origin.X, origin.Z, width, height, flags, cellHeights);
        }

        private RoadNetworkState BuildRoadState()
        {
            var state = new RoadNetworkState(_grid);
            var islandData = CreateAllBuildableIslandData("Island", new GridCoordinate(0, 0), 20, 20);
            state.AddIsland(islandData, new DenseGridHeightSource(_grid, 0, 0, 20, 20, islandData.CellHeights));
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
            var otherIslandData = CreateAllBuildableIslandData("OtherIsland", new GridCoordinate(150, 150), 10, 10);
            roads.AddIsland(otherIslandData, new DenseGridHeightSource(_grid, 150, 150, 10, 10, otherIslandData.CellHeights));
            roads.TryBuildPath(RoadId, new[] { new GridCoordinate(152, 150), new GridCoordinate(153, 150) }, PlayerId.None, _registry, null, out _, out _);

            // House at origin (0,0) is on a completely different island footprint --
            // no island registered there at all in this fixture's RoadNetworkState.
            var instance = HouseAt(new GridCoordinate(0, 0));
            Assert.IsFalse(BuildingRoadConnectionQuery.IsConnected(instance, House, roads));
        }
    }
}
