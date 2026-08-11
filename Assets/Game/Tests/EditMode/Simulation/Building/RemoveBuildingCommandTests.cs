using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Tests.EditMode.Simulation.Building
{
    public class RemoveBuildingCommandTests
    {
        [Test]
        public void Ctor_StoresAllFields()
        {
            var player = PlayerId.NewId();
            var tick = GameTick.FromRaw(7);
            var targetId = BuildingInstanceId.FromRaw(42);

            var command = new RemoveBuildingCommand(player, tick, targetId);

            Assert.AreEqual(player, command.IssuedBy);
            Assert.AreEqual(tick, command.IssuedAtTick);
            Assert.AreEqual(targetId, command.TargetInstanceId);
        }

        [Test]
        public void ImplementsISimulationCommand()
        {
            ISimulationCommand command = new RemoveBuildingCommand(PlayerId.NewId(), GameTick.Zero, BuildingInstanceId.FromRaw(1));

            Assert.IsNotNull(command);
        }
    }
}
