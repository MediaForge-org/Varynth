using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.World.Placement;

namespace Varynth.Tests.EditMode.World.Placement
{
    public class BuildingRepeatPlannerTests
    {
        [Test]
        public void StartEqualsEnd_ProducesExactlyOneOrigin()
        {
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(5, 5), new GridCoordinate(5, 5), 2, 2);

            Assert.AreEqual(1, origins.Count);
            Assert.AreEqual(new GridCoordinate(5, 5), origins[0]);
        }

        [Test]
        public void HorizontalDrag_ProducesLineAlongX()
        {
            // 2x2 footprint, drag 3 tiles worth (6 cells) along +X.
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(6, 0), 2, 2);

            Assert.AreEqual(4, origins.Count); // 0,2,4,6 step 2
            CollectionAssert.AreEqual(
                new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0), new GridCoordinate(4, 0), new GridCoordinate(6, 0) },
                origins);
        }

        [Test]
        public void VerticalDrag_ProducesLineAlongZ()
        {
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(0, 4), 2, 2);

            Assert.AreEqual(3, origins.Count);
            CollectionAssert.AreEqual(
                new[] { new GridCoordinate(0, 0), new GridCoordinate(0, 2), new GridCoordinate(0, 4) },
                origins);
        }

        [Test]
        public void RectangularDrag_ProducesGrid_RowMajorOrder()
        {
            // 2x2 footprint, 2 columns x 2 rows.
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(2, 2), 2, 2);

            CollectionAssert.AreEqual(
                new[]
                {
                    new GridCoordinate(0, 0), new GridCoordinate(2, 0),
                    new GridCoordinate(0, 2), new GridCoordinate(2, 2)
                },
                origins);
        }

        [Test]
        public void NegativeDirection_StepsCorrectly()
        {
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(-4, 0), 2, 2);

            CollectionAssert.AreEqual(
                new[] { new GridCoordinate(0, 0), new GridCoordinate(-2, 0), new GridCoordinate(-4, 0) },
                origins);
        }

        [Test]
        public void NegativeGlobalOrigin_Works()
        {
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(-100, -100), new GridCoordinate(-96, -100), 2, 2);

            Assert.AreEqual(3, origins.Count);
            Assert.AreEqual(new GridCoordinate(-100, -100), origins[0]);
            Assert.AreEqual(new GridCoordinate(-96, -100), origins[2]);
        }

        [Test]
        public void ThreeByTwoFootprintRotated90_UsesSwappedEffectiveSpacing()
        {
            // A 3x2 footprint rotated 90 degrees occupies 2x3 cells -- caller passes
            // effectiveWidth=2, effectiveLength=3 (already swapped upstream).
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(4, 0), 2, 3);

            CollectionAssert.AreEqual(
                new[] { new GridCoordinate(0, 0), new GridCoordinate(2, 0), new GridCoordinate(4, 0) },
                origins);
        }

        [Test]
        public void GeneratedFootprints_NeverOverlap()
        {
            var origins = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(0, 0), new GridCoordinate(10, 6), 3, 2);

            var occupied = new HashSet<GridCoordinate>();
            foreach (var origin in origins)
            {
                var cells = Varynth.World.Placement.BuildingFootprint.GetOccupiedCells(origin, 3, 2, Varynth.Core.Common.BuildingRotation.Deg0);
                foreach (var cell in cells)
                {
                    Assert.IsTrue(occupied.Add(cell), $"Cell {cell} occupied by more than one planned building.");
                }
            }
        }

        [Test]
        public void SameInputs_ProduceIdenticalOutput_Deterministic()
        {
            var a = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(3, -2), new GridCoordinate(11, 8), 2, 2);
            var b = BuildingRepeatPlanner.PlanOrigins(new GridCoordinate(3, -2), new GridCoordinate(11, 8), 2, 2);

            CollectionAssert.AreEqual(a, b);
        }
    }
}
