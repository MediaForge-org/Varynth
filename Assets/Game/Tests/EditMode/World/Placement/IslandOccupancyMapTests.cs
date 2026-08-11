using System;
using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Placement;

namespace Varynth.Tests.EditMode.World.Placement
{
    public class IslandOccupancyMapTests
    {
        [Test]
        public void Occupy_ThenTryGetOccupant_ReturnsSameId()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);
            var id = BuildingInstanceId.FromRaw(1);
            var cells = new List<GridCoordinate> { new GridCoordinate(2, 2), new GridCoordinate(3, 2) };

            map.Occupy(cells, id);

            Assert.IsTrue(map.TryGetOccupant(new GridCoordinate(2, 2), out var occupant));
            Assert.AreEqual(id, occupant);
            Assert.IsTrue(map.TryGetOccupant(new GridCoordinate(3, 2), out var occupant2));
            Assert.AreEqual(id, occupant2);
        }

        [Test]
        public void TryGetOccupant_UnoccupiedCell_ReturnsFalse()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);

            Assert.IsFalse(map.TryGetOccupant(new GridCoordinate(5, 5), out _));
        }

        [Test]
        public void Occupy_OverlappingCells_Rejected()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);
            map.Occupy(new[] { new GridCoordinate(0, 0) }, BuildingInstanceId.FromRaw(1));

            Assert.Throws<InvalidOperationException>(() =>
                map.Occupy(new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }, BuildingInstanceId.FromRaw(2)));
        }

        [Test]
        public void Occupy_RejectedBatch_LeavesNoPartialOccupancy()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);
            map.Occupy(new[] { new GridCoordinate(1, 0) }, BuildingInstanceId.FromRaw(1));

            Assert.Throws<InvalidOperationException>(() =>
                map.Occupy(new[] { new GridCoordinate(0, 0), new GridCoordinate(1, 0) }, BuildingInstanceId.FromRaw(2)));

            // (0,0) must still be free -- the batch must not have partially applied.
            Assert.IsFalse(map.TryGetOccupant(new GridCoordinate(0, 0), out _));
        }

        [Test]
        public void Release_FreesAllCells()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);
            var cells = new List<GridCoordinate> { new GridCoordinate(4, 4), new GridCoordinate(5, 4) };
            map.Occupy(cells, BuildingInstanceId.FromRaw(1));

            map.Release(cells);

            Assert.IsFalse(map.TryGetOccupant(new GridCoordinate(4, 4), out _));
            Assert.IsFalse(map.TryGetOccupant(new GridCoordinate(5, 4), out _));
        }

        [Test]
        public void Release_ThenReoccupy_Succeeds()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 10, 10);
            var cells = new List<GridCoordinate> { new GridCoordinate(1, 1) };
            map.Occupy(cells, BuildingInstanceId.FromRaw(1));
            map.Release(cells);

            map.Occupy(cells, BuildingInstanceId.FromRaw(2));

            Assert.IsTrue(map.TryGetOccupant(new GridCoordinate(1, 1), out var occupant));
            Assert.AreEqual(BuildingInstanceId.FromRaw(2), occupant);
        }

        [Test]
        public void CanOccupy_OutsideBounds_ReturnsFalse()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 5, 5);

            Assert.IsFalse(map.CanOccupy(new[] { new GridCoordinate(100, 100) }));
        }

        [Test]
        public void CanOccupy_FreeCells_ReturnsTrue()
        {
            var map = new IslandOccupancyMap(new GridCoordinate(0, 0), 5, 5);

            Assert.IsTrue(map.CanOccupy(new[] { new GridCoordinate(1, 1), new GridCoordinate(2, 1) }));
        }
    }
}
