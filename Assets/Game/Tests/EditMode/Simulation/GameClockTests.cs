using System;
using NUnit.Framework;
using Varynth.Core.Simulation.Clock;

namespace Varynth.Tests.EditMode.Simulation
{
    public class GameClockTests
    {
        [Test]
        public void StartsAtTickZero_NotPaused()
        {
            var clock = new GameClock();
            Assert.AreEqual(GameTick.Zero, clock.CurrentTick);
            Assert.IsFalse(clock.IsPaused);
        }

        [Test]
        public void Advance_Default_IncrementsByOne()
        {
            var clock = new GameClock();
            var advanced = clock.Advance();
            Assert.IsTrue(advanced);
            Assert.AreEqual(GameTick.FromRaw(1), clock.CurrentTick);
        }

        [Test]
        public void Advance_MultipleTicks_IncrementsByDelta()
        {
            var clock = new GameClock();
            clock.Advance(10);
            Assert.AreEqual(GameTick.FromRaw(10), clock.CurrentTick);
        }

        [Test]
        public void Pause_PreventsAdvance()
        {
            var clock = new GameClock();
            clock.Pause();
            var advanced = clock.Advance();
            Assert.IsFalse(advanced);
            Assert.AreEqual(GameTick.Zero, clock.CurrentTick);
        }

        [Test]
        public void Resume_AllowsAdvanceAgain()
        {
            var clock = new GameClock();
            clock.Pause();
            clock.Advance();
            clock.Resume();
            var advanced = clock.Advance();
            Assert.IsTrue(advanced);
            Assert.AreEqual(GameTick.FromRaw(1), clock.CurrentTick);
        }

        [Test]
        public void Step_AdvancesEvenWhilePaused()
        {
            var clock = new GameClock();
            clock.Pause();
            clock.Step();
            Assert.AreEqual(GameTick.FromRaw(1), clock.CurrentTick);
            Assert.IsTrue(clock.IsPaused);
        }

        [Test]
        public void Advance_NearMaxValue_ThrowsOverflowInsteadOfWrapping()
        {
            var clock = new GameClock();
            clock.Step(ulong.MaxValue);
            Assert.Throws<OverflowException>(() => clock.Advance());
        }

        [Test]
        public void Pause_IsIdempotent()
        {
            var clock = new GameClock();
            clock.Pause();
            clock.Pause();
            Assert.IsTrue(clock.IsPaused);
        }
    }
}
