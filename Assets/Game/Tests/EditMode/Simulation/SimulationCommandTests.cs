using System;
using NUnit.Framework;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Tests.EditMode.Simulation
{
    internal sealed class TestCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }

        public TestCommand(PlayerId issuedBy, GameTick issuedAtTick)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
        }
    }

    public class SimulationCommandTests
    {
        [Test]
        public void Command_ExposesStableInspectableMetadata()
        {
            var player = PlayerId.NewId();
            var tick = GameTick.FromRaw(5);
            var command = new TestCommand(player, tick);

            Assert.AreEqual(player, command.IssuedBy);
            Assert.AreEqual(tick, command.IssuedAtTick);
        }

        [Test]
        public void Command_WorksWithPlayerIdNone_NoOnlineIdentityRequired()
        {
            var command = new TestCommand(PlayerId.None, GameTick.Zero);

            Assert.IsTrue(command.IssuedBy.IsNone);
        }

        [Test]
        public void PlayerId_NewId_ProducesUniqueIds()
        {
            var a = PlayerId.NewId();
            var b = PlayerId.NewId();

            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void PlayerId_Equality_SameGuid_AreEqual()
        {
            var guid = Guid.NewGuid();
            var a = PlayerId.FromGuid(guid);
            var b = PlayerId.FromGuid(guid);

            Assert.AreEqual(a, b);
        }

        [Test]
        public void PlayerId_None_IsDistinctFromGenerated()
        {
            Assert.IsTrue(PlayerId.None.IsNone);
            Assert.AreNotEqual(PlayerId.None, PlayerId.NewId());
        }
    }
}
