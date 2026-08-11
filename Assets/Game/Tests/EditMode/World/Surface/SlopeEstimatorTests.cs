using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class SlopeEstimatorTests
    {
        private sealed class FlatHeightSource : IWorldHeightSource
        {
            private readonly float _height;
            public FlatHeightSource(float height) { _height = height; }
            public float GetHeightAt(float worldX, float worldZ) => _height;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = _height; return true; }
        }

        private sealed class SlopedAlongXHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => worldX;
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = worldX; return true; }
        }

        private sealed class NoDataHeightSource : IWorldHeightSource
        {
            public float GetHeightAt(float worldX, float worldZ) => throw new System.InvalidOperationException();
            public bool TryGetHeight(float worldX, float worldZ, out float height) { height = default; return false; }
        }

        /// <summary>Valid only at a cell's own center and its north neighbor -- everything
        /// else (south/east/west, e.g. off the edge of an island into open water) has no data.</summary>
        private sealed class EdgePlateauHeightSource : IWorldHeightSource
        {
            private const float Height = 50f;

            public float GetHeightAt(float worldX, float worldZ) => Height;

            public bool TryGetHeight(float worldX, float worldZ, out float height)
            {
                if (Mathf.Approximately(worldX, 2f) && (Mathf.Approximately(worldZ, 2f) || Mathf.Approximately(worldZ, 6f)))
                {
                    height = Height;
                    return true;
                }

                height = default;
                return false;
            }
        }

        [Test]
        public void FlatTerrain_ReturnsNearZeroSlope()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FlatHeightSource(10f);

            var slope = SlopeEstimator.EstimateSlopeDegrees(heights, grid, new GridCoordinate(0, 0));

            Assert.AreEqual(0f, slope, 1e-4f);
        }

        [Test]
        public void SlopedTerrain_ReturnsExpectedAngle()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new SlopedAlongXHeightSource();

            // Center (2,2): height 2. East (6,2): height 6, delta 4. CellSize 4 -> atan(4/4) = 45deg.
            var slope = SlopeEstimator.EstimateSlopeDegrees(heights, grid, new GridCoordinate(0, 0));

            Assert.AreEqual(45f, slope, 0.5f);
        }

        [Test]
        public void MissingCenterSample_ReturnsNeutralZero()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new NoDataHeightSource();

            var slope = SlopeEstimator.EstimateSlopeDegrees(heights, grid, new GridCoordinate(0, 0));

            Assert.AreEqual(0f, slope, 1e-4f);
        }

        [Test]
        public void MissingNeighborSamples_AreExcludedNotFabricatedAsZero()
        {
            // Regression test: a missing neighbor (off the edge of this island's terrain)
            // must never be treated as height 0 -- that would fabricate an artificial cliff.
            // Here only the center and its north neighbor have data (both height 50, flat);
            // south/east/west are missing. If they were wrongly fabricated as height 0, the
            // computed slope would spike towards ~90 degrees. Correctly excluding them, the
            // only valid comparison (north) is flat, so the result must stay ~0.
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new EdgePlateauHeightSource();

            var slope = SlopeEstimator.EstimateSlopeDegrees(heights, grid, new GridCoordinate(0, 0));

            Assert.AreEqual(0f, slope, 1e-4f);
        }
    }
}
