using System;

namespace Varynth.World.Terrain
{
    /// <summary>
    /// Pure, deterministic heightmap generator for the Phase 2A prototype island.
    /// Never uses UnityEngine.Random -- a given seed + resolution always produces
    /// the exact same output, which is required both for reproducible builds and
    /// for a determinism unit test. Output is normalized 0..1, matching Unity's
    /// TerrainData.SetHeights expectation.
    /// </summary>
    public static class IslandHeightmapGenerator
    {
        public static float[,] Generate(
            int resolution,
            int seed,
            float islandRadius01 = 0.55f,
            float coastNoiseStrength = 0.16f,
            int octaves = 4,
            float persistence = 0.5f,
            float lacunarity = 2f)
        {
            if (resolution < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(resolution), resolution, "Resolution must be at least 2.");
            }

            var heights = new float[resolution, resolution];

            for (var y = 0; y < resolution; y++)
            {
                for (var x = 0; x < resolution; x++)
                {
                    var nx = (x / (float)(resolution - 1)) * 2f - 1f;
                    var ny = (y / (float)(resolution - 1)) * 2f - 1f;
                    var radius = MathF.Sqrt(nx * nx + ny * ny);
                    var angle = MathF.Atan2(ny, nx);

                    var coastPerturb = Fbm(
                        MathF.Cos(angle) * 2f + 10f,
                        MathF.Sin(angle) * 2f + 10f,
                        seed + 9001,
                        octaves: 2,
                        persistence: 0.5f,
                        lacunarity: 2f) * 2f - 1f;

                    var effectiveRadius = islandRadius01 + coastPerturb * coastNoiseStrength;
                    var falloff = Clamp01(1f - radius / effectiveRadius);
                    falloff = SmoothStep(falloff);

                    var elevationNoise = Fbm(nx * 2.5f, ny * 2.5f, seed, octaves, persistence, lacunarity);

                    var height01 = falloff * (0.55f + 0.45f * elevationNoise);
                    heights[y, x] = Clamp01(height01);
                }
            }

            return heights;
        }

        private static float Fbm(float x, float y, int seed, int octaves, float persistence, float lacunarity)
        {
            float total = 0f;
            float amplitude = 1f;
            float frequency = 1f;
            float maxValue = 0f;

            for (var i = 0; i < octaves; i++)
            {
                total += ValueNoise(x * frequency, y * frequency, seed + i * 1013) * amplitude;
                maxValue += amplitude;
                amplitude *= persistence;
                frequency *= lacunarity;
            }

            return maxValue > 0f ? total / maxValue : 0f;
        }

        private static float ValueNoise(float x, float y, int seed)
        {
            var x0 = FloorToInt(x);
            var y0 = FloorToInt(y);
            var x1 = x0 + 1;
            var y1 = y0 + 1;

            var tx = x - x0;
            var ty = y - y0;

            var v00 = Hash(x0, y0, seed);
            var v10 = Hash(x1, y0, seed);
            var v01 = Hash(x0, y1, seed);
            var v11 = Hash(x1, y1, seed);

            var sx = SmoothStep(tx);
            var sy = SmoothStep(ty);

            var ix0 = Lerp(v00, v10, sx);
            var ix1 = Lerp(v01, v11, sx);
            return Lerp(ix0, ix1, sy);
        }

        /// <summary>
        /// Deterministic 0..1 hash, reused by ResourceCandidateGenerator/
        /// VegetationCandidateGenerator for seed-based candidate selection so the
        /// project has one hash implementation, not three near-duplicates.
        /// </summary>
        internal static float Hash(int x, int y, int seed)
        {
            unchecked
            {
                var h = seed;
                h = h * 374761393 + x * 668265263;
                h = h * 374761393 + y * 668265263;
                h = (h ^ (h >> 13)) * 1274126177;
                h ^= h >> 16;
                return (h & 0x7fffffff) / (float)int.MaxValue;
            }
        }

        private static int FloorToInt(float value)
        {
            return (int)MathF.Floor(value);
        }

        private static float SmoothStep(float t)
        {
            t = Clamp01(t);
            return t * t * (3f - 2f * t);
        }

        private static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        private static float Clamp01(float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }
    }
}
