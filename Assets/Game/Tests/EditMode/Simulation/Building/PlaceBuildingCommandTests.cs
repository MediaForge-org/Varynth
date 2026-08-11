using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Tests.EditMode.Simulation.Building
{
    public class PlaceBuildingCommandTests
    {
        [Test]
        public void Ctor_StoresAllFields()
        {
            var player = PlayerId.NewId();
            var tick = GameTick.FromRaw(5);
            var buildingId = ContentId.Parse("bld.prototype.house");
            var cell = new GridCoordinate(3, 4);

            var command = new PlaceBuildingCommand(player, tick, buildingId, cell, BuildingRotation.Deg90);

            Assert.AreEqual(player, command.IssuedBy);
            Assert.AreEqual(tick, command.IssuedAtTick);
            Assert.AreEqual(buildingId, command.BuildingId);
            Assert.AreEqual(cell, command.Cell);
            Assert.AreEqual(BuildingRotation.Deg90, command.Rotation);
        }

        [Test]
        public void ImplementsISimulationCommand()
        {
            ISimulationCommand command = new PlaceBuildingCommand(
                PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), new GridCoordinate(0, 0), BuildingRotation.Deg0);

            Assert.IsNotNull(command);
        }

        [Test]
        public void SameFieldValues_ProduceEquivalentCommands()
        {
            var player = PlayerId.FromGuid(System.Guid.NewGuid());
            var tick = GameTick.FromRaw(10);
            var buildingId = ContentId.Parse("bld.prototype.house");
            var cell = new GridCoordinate(1, 1);

            var a = new PlaceBuildingCommand(player, tick, buildingId, cell, BuildingRotation.Deg180);
            var b = new PlaceBuildingCommand(player, tick, buildingId, cell, BuildingRotation.Deg180);

            // Determinism: identical inputs describe identical intent (field-by-field),
            // even though these are two distinct immutable instances.
            Assert.AreEqual(a.IssuedBy, b.IssuedBy);
            Assert.AreEqual(a.IssuedAtTick, b.IssuedAtTick);
            Assert.AreEqual(a.BuildingId, b.BuildingId);
            Assert.AreEqual(a.Cell, b.Cell);
            Assert.AreEqual(a.Rotation, b.Rotation);
        }
    }
}
