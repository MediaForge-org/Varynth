using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;

namespace Varynth.Tests.EditMode.World
{
    public class WorldGridTests
    {
        [Test]
        public void WorldToCell_PositiveCoordinates_ReturnsExpectedCell()
        {
            var grid = new WorldGrid(4f, (0f, 0f));

            var cell = grid.WorldToCell(10f, 6f);

            Assert.AreEqual(new GridCoordinate(2, 1), cell);
        }

        [Test]
        public void WorldToCell_NegativeCoordinates_FloorsCorrectly()
        {
            var grid = new WorldGrid(4f, (0f, 0f));

            var cell = grid.WorldToCell(-1f, -5f);

            Assert.AreEqual(new GridCoordinate(-1, -2), cell);
        }

        [Test]
        public void WorldToCell_ExactCellBoundary_BelongsToNextCell()
        {
            var grid = new WorldGrid(4f, (0f, 0f));

            var onBoundary = grid.WorldToCell(8f, 0f);
            var justBefore = grid.WorldToCell(7.999f, 0f);

            Assert.AreEqual(new GridCoordinate(2, 0), onBoundary);
            Assert.AreEqual(new GridCoordinate(1, 0), justBefore);
        }

        [Test]
        public void CellToWorldCenter_ReturnsCellMidpoint()
        {
            var grid = new WorldGrid(4f, (0f, 0f));

            var center = grid.CellToWorldCenter(new GridCoordinate(2, 1));

            Assert.AreEqual(10f, center.X, 1e-5f);
            Assert.AreEqual(6f, center.Z, 1e-5f);
        }

        [Test]
        public void Roundtrip_CellToWorldCenter_ThenBackToCell_IsStable()
        {
            var grid = new WorldGrid(4f, (0f, 0f));
            var original = new GridCoordinate(-3, 7);

            var center = grid.CellToWorldCenter(original);
            var roundtripped = grid.WorldToCell(center.X, center.Z);

            Assert.AreEqual(original, roundtripped);
        }

        [Test]
        public void WorldToCell_DifferentCellSize_ScalesCorrectly()
        {
            var grid = new WorldGrid(2f, (0f, 0f));

            var cell = grid.WorldToCell(5f, 5f);

            Assert.AreEqual(new GridCoordinate(2, 2), cell);
        }

        [Test]
        public void WorldToCell_NonZeroOrigin_OffsetsCorrectly()
        {
            var grid = new WorldGrid(4f, (10f, 10f));

            var cell = grid.WorldToCell(10f, 10f);

            Assert.AreEqual(new GridCoordinate(0, 0), cell);
        }

        [Test]
        public void Constructor_NonPositiveCellSize_Throws()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new WorldGrid(0f, (0f, 0f)));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new WorldGrid(-1f, (0f, 0f)));
        }
    }
}
