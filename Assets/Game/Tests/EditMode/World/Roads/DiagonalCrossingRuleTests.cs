using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;
using Varynth.World.Roads;

namespace Varynth.Tests.EditMode.World.Roads
{
    public class DiagonalCrossingRuleTests
    {
        private static readonly ContentId RoadId = ContentId.Parse("road.prototype.basic");

        [Test]
        public void OppositeDiagonal_SameSquare_IsDetected()
        {
            var graph = new RoadGraph();
            // "\" pair of the square (0,0)-(1,1): (0,1)-(1,0).
            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 1), new GridCoordinate(1, 0), RoadDirection.SE, PlayerId.None);

            // Candidate "/" pair of the same square: (0,0)-(1,1).
            var present = DiagonalCrossingRule.IsOppositeDiagonalPresent(new GridCoordinate(0, 0), new GridCoordinate(1, 1), graph);
            Assert.IsTrue(present);
        }

        [Test]
        public void SameOrientation_InSameSquare_NotFlagged()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 0), new GridCoordinate(1, 1), RoadDirection.NE, PlayerId.None);

            var present = DiagonalCrossingRule.IsOppositeDiagonalPresent(new GridCoordinate(0, 0), new GridCoordinate(1, 1), graph);
            Assert.IsFalse(present, "Checking the exact same segment/orientation is not a crossing.");
        }

        [Test]
        public void NoExistingDiagonal_NotFlagged()
        {
            var graph = new RoadGraph();
            var present = DiagonalCrossingRule.IsOppositeDiagonalPresent(new GridCoordinate(10, 10), new GridCoordinate(11, 11), graph);
            Assert.IsFalse(present);
        }

        [Test]
        public void DifferentSquare_NotFlagged()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(0, 1), new GridCoordinate(1, 0), RoadDirection.SE, PlayerId.None);

            var present = DiagonalCrossingRule.IsOppositeDiagonalPresent(new GridCoordinate(5, 5), new GridCoordinate(6, 6), graph);
            Assert.IsFalse(present);
        }

        [Test]
        public void NegativeGridSquare_DetectedCorrectly()
        {
            var graph = new RoadGraph();
            graph.AddSegment(RoadSegmentId.FromRaw(1), RoadId, new GridCoordinate(-5, -4), new GridCoordinate(-4, -5), RoadDirection.SE, PlayerId.None);

            var present = DiagonalCrossingRule.IsOppositeDiagonalPresent(new GridCoordinate(-5, -5), new GridCoordinate(-4, -4), graph);
            Assert.IsTrue(present);
        }
    }
}
