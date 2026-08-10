using UnityEngine;
using Varynth.Core.Common;

namespace Varynth.World.Grid
{
    /// <summary>
    /// Converts between continuous world positions (X/Z) and integer grid cells.
    /// Pure math, no scene/GameObject dependency -- fully unit-testable.
    /// </summary>
    public sealed class WorldGrid
    {
        public float CellSize { get; }
        public Vector2 Origin { get; }

        public WorldGrid(float cellSize, Vector2 origin)
        {
            if (cellSize <= 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cellSize), cellSize, "Cell size must be positive.");
            }

            CellSize = cellSize;
            Origin = origin;
        }

        public GridCoordinate WorldToCell(float worldX, float worldZ)
        {
            var cellX = Mathf.FloorToInt((worldX - Origin.x) / CellSize);
            var cellZ = Mathf.FloorToInt((worldZ - Origin.y) / CellSize);
            return new GridCoordinate(cellX, cellZ);
        }

        public GridCoordinate WorldToCell(Vector3 worldPosition)
        {
            return WorldToCell(worldPosition.x, worldPosition.z);
        }

        public Vector2 CellToWorldCenter(GridCoordinate cell)
        {
            var centerX = Origin.x + (cell.X + 0.5f) * CellSize;
            var centerZ = Origin.y + (cell.Z + 0.5f) * CellSize;
            return new Vector2(centerX, centerZ);
        }
    }
}
