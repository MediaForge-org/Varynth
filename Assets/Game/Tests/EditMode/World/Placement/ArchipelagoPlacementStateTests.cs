using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Common;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Placement
{
    // Regression guard for adjustment 1 (and hardened further by Phase 2E point 12):
    // this fixture builds ArchipelagoPlacementState purely from plain SimulationIslandData
    // + DenseGridHeightSource -- no UnityEngine.Terrain/GameObject/ScriptableObject
    // needed at all, and no Varynth.Tooling.Editor type (that assembly is Editor-only
    // and would not exist in a Player build). Genuinely headless.
    public class ArchipelagoPlacementStateTests
    {
        private WorldGrid _grid;
        private ContentRegistry<BuildingDefinition> _registry;

        [SetUp]
        public void SetUp()
        {
            _grid = new WorldGrid(4f, (0f, 0f));

            var houseDefinition = new BuildingDefinition(
                ContentId.Parse("bld.prototype.house"), LocalizationKey.Parse("bld.house.name"), 2, 2, "house");
            _registry = new ContentRegistry<BuildingDefinition>();
            _registry.Register(houseDefinition);
        }

        private static SimulationIslandData CreateAllBuildableIslandData(string name, GridCoordinate origin, int width, int height)
        {
            var flags = new SurfaceCellFlags[width * height];
            for (var i = 0; i < flags.Length; i++)
            {
                flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            }
            var cellHeights = new float[width * height];
            for (var i = 0; i < cellHeights.Length; i++) cellHeights[i] = 5f; // well above sea level 0

            return new SimulationIslandData(IslandId.FromName(name), name, origin.X, origin.Z, width, height, flags, cellHeights);
        }

        private ArchipelagoPlacementState BuildState()
        {
            var state = new ArchipelagoPlacementState(_grid);

            var islandAData = CreateAllBuildableIslandData("IslandA", new GridCoordinate(0, 0), 10, 10);
            state.AddIsland(islandAData, new DenseGridHeightSource(_grid, 0, 0, 10, 10, islandAData.CellHeights));

            var islandBData = CreateAllBuildableIslandData("IslandB", new GridCoordinate(50, 50), 10, 10);
            state.AddIsland(islandBData, new DenseGridHeightSource(_grid, 50, 50, 10, 10, islandBData.CellHeights));

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
