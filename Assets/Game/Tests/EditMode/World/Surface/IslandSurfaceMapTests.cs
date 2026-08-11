using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class IslandSurfaceMapTests
    {
        [Test]
        public void SetAndGetFlags_WithinBounds_Roundtrips()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(10, 20), width: 5, height: 5);
            var cell = new GridCoordinate(12, 22);

            map.SetFlags(cell, SurfaceCellFlags.Land | SurfaceCellFlags.Buildable);

            Assert.IsTrue(map.TryGetFlags(cell, out var flags));
            Assert.AreEqual(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable, flags);
        }

        [Test]
        public void TryGetFlags_OutsideBounds_ReturnsFalse()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), width: 3, height: 3);

            Assert.IsFalse(map.TryGetFlags(new GridCoordinate(3, 0), out _));
            Assert.IsFalse(map.TryGetFlags(new GridCoordinate(0, 3), out _));
            Assert.IsFalse(map.TryGetFlags(new GridCoordinate(-1, 0), out _));
            Assert.IsFalse(map.TryGetFlags(new GridCoordinate(0, -1), out _));
        }

        [Test]
        public void SetFlags_OutsideBounds_Throws()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), width: 2, height: 2);

            Assert.Throws<System.ArgumentOutOfRangeException>(() => map.SetFlags(new GridCoordinate(5, 5), SurfaceCellFlags.Water));
        }

        [Test]
        public void DefaultFlags_AreNone()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), width: 2, height: 2);

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(1, 1), out var flags));
            Assert.AreEqual(SurfaceCellFlags.None, flags);
        }

        [Test]
        public void NonZeroOrigin_LocalIndexingIsCorrect()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(-5, -5), width: 4, height: 4);

            map.SetFlags(new GridCoordinate(-5, -5), SurfaceCellFlags.Water);
            map.SetFlags(new GridCoordinate(-2, -2), SurfaceCellFlags.Land);

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(-5, -5), out var originFlags));
            Assert.AreEqual(SurfaceCellFlags.Water, originFlags);

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(-2, -2), out var farFlags));
            Assert.AreEqual(SurfaceCellFlags.Land, farFlags);
        }

        [Test]
        public void Constructor_NonPositiveDimensions_Throws()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new IslandSurfaceMap(new GridCoordinate(0, 0), 0, 5));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new IslandSurfaceMap(new GridCoordinate(0, 0), 5, 0));
        }
    }
}
