using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;

namespace Varynth.Tests.EditMode.World
{
    public class GridCoordinateTests
    {
        [Test]
        public void Equals_SameXZ_AreEqual()
        {
            var a = new GridCoordinate(3, -5);
            var b = new GridCoordinate(3, -5);

            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test]
        public void Equals_DifferentXZ_AreNotEqual()
        {
            var a = new GridCoordinate(3, -5);
            var b = new GridCoordinate(3, 5);

            Assert.AreNotEqual(a, b);
            Assert.IsTrue(a != b);
        }

        [Test]
        public void GetHashCode_SameXZ_SameHash()
        {
            var a = new GridCoordinate(10, 20);
            var b = new GridCoordinate(10, 20);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void UsableAsDictionaryKey()
        {
            var dict = new Dictionary<GridCoordinate, string>
            {
                [new GridCoordinate(1, 1)] = "a",
                [new GridCoordinate(-1, -1)] = "b"
            };

            Assert.AreEqual("a", dict[new GridCoordinate(1, 1)]);
            Assert.AreEqual("b", dict[new GridCoordinate(-1, -1)]);
        }

        [Test]
        public void ToString_ContainsXAndZ()
        {
            var coordinate = new GridCoordinate(7, -2);

            var text = coordinate.ToString();

            StringAssert.Contains("7", text);
            StringAssert.Contains("-2", text);
        }
    }
}
