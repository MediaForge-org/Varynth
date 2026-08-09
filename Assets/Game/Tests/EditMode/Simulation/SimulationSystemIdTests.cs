using System;
using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Simulation.Scheduling;

namespace Varynth.Tests.EditMode.Simulation
{
    public class SimulationSystemIdTests
    {
        [TestCase("population")]
        [TestCase("production")]
        [TestCase("simulation.trade")]
        public void Parse_ValidId_Succeeds(string raw)
        {
            var id = SimulationSystemId.Parse(raw);
            Assert.AreEqual(raw, id.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("Production")]
        [TestCase("trade..ships")]
        [TestCase("production@core")]
        [TestCase(".population")]
        [TestCase("population.")]
        public void Parse_InvalidId_Throws(string raw)
        {
            Assert.Throws<ArgumentException>(() => SimulationSystemId.Parse(raw));
        }

        [Test]
        public void TryParse_InvalidId_ReturnsFalseAndDefault()
        {
            var success = SimulationSystemId.TryParse("Production", out var id);
            Assert.IsFalse(success);
            Assert.IsTrue(id.IsDefault);
        }

        [Test]
        public void TryParse_ValidId_ReturnsTrue()
        {
            var success = SimulationSystemId.TryParse("population", out var id);
            Assert.IsTrue(success);
            Assert.AreEqual("population", id.ToString());
        }

        [Test]
        public void Equality_SameValue_AreEqual()
        {
            var a = SimulationSystemId.Parse("population");
            var b = SimulationSystemId.Parse("population");
            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void CanBeUsedAsDictionaryKey()
        {
            var dict = new Dictionary<SimulationSystemId, int>
            {
                [SimulationSystemId.Parse("population")] = 1
            };
            Assert.IsTrue(dict.ContainsKey(SimulationSystemId.Parse("population")));
        }
    }
}
