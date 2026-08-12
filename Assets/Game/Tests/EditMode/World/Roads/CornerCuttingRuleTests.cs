using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Roads;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class CornerCuttingRuleTests
    {
        private static IslandSurfaceMap FlatSurface()
        {
            return new IslandSurfaceMap(new GridCoordinate(0, 0), 20, 20);
        }

        [Test]
        public void NeitherFlankBlocked_DiagonalAllowed()
        {
            var surface = FlatSurface();
            var blocked = CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: false);
            Assert.IsFalse(blocked);
        }

        [Test]
        public void OneFlankBlocked_ByWater_DiagonalInvalid()
        {
            var surface = FlatSurface();
            surface.SetFlags(new GridCoordinate(6, 5), SurfaceCellFlags.Water); // one of NE's two flanking cells

            var blocked = CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: false);
            Assert.IsTrue(blocked, "A single blocked flanking cell must already invalidate the diagonal (conservative width-1 rule).");
        }

        [Test]
        public void OneFlankBlocked_ByRockOrSteep_DiagonalInvalid()
        {
            var surface = FlatSurface();
            surface.SetFlags(new GridCoordinate(5, 6), SurfaceCellFlags.RockOrSteep);

            var blocked = CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: false);
            Assert.IsTrue(blocked);
        }

        [Test]
        public void BothFlanksBlocked_DiagonalInvalid()
        {
            var surface = FlatSurface();
            surface.SetFlags(new GridCoordinate(6, 5), SurfaceCellFlags.Water);
            surface.SetFlags(new GridCoordinate(5, 6), SurfaceCellFlags.Water);

            var blocked = CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: false);
            Assert.IsTrue(blocked);
        }

        [Test]
        public void OrthogonalDirection_NeverBlockedByThisRule()
        {
            var surface = FlatSurface();
            var blocked = CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.E, surface, null, allowsCoastPlacement: false);
            Assert.IsFalse(blocked);
        }

        [Test]
        public void CoastFlank_BlockedUnlessAllowed()
        {
            var surface = FlatSurface();
            surface.SetFlags(new GridCoordinate(6, 5), SurfaceCellFlags.Coast);

            Assert.IsTrue(CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: false));
            Assert.IsFalse(CornerCuttingRule.IsDiagonalClearanceBlocked(new GridCoordinate(5, 5), RoadDirection.NE, surface, null, allowsCoastPlacement: true));
        }
    }
}
