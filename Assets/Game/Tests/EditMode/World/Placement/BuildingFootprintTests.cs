using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Placement;

namespace Varynth.Tests.EditMode.World.Placement
{
    public class BuildingFootprintTests
    {
        [Test]
        public void GetOccupiedCells_1x1_ReturnsSingleCell()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(5, 5), 1, 1, BuildingRotation.Deg0);

            Assert.AreEqual(1, cells.Count);
            Assert.AreEqual(new GridCoordinate(5, 5), cells[0]);
        }

        [Test]
        public void GetOccupiedCells_2x2_Deg0_ReturnsExpectedCells()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 2, 2, BuildingRotation.Deg0);

            CollectionAssert.AreEquivalent(new[]
            {
                new GridCoordinate(0, 0), new GridCoordinate(1, 0),
                new GridCoordinate(0, 1), new GridCoordinate(1, 1)
            }, cells);
        }

        [Test]
        public void GetOccupiedCells_3x2_Deg0_HasSixCells()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg0);

            Assert.AreEqual(6, cells.Count);
        }

        [Test]
        public void GetOccupiedCells_3x2_Deg90_SwapsWidthAndLength()
        {
            var deg0 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg0);
            var deg90 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg90);

            Assert.AreEqual(6, deg90.Count);
            // Deg0: 3 wide (X) x 2 deep (Z); Deg90: 2 wide (X) x 3 deep (Z).
            Assert.IsTrue(Contains(deg0, 2, 1));
            Assert.IsFalse(Contains(deg0, 1, 2));
            Assert.IsTrue(Contains(deg90, 1, 2));
            Assert.IsFalse(Contains(deg90, 2, 1));
        }

        [Test]
        public void GetOccupiedCells_3x2_Deg180_SameCellCountAsDeg0()
        {
            var deg0 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg0);
            var deg180 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg180);

            Assert.AreEqual(deg0.Count, deg180.Count);
            Assert.IsTrue(Contains(deg180, 2, 1));
        }

        [Test]
        public void GetOccupiedCells_3x2_Deg270_MatchesDeg90Shape()
        {
            var deg90 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg90);
            var deg270 = BuildingFootprint.GetOccupiedCells(new GridCoordinate(0, 0), 3, 2, BuildingRotation.Deg270);

            Assert.AreEqual(deg90.Count, deg270.Count);
            Assert.IsTrue(Contains(deg270, 1, 2));
        }

        [Test]
        public void GetOccupiedCells_NeverProducesDuplicateCells()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(-3, 7), 4, 3, BuildingRotation.Deg90);

            var seen = new HashSet<GridCoordinate>(cells);
            Assert.AreEqual(cells.Count, seen.Count);
        }

        [Test]
        public void GetOccupiedCells_NegativeOrigin_ProducesCorrectCells()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(-2, -2), 2, 2, BuildingRotation.Deg0);

            CollectionAssert.AreEquivalent(new[]
            {
                new GridCoordinate(-2, -2), new GridCoordinate(-1, -2),
                new GridCoordinate(-2, -1), new GridCoordinate(-1, -1)
            }, cells);
        }

        [Test]
        public void GetOccupiedCells_LargeGlobalOrigin_ProducesCorrectCellCount()
        {
            var cells = BuildingFootprint.GetOccupiedCells(new GridCoordinate(1_000_000, -1_000_000), 3, 2, BuildingRotation.Deg0);

            Assert.AreEqual(6, cells.Count);
            Assert.IsTrue(Contains(cells, 1_000_002, -999_999));
        }

        [Test]
        public void GetOccupiedCells_IsDeterministic_SameInputsSameOutput()
        {
            var a = BuildingFootprint.GetOccupiedCells(new GridCoordinate(2, 3), 3, 2, BuildingRotation.Deg90);
            var b = BuildingFootprint.GetOccupiedCells(new GridCoordinate(2, 3), 3, 2, BuildingRotation.Deg90);

            CollectionAssert.AreEqual(a, b);
        }

        private static bool Contains(IReadOnlyList<GridCoordinate> cells, int x, int z)
        {
            foreach (var cell in cells)
            {
                if (cell.X == x && cell.Z == z)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
