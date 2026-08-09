using System;
using NUnit.Framework;
using Varynth.Core.Simulation.Clock;

namespace Varynth.Tests.EditMode.Simulation
{
    public class GameTickTests
    {
        [Test]
        public void Zero_HasValueZero()
        {
            Assert.AreEqual(0UL, GameTick.Zero.Value);
        }

        [Test]
        public void Add_NormalCase_IncrementsValue()
        {
            var tick = GameTick.Zero.Add(5);
            Assert.AreEqual(5UL, tick.Value);
        }

        [Test]
        public void Add_Overflow_ThrowsOverflowException()
        {
            var nearMax = GameTick.FromRaw(ulong.MaxValue);
            Assert.Throws<OverflowException>(() => nearMax.Add(1));
        }

        [Test]
        public void Equality_SameValue_AreEqual()
        {
            var a = GameTick.FromRaw(42);
            var b = GameTick.FromRaw(42);
            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
        }

        [Test]
        public void Comparison_OrdersByValue()
        {
            var a = GameTick.FromRaw(1);
            var b = GameTick.FromRaw(2);
            Assert.IsTrue(a < b);
            Assert.IsTrue(b > a);
            Assert.AreEqual(-1, a.CompareTo(b));
        }

        [Test]
        public void ToString_ReturnsNumericValue()
        {
            Assert.AreEqual("7", GameTick.FromRaw(7).ToString());
        }
    }
}
