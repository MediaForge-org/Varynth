using System.Linq;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class ResourceCandidateGeneratorTests
    {
        private static RectInt Bounds => new RectInt(0, 0, 10, 10);

        private static IslandSurfaceMap BuildMap()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 10, 10);

            for (var z = 0; z < 10; z++)
            {
                for (var x = 0; x < 10; x++)
                {
                    var cell = new GridCoordinate(x, z);

                    // Border ring = Coast (not eligible), everything else Land,
                    // with a checkerboard-ish RockOrSteep pattern to exercise the
                    // rocky-preference rule.
                    if (x == 0 || z == 0 || x == 9 || z == 9)
                    {
                        map.SetFlags(cell, SurfaceCellFlags.Coast);
                    }
                    else
                    {
                        var flags = SurfaceCellFlags.Land;
                        if ((x + z) % 2 == 0)
                        {
                            flags |= SurfaceCellFlags.RockOrSteep;
                        }
                        else
                        {
                            flags |= SurfaceCellFlags.Buildable;
                        }
                        map.SetFlags(cell, flags);
                    }
                }
            }

            return map;
        }

        [Test]
        public void SameSeed_ProducesIdenticalCandidates()
        {
            var map = BuildMap();

            var first = ResourceCandidateGenerator.Generate(map, Bounds, seed: 42, maxCandidates: 5, minSpacingCells: 2);
            var second = ResourceCandidateGenerator.Generate(map, Bounds, seed: 42, maxCandidates: 5, minSpacingCells: 2);

            CollectionAssert.AreEqual(
                first.Select(c => c.Cell).ToList(),
                second.Select(c => c.Cell).ToList());
        }

        [Test]
        public void DifferentSeed_ProducesDifferentCandidates()
        {
            var map = BuildMap();

            var first = ResourceCandidateGenerator.Generate(map, Bounds, seed: 42, maxCandidates: 5, minSpacingCells: 2);
            var second = ResourceCandidateGenerator.Generate(map, Bounds, seed: 4242, maxCandidates: 5, minSpacingCells: 2);

            CollectionAssert.AreNotEqual(
                first.Select(c => c.Cell).ToList(),
                second.Select(c => c.Cell).ToList());
        }

        [Test]
        public void AllCandidates_AreLandAndNotCoast()
        {
            var map = BuildMap();

            var candidates = ResourceCandidateGenerator.Generate(map, Bounds, seed: 7, maxCandidates: 8, minSpacingCells: 1);

            foreach (var candidate in candidates)
            {
                Assert.IsTrue(map.TryGetFlags(candidate.Cell, out var flags));
                Assert.IsTrue((flags & SurfaceCellFlags.Land) != 0);
                Assert.IsFalse((flags & SurfaceCellFlags.Coast) != 0);
            }
        }

        [Test]
        public void NoDuplicateCells()
        {
            var map = BuildMap();

            var candidates = ResourceCandidateGenerator.Generate(map, Bounds, seed: 7, maxCandidates: 8, minSpacingCells: 1);
            var distinctCells = candidates.Select(c => c.Cell).Distinct().Count();

            Assert.AreEqual(candidates.Count, distinctCells);
        }

        [Test]
        public void RespectsMinimumSpacing()
        {
            var map = BuildMap();
            const int minSpacing = 3;

            var candidates = ResourceCandidateGenerator.Generate(map, Bounds, seed: 7, maxCandidates: 8, minSpacingCells: minSpacing);

            for (var i = 0; i < candidates.Count; i++)
            {
                for (var j = i + 1; j < candidates.Count; j++)
                {
                    var dx = candidates[i].Cell.X - candidates[j].Cell.X;
                    var dz = candidates[i].Cell.Z - candidates[j].Cell.Z;
                    var distanceSquared = dx * dx + dz * dz;
                    Assert.GreaterOrEqual(distanceSquared, minSpacing * minSpacing);
                }
            }
        }

        [Test]
        public void CountNeverExceedsMaxCandidates()
        {
            var map = BuildMap();

            var candidates = ResourceCandidateGenerator.Generate(map, Bounds, seed: 7, maxCandidates: 3, minSpacingCells: 1);

            Assert.LessOrEqual(candidates.Count, 3);
        }
    }
}
