using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Building;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Tests.EditMode.Simulation.Building
{
    public class PlaceBuildingBatchCommandTests
    {
        [Test]
        public void Origins_DefensivelyCopied_ExternalMutationDoesNotAffectCommand()
        {
            var originList = new List<GridCoordinate> { new GridCoordinate(0, 0), new GridCoordinate(2, 0) };
            var command = new PlaceBuildingBatchCommand(PlayerId.NewId(), GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, originList);

            originList.Add(new GridCoordinate(4, 0));
            originList[0] = new GridCoordinate(99, 99);

            Assert.AreEqual(2, command.Origins.Count);
            Assert.AreEqual(new GridCoordinate(0, 0), command.Origins[0]);
        }

        [Test]
        public void SameInputs_ProduceEqualState_Deterministic()
        {
            var origins = new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0) };
            var a = new PlaceBuildingBatchCommand(PlayerId.None, GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, origins);
            var b = new PlaceBuildingBatchCommand(PlayerId.None, GameTick.Zero, ContentId.Parse("bld.prototype.house"), BuildingRotation.Deg0, origins);

            CollectionAssert.AreEqual(a.Origins, b.Origins);
            Assert.AreEqual(a.BuildingId, b.BuildingId);
            Assert.AreEqual(a.Rotation, b.Rotation);
        }
    }
}
