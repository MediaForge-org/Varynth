using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.Core.Simulation.Road;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Roads;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Roads
{
    // Phase 2E: fully headless -- no Terrain/GameObject/ScriptableObject needed.
    public class RoadCommandHandlerTests
    {
        private ContentRegistry<RoadDefinition> _registry;
        private RoadNetworkState _state;
        private RoadCommandHandler _handler;
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        private static SimulationIslandData CreateAllBuildableIslandData(string name, int originX, int originZ, int width, int height, SurfaceCellFlags[] flagsOverride = null)
        {
            var flags = flagsOverride ?? BuildFlags(width, height);
            var cellHeights = new float[width * height];
            return new SimulationIslandData(IslandId.FromName(name), name, originX, originZ, width, height, flags, cellHeights);
        }

        private static SurfaceCellFlags[] BuildFlags(int width, int height)
        {
            var flags = new SurfaceCellFlags[width * height];
            for (var i = 0; i < flags.Length; i++) flags[i] = SurfaceCellFlags.Land | SurfaceCellFlags.Buildable;
            return flags;
        }

        [SetUp]
        public void SetUp()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var islandData = CreateAllBuildableIslandData("Island", 0, 0, 20, 20);

            _state = new RoadNetworkState(grid);
            _state.AddIsland(islandData, new DenseGridHeightSource(grid, 0, 0, 20, 20, islandData.CellHeights));

            _registry = new ContentRegistry<RoadDefinition>();
            _registry.Register(new RoadDefinition(RoadId, LocalizationKey.Parse("road.name"), "road"));

            _handler = new RoadCommandHandler(_state, _registry);
        }

        [Test]
        public void Handle_BuildRoadCommand_FullyValidPath_CreatesSegments()
        {
            var command = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId,
                new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0), new GridCoordinate(2, 0) });

            var handled = _handler.Handle(command, out var created, out var validation);

            Assert.IsTrue(handled, $"issues: {validation.Issues}");
            Assert.AreEqual(2, created.Count);
        }

        [Test]
        public void Handle_EarlySegmentValid_LaterSegmentInvalid_AtomicallyRejectsWholePath()
        {
            // Block cell (3,0) with water so the 2nd->3rd segment is invalid, while the
            // 1st->2nd segment is perfectly valid on its own.
            var grid = new WorldGrid(4f, (0f, 0f));
            var flags = BuildFlags(20, 20);
            flags[0 * 20 + 3] = SurfaceCellFlags.Water; // cell (3,0) -- local index z*width+x
            var islandData = CreateAllBuildableIslandData("BlockIsland", 0, 0, 20, 20, flags);

            var state = new RoadNetworkState(grid);
            state.AddIsland(islandData, new DenseGridHeightSource(grid, 0, 0, 20, 20, islandData.CellHeights));
            var handler = new RoadCommandHandler(state, _registry);

            var command = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId,
                new[] { new GridCoordinate(1, 0), new GridCoordinate(2, 0), new GridCoordinate(3, 0) });

            var handled = handler.Handle(command, out var created, out var validation);

            Assert.IsFalse(handled);
            Assert.AreEqual(0, created.Count);
            Assert.IsFalse(state.GetGraph(0).HasSegmentBetween(new GridCoordinate(1, 0), new GridCoordinate(2, 0)),
                "No segment -- not even the earlier, individually-valid one -- may exist after an atomically rejected batch.");

            // The counter must not have advanced: a subsequent valid build starts at raw id 1.
            var followUp = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId,
                new[] { new GridCoordinate(1, 0), new GridCoordinate(2, 0) });
            handler.Handle(followUp, out var followUpCreated, out _);
            Assert.AreEqual(1UL, followUpCreated[0].Id.Value);
        }

        [Test]
        public void Handle_PathReusingExistingSegment_OnlyCreatesMissingSegments_NoDetour()
        {
            var first = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId, new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) });
            _handler.Handle(first, out var firstCreated, out _);
            var existingId = firstCreated[0].Id;

            var second = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId,
                new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0), new GridCoordinate(2, 0) });
            var handled = _handler.Handle(second, out var secondCreated, out _);

            Assert.IsTrue(handled);
            Assert.AreEqual(1, secondCreated.Count, "Only the genuinely missing segment should be created.");
            Assert.IsTrue(_state.GetGraph(0).TryGetSegment(existingId, out _), "The pre-existing segment must be untouched, not recreated.");
        }

        [Test]
        public void Handle_RemoveRoadCommand_RemovesSegment()
        {
            var build = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, RoadId, new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) });
            _handler.Handle(build, out var created, out _);

            var remove = new RemoveRoadCommand(PlayerId.NewId(), GameTick.Zero, created[0].Id);
            var handled = _handler.Handle(remove, out var removed);

            Assert.IsTrue(handled);
            Assert.AreEqual(created[0].Id, removed.Id);
        }
    }
}
