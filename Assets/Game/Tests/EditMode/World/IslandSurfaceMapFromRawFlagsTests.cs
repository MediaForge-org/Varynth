using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World
{
    public class IslandSurfaceMapFromRawFlagsTests
    {
        [Test]
        public void FromRawFlags_RoundTripsExactly()
        {
            var origin = new GridCoordinate(3, 4);
            var flags = new[]
            {
                SurfaceCellFlags.Water, SurfaceCellFlags.Coast,
                SurfaceCellFlags.Land | SurfaceCellFlags.Buildable, SurfaceCellFlags.Land | SurfaceCellFlags.RockOrSteep
            };

            var map = IslandSurfaceMap.FromRawFlags(origin, 2, 2, flags);

            map.TryGetFlags(new GridCoordinate(3, 4), out var a);
            map.TryGetFlags(new GridCoordinate(4, 4), out var b);
            map.TryGetFlags(new GridCoordinate(3, 5), out var c);
            map.TryGetFlags(new GridCoordinate(4, 5), out var d);

            Assert.AreEqual(SurfaceCellFlags.Water, a);
            Assert.AreEqual(SurfaceCellFlags.Coast, b);
            Assert.AreEqual(SurfaceCellFlags.Land | SurfaceCellFlags.Buildable, c);
            Assert.AreEqual(SurfaceCellFlags.Land | SurfaceCellFlags.RockOrSteep, d);
        }

        [Test]
        public void FromRawFlags_DoesNotMutateSourceArray()
        {
            var flags = new[] { SurfaceCellFlags.Water };
            var map = IslandSurfaceMap.FromRawFlags(new GridCoordinate(0, 0), 1, 1, flags);

            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Buildable);

            // The map wraps a copy, not the original array -- source must be untouched.
            Assert.AreEqual(SurfaceCellFlags.Water, flags[0]);
        }

        [Test]
        public void FromRawFlags_WrongLength_Throws()
        {
            Assert.Throws<System.ArgumentException>(() =>
                IslandSurfaceMap.FromRawFlags(new GridCoordinate(0, 0), 2, 2, new[] { SurfaceCellFlags.Water }));
        }
    }
}
