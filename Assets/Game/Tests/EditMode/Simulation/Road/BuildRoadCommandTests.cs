using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;
using Varynth.Core.Simulation.Road;

namespace Varynth.Tests.EditMode.Simulation.Road
{
    public class BuildRoadCommandTests
    {
        [Test]
        public void OrderedPath_DefensivelyCopied_ExternalMutationDoesNotAffectCommand()
        {
            var pathList = new List<GridCoordinate> { new GridCoordinate(0, 0), new GridCoordinate(1, 0) };
            var command = new BuildRoadCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("road.prototype.basic"), pathList);

            pathList.Add(new GridCoordinate(2, 0));
            pathList[0] = new GridCoordinate(99, 99);

            Assert.AreEqual(2, command.OrderedPath.Count);
            Assert.AreEqual(new GridCoordinate(0, 0), command.OrderedPath[0]);
        }
    }

    public class RemoveRoadCommandTests
    {
        [Test]
        public void Fields_AreExposedAsGiven()
        {
            var target = RoadSegmentId.FromRaw(7);
            var command = new RemoveRoadCommand(PlayerId.None, GameTick.Zero, target);

            Assert.AreEqual(target, command.TargetSegment);
        }
    }
}
