using System;
using Varynth.Core.Common;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Dense, speed-/memory-efficient surface classification for one island's cell
    /// footprint. A single SurfaceCellFlags[] array (one byte per cell), global
    /// GridCoordinate converted to a local O(1) array index -- no per-cell objects,
    /// no Dictionary&lt;GridCoordinate, HeavyObject&gt;.
    /// </summary>
    public sealed class IslandSurfaceMap
    {
        private readonly SurfaceCellFlags[] _flags;

        public GridCoordinate OriginCell { get; }
        public int Width { get; }
        public int Height { get; }

        public IslandSurfaceMap(GridCoordinate originCell, int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be positive.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be positive.");
            }

            OriginCell = originCell;
            Width = width;
            Height = height;
            _flags = new SurfaceCellFlags[width * height];
        }

        private IslandSurfaceMap(GridCoordinate originCell, int width, int height, SurfaceCellFlags[] flags)
        {
            OriginCell = originCell;
            Width = width;
            Height = height;
            _flags = flags;
        }

        /// <summary>
        /// Wraps an already-classified flags array directly -- a cheap array copy, not
        /// a recompute. Used to reconstruct a runtime IslandSurfaceMap from a serialized
        /// IslandSurfaceRuntimeData asset without re-running SurfaceMapGenerator at
        /// every game start.
        /// </summary>
        public static IslandSurfaceMap FromRawFlags(GridCoordinate originCell, int width, int height, SurfaceCellFlags[] flags)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be positive.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be positive.");
            }

            if (flags == null || flags.Length != width * height)
            {
                throw new ArgumentException($"Expected a flags array of length {width * height}, got {flags?.Length ?? 0}.", nameof(flags));
            }

            var copy = new SurfaceCellFlags[flags.Length];
            System.Array.Copy(flags, copy, flags.Length);
            return new IslandSurfaceMap(originCell, width, height, copy);
        }

        public bool TryGetFlags(GridCoordinate cell, out SurfaceCellFlags flags)
        {
            if (!TryGetIndex(cell, out var index))
            {
                flags = SurfaceCellFlags.None;
                return false;
            }

            flags = _flags[index];
            return true;
        }

        public void SetFlags(GridCoordinate cell, SurfaceCellFlags flags)
        {
            if (!TryGetIndex(cell, out var index))
            {
                throw new ArgumentOutOfRangeException(nameof(cell), cell, "Cell is outside this island's surface map bounds.");
            }

            _flags[index] = flags;
        }

        private bool TryGetIndex(GridCoordinate cell, out int index)
        {
            var localX = cell.X - OriginCell.X;
            var localZ = cell.Z - OriginCell.Z;

            if (localX < 0 || localX >= Width || localZ < 0 || localZ >= Height)
            {
                index = -1;
                return false;
            }

            index = localZ * Width + localX;
            return true;
        }
    }
}
