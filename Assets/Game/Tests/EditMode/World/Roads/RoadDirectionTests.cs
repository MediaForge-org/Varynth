using NUnit.Framework;
using Varynth.Core.Common;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class RoadDirectionTests
    {
        [TestCase(0, 1, RoadDirection.N)]
        [TestCase(1, 1, RoadDirection.NE)]
        [TestCase(1, 0, RoadDirection.E)]
        [TestCase(1, -1, RoadDirection.SE)]
        [TestCase(0, -1, RoadDirection.S)]
        [TestCase(-1, -1, RoadDirection.SW)]
        [TestCase(-1, 0, RoadDirection.W)]
        [TestCase(-1, 1, RoadDirection.NW)]
        public void TryFromDelta_AllEightUnitDeltas_ResolveCorrectly(int dx, int dz, RoadDirection expected)
        {
            Assert.IsTrue(RoadDirectionExtensions.TryFromDelta(dx, dz, out var direction));
            Assert.AreEqual(expected, direction);
        }

        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(0, 2)]
        [TestCase(2, 2)]
        public void TryFromDelta_InvalidDeltas_Rejected(int dx, int dz)
        {
            Assert.IsFalse(RoadDirectionExtensions.TryFromDelta(dx, dz, out _));
        }

        [TestCase(RoadDirection.N, false)]
        [TestCase(RoadDirection.NE, true)]
        [TestCase(RoadDirection.E, false)]
        [TestCase(RoadDirection.SE, true)]
        public void IsDiagonal_IsCorrect(RoadDirection direction, bool expected)
        {
            Assert.AreEqual(expected, direction.IsDiagonal());
        }

        [Test]
        public void CostUnits_AreIntegers_OrthogonalAndDiagonal()
        {
            Assert.AreEqual(1000, RoadDirection.N.CostUnits());
            Assert.AreEqual(1414, RoadDirection.NE.CostUnits());
        }

        [Test]
        public void Opposite_RoundTrips()
        {
            Assert.AreEqual(RoadDirection.S, RoadDirection.N.Opposite());
            Assert.AreEqual(RoadDirection.N, RoadDirection.N.Opposite().Opposite());
            Assert.AreEqual(RoadDirection.SW, RoadDirection.NE.Opposite());
        }
    }
}
