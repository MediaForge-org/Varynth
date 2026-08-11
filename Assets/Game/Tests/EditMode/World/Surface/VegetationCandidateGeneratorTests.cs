using System.Linq;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Surface;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class VegetationCandidateGeneratorTests
    {
        private static RectInt Bounds => new RectInt(0, 0, 4, 1);

        private static IslandSurfaceMap BuildMixedMap()
        {
            var map = new IslandSurfaceMap(new GridCoordinate(0, 0), 4, 1);

            map.SetFlags(new GridCoordinate(0, 0), SurfaceCellFlags.Water);
            map.SetFlags(new GridCoordinate(1, 0), SurfaceCellFlags.Coast);
            map.SetFlags(new GridCoordinate(2, 0), SurfaceCellFlags.Land | SurfaceCellFlags.RockOrSteep);
            map.SetFlags(new GridCoordinate(3, 0), SurfaceCellFlags.Land | SurfaceCellFlags.Buildable | SurfaceCellFlags.VegetationCandidate);

            return map;
        }

        [Test]
        public void OnlyReturnsCellsFlaggedAsVegetationCandidate()
        {
            var map = BuildMixedMap();

            var candidates = VegetationCandidateGenerator.Generate(map, Bounds);

            Assert.AreEqual(1, candidates.Count);
            Assert.AreEqual(new GridCoordinate(3, 0), candidates[0].Cell);
        }

        [Test]
        public void ExcludesWaterCoastAndRock()
        {
            var map = BuildMixedMap();

            var candidates = VegetationCandidateGenerator.Generate(map, Bounds);
            var cells = candidates.Select(c => c.Cell).ToList();

            Assert.IsFalse(cells.Contains(new GridCoordinate(0, 0)));
            Assert.IsFalse(cells.Contains(new GridCoordinate(1, 0)));
            Assert.IsFalse(cells.Contains(new GridCoordinate(2, 0)));
        }

        [Test]
        public void IsDeterministic_SameMapProducesSameResult()
        {
            var map = BuildMixedMap();

            var first = VegetationCandidateGenerator.Generate(map, Bounds).Select(c => c.Cell).ToList();
            var second = VegetationCandidateGenerator.Generate(map, Bounds).Select(c => c.Cell).ToList();

            CollectionAssert.AreEqual(first, second);
        }
    }
}
