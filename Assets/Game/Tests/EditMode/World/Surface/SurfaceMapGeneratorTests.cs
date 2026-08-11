using System;
using NUnit.Framework;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.Tests.EditMode.World.Surface
{
    public class SurfaceMapGeneratorTests
    {
        private sealed class FuncHeightSource : IWorldHeightSource
        {
            private readonly Func<float, float, float?> _fn;
            public FuncHeightSource(Func<float, float, float?> fn) { _fn = fn; }

            public float GetHeightAt(float worldX, float worldZ) => _fn(worldX, worldZ) ?? 0f;

            public bool TryGetHeight(float worldX, float worldZ, out float height)
            {
                var value = _fn(worldX, worldZ);
                if (value.HasValue)
                {
                    height = value.Value;
                    return true;
                }

                height = default;
                return false;
            }
        }

        private static SurfaceClassificationConfig DefaultConfig() => new SurfaceClassificationConfig
        {
            SeaLevelWorldY = 0f,
            CoastBandHeight = 3f,
            SlopeThresholdDegrees = 30f
        };

        [Test]
        public void UnderwaterCell_ClassifiedAsWater_NotBuildable()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FuncHeightSource((x, z) => -5f);
            var bounds = new RectInt(0, 0, 1, 1);

            var map = SurfaceMapGenerator.Generate(grid, heights, bounds, DefaultConfig());

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(0, 0), out var flags));
            Assert.IsTrue((flags & SurfaceCellFlags.Water) != 0);
            Assert.IsFalse((flags & SurfaceCellFlags.Buildable) != 0);
        }

        [Test]
        public void NearSeaLevelCell_ClassifiedAsCoast_NotBuildable()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FuncHeightSource((x, z) => 1f); // within 0..CoastBandHeight(3)
            var bounds = new RectInt(0, 0, 1, 1);

            var map = SurfaceMapGenerator.Generate(grid, heights, bounds, DefaultConfig());

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(0, 0), out var flags));
            Assert.IsTrue((flags & SurfaceCellFlags.Coast) != 0);
            Assert.IsFalse((flags & SurfaceCellFlags.Buildable) != 0);
        }

        [Test]
        public void HighButFlatCell_IsBuildable()
        {
            // Proves height alone does not gate Buildable/RockOrSteep -- only slope does.
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FuncHeightSource((x, z) => 200f);
            var bounds = new RectInt(0, 0, 1, 1);

            var map = SurfaceMapGenerator.Generate(grid, heights, bounds, DefaultConfig());

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(0, 0), out var flags));
            Assert.IsTrue((flags & SurfaceCellFlags.Land) != 0);
            Assert.IsFalse((flags & SurfaceCellFlags.RockOrSteep) != 0);
            Assert.IsTrue((flags & SurfaceCellFlags.Buildable) != 0);
        }

        [Test]
        public void SteepCell_ClassifiedAsRockOrSteep_NotBuildable()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FuncHeightSource((x, z) => x * 10f); // steep along X
            var bounds = new RectInt(0, 0, 1, 1);

            var map = SurfaceMapGenerator.Generate(grid, heights, bounds, DefaultConfig());

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(0, 0), out var flags));
            Assert.IsTrue((flags & SurfaceCellFlags.Land) != 0);
            Assert.IsTrue((flags & SurfaceCellFlags.RockOrSteep) != 0);
            Assert.IsFalse((flags & SurfaceCellFlags.Buildable) != 0);
        }

        [Test]
        public void CellWithNoHeightData_ClassifiedAsWater()
        {
            var grid = new WorldGrid(4f, Vector2.zero);
            var heights = new FuncHeightSource((x, z) => null);
            var bounds = new RectInt(0, 0, 1, 1);

            var map = SurfaceMapGenerator.Generate(grid, heights, bounds, DefaultConfig());

            Assert.IsTrue(map.TryGetFlags(new GridCoordinate(0, 0), out var flags));
            Assert.IsTrue((flags & SurfaceCellFlags.Water) != 0);
        }
    }
}
