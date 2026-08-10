using NUnit.Framework;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World
{
    public class IslandHeightmapGeneratorTests
    {
        private const int Resolution = 65;
        private const int Seed = 20260809;

        // Matches WorldPrototypeSceneBuilder's sea-level convention:
        // Terrain Transform Y = -15, TerrainData vertical size = 40 -> 15/40 = 0.375.
        private const float SeaLevelNormalized = 15f / 40f;

        [Test]
        public void Generate_SameSeedAndResolution_ProducesIdenticalHeights()
        {
            var first = IslandHeightmapGenerator.Generate(Resolution, Seed);
            var second = IslandHeightmapGenerator.Generate(Resolution, Seed);

            for (var y = 0; y < Resolution; y++)
            {
                for (var x = 0; x < Resolution; x++)
                {
                    Assert.AreEqual(first[y, x], second[y, x], 0f, $"Mismatch at ({x},{y})");
                }
            }
        }

        [Test]
        public void Generate_DifferentSeed_ProducesDifferentHeights()
        {
            var first = IslandHeightmapGenerator.Generate(Resolution, Seed);
            var second = IslandHeightmapGenerator.Generate(Resolution, Seed + 1);

            var anyDifferent = false;
            for (var y = 0; y < Resolution && !anyDifferent; y++)
            {
                for (var x = 0; x < Resolution && !anyDifferent; x++)
                {
                    if (first[y, x] != second[y, x])
                    {
                        anyDifferent = true;
                    }
                }
            }

            Assert.IsTrue(anyDifferent);
        }

        [Test]
        public void Generate_AllValuesWithinNormalizedRange()
        {
            var heights = IslandHeightmapGenerator.Generate(Resolution, Seed);

            for (var y = 0; y < Resolution; y++)
            {
                for (var x = 0; x < Resolution; x++)
                {
                    Assert.GreaterOrEqual(heights[y, x], 0f);
                    Assert.LessOrEqual(heights[y, x], 1f);
                }
            }
        }

        [Test]
        public void Generate_ProducesValuesBothBelowAndAboveSeaLevel()
        {
            var heights = IslandHeightmapGenerator.Generate(Resolution, Seed);

            var hasBelowSeaLevel = false;
            var hasAboveSeaLevel = false;

            for (var y = 0; y < Resolution; y++)
            {
                for (var x = 0; x < Resolution; x++)
                {
                    if (heights[y, x] < SeaLevelNormalized) hasBelowSeaLevel = true;
                    if (heights[y, x] > SeaLevelNormalized) hasAboveSeaLevel = true;
                }
            }

            Assert.IsTrue(hasBelowSeaLevel, "Expected at least one underwater sample (island is not flat/all-land).");
            Assert.IsTrue(hasAboveSeaLevel, "Expected at least one above-sea-level land sample.");
        }

        [Test]
        public void Generate_IsNotFlat()
        {
            var heights = IslandHeightmapGenerator.Generate(Resolution, Seed);

            var min = float.MaxValue;
            var max = float.MinValue;

            for (var y = 0; y < Resolution; y++)
            {
                for (var x = 0; x < Resolution; x++)
                {
                    min = System.Math.Min(min, heights[y, x]);
                    max = System.Math.Max(max, heights[y, x]);
                }
            }

            Assert.Greater(max - min, 0.3f);
        }

        [Test]
        public void Generate_InvalidResolution_Throws()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => IslandHeightmapGenerator.Generate(1, Seed));
        }
    }
}
