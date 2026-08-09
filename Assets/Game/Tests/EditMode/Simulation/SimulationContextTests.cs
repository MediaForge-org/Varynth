using NUnit.Framework;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Context;

namespace Varynth.Tests.EditMode.Simulation
{
    public class SimulationContextTests
    {
        [Test]
        public void AllThreeLevels_AreDistinct()
        {
            Assert.AreNotEqual(SimulationLevel.ActiveNear, SimulationLevel.ActiveFar);
            Assert.AreNotEqual(SimulationLevel.ActiveFar, SimulationLevel.Background);
            Assert.AreNotEqual(SimulationLevel.ActiveNear, SimulationLevel.Background);
        }

        [Test]
        public void Context_CarriesTickAndLevel()
        {
            var tick = GameTick.FromRaw(42);
            var context = new SimulationContext(tick, SimulationLevel.ActiveFar);
            Assert.AreEqual(tick, context.Tick);
            Assert.AreEqual(SimulationLevel.ActiveFar, context.Level);
        }

        [TestCase(SimulationLevel.ActiveNear, SimulationLevelMask.ActiveNear)]
        [TestCase(SimulationLevel.ActiveFar, SimulationLevelMask.ActiveFar)]
        [TestCase(SimulationLevel.Background, SimulationLevelMask.Background)]
        public void ToMask_MapsToSingleBit(SimulationLevel level, SimulationLevelMask expected)
        {
            Assert.AreEqual(expected, level.ToMask());
        }

        [Test]
        public void CombinedMask_MatchesBothLevelsViaBitwiseAnd()
        {
            var combined = SimulationLevelMask.ActiveNear | SimulationLevelMask.ActiveFar;
            Assert.AreNotEqual(SimulationLevelMask.None, combined & SimulationLevel.ActiveNear.ToMask());
            Assert.AreNotEqual(SimulationLevelMask.None, combined & SimulationLevel.ActiveFar.ToMask());
            Assert.AreEqual(SimulationLevelMask.None, combined & SimulationLevel.Background.ToMask());
        }

        [Test]
        public void Mask_None_IsInvalid()
        {
            Assert.IsFalse(SimulationLevelMask.None.IsValid());
        }

        [Test]
        public void Mask_UndefinedBits_AreInvalid()
        {
            var garbage = (SimulationLevelMask)(1 << 10);
            Assert.IsFalse(garbage.IsValid());
        }

        [Test]
        public void Mask_UndefinedBitsCombinedWithValidBit_AreInvalid()
        {
            var mixed = SimulationLevelMask.ActiveNear | (SimulationLevelMask)(1 << 10);
            Assert.IsFalse(mixed.IsValid());
        }

        [Test]
        public void Mask_All_IsValid()
        {
            Assert.IsTrue(SimulationLevelMask.All.IsValid());
        }

        [Test]
        public void Mask_SingleDefinedLevel_IsValid()
        {
            Assert.IsTrue(SimulationLevelMask.ActiveNear.IsValid());
        }
    }
}
