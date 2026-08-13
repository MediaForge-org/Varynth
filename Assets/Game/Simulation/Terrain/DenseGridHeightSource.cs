using System;
using System.Collections.Generic;
using Varynth.World.Grid;

namespace Varynth.World.Terrain
{
    /// <summary>
    /// Engine-free IWorldHeightSource backed by a dense per-cell baked height array
    /// (Phase 2E) -- the authoritative-validation counterpart to
    /// UnityTerrainHeightSource, which stays live/Unity-only for Presentation-side
    /// continuous mesh sampling. Same "world position -> cell -> O(1) array index"
    /// pattern as IslandSurfaceMap; out-of-bounds queries fail explicitly via
    /// TryGetHeight rather than fabricating a height (mirrors the existing
    /// CompositeWorldHeightSource convention).
    /// </summary>
    public sealed class DenseGridHeightSource : IWorldHeightSource
    {
        private readonly WorldGrid _grid;
        private readonly int _originCellX;
        private readonly int _originCellZ;
        private readonly int _width;
        private readonly int _height;
        private readonly float[] _heights;

        public DenseGridHeightSource(WorldGrid grid, int originCellX, int originCellZ, int width, int height, IReadOnlyList<float> heights)
        {
            if (heights == null || heights.Count != width * height)
            {
                throw new ArgumentException($"Expected a heights array of length {width * height}, got {heights?.Count ?? 0}.", nameof(heights));
            }

            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _originCellX = originCellX;
            _originCellZ = originCellZ;
            _width = width;
            _height = height;
            _heights = new float[heights.Count];
            for (var i = 0; i < heights.Count; i++)
            {
                _heights[i] = heights[i];
            }
        }

        public float GetHeightAt(float worldX, float worldZ)
        {
            if (TryGetHeight(worldX, worldZ, out var height))
            {
                return height;
            }

            throw new InvalidOperationException($"No baked height sample at world ({worldX}, {worldZ}) -- outside this island's baked bounds.");
        }

        public bool TryGetHeight(float worldX, float worldZ, out float height)
        {
            var cell = _grid.WorldToCell(worldX, worldZ);
            var localX = cell.X - _originCellX;
            var localZ = cell.Z - _originCellZ;

            if (localX < 0 || localX >= _width || localZ < 0 || localZ >= _height)
            {
                height = default;
                return false;
            }

            height = _heights[localZ * _width + localX];
            return true;
        }
    }
}
