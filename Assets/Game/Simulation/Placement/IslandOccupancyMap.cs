using System;
using System.Collections.Generic;
using Varynth.Core.Common;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Dense, per-island occupancy grid -- same shape as IslandSurfaceMap (flat array,
    /// O(1) local-index lookup, no per-cell objects). Stores raw ulong instance-id
    /// values; 0 (BuildingInstanceId.None) means unoccupied. Occupy is atomic: it
    /// re-validates every cell before writing any of them, so a rejected batch never
    /// leaves partial occupancy behind.
    /// </summary>
    public sealed class IslandOccupancyMap
    {
        private readonly ulong[] _occupants;

        public GridCoordinate OriginCell { get; }
        public int Width { get; }
        public int Height { get; }

        public IslandOccupancyMap(GridCoordinate originCell, int width, int height)
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
            _occupants = new ulong[width * height];
        }

        public bool TryGetOccupant(GridCoordinate cell, out BuildingInstanceId occupant)
        {
            if (!TryGetIndex(cell, out var index))
            {
                occupant = BuildingInstanceId.None;
                return false;
            }

            var raw = _occupants[index];
            occupant = BuildingInstanceId.FromRaw(raw);
            return raw != 0;
        }

        public bool CanOccupy(IReadOnlyList<GridCoordinate> cells)
        {
            for (var i = 0; i < cells.Count; i++)
            {
                if (!TryGetIndex(cells[i], out var index) || _occupants[index] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Occupy(IReadOnlyList<GridCoordinate> cells, BuildingInstanceId id)
        {
            if (!CanOccupy(cells))
            {
                throw new InvalidOperationException("Cannot occupy: at least one cell is outside this map's bounds or already occupied.");
            }

            for (var i = 0; i < cells.Count; i++)
            {
                TryGetIndex(cells[i], out var index);
                _occupants[index] = id.Value;
            }
        }

        public void Release(IReadOnlyList<GridCoordinate> cells)
        {
            for (var i = 0; i < cells.Count; i++)
            {
                if (TryGetIndex(cells[i], out var index))
                {
                    _occupants[index] = 0;
                }
            }
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
