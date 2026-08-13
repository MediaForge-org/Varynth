using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
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
    // Mirrors ArchipelagoPlacementStateTests' fixture shape exactly -- built purely
    // from plain SimulationIslandData + DenseGridHeightSource (Phase 2E), no
    // UnityEngine.Terrain/GameObject/ScriptableObject and no Varynth.Tooling.Editor
    // type. Genuinely headless.
    public class RoadNetworkStateTests
    {
        private WorldGrid _grid;
        private ContentRegistry<RoadDefinition> _registry;

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, (0f, 0f));

            var roadDefinition = new RoadDefinition(ContentId.Parse("road.prototype.basic"), LocalizationKey.Parse("road.name"), "road");
            _registry = new ContentRegistry<RoadDefinition>();
            _registry.Register(roadDefinition);
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

        private RoadNetworkState BuildState()
        {
            var state = new RoadNetworkState(_grid);

            var islandAData = CreateAllBuildableIslandData("IslandA", new GridCoordinate(0, 0), 10, 10);
            state.AddIsland(islandAData, new DenseGridHeightSource(_grid, 0, 0, 10, 10, islandAData.CellHeights));

            var islandBData = CreateAllBuildableIslandData("IslandB", new GridCoordinate(50, 50), 10, 10);
            state.AddIsland(islandBData, new DenseGridHeightSource(_grid, 50, 50, 10, 10, islandBData.CellHeights));

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
