namespace Varynth.World.Grid
{
    /// <summary>
    /// Converts between continuous world positions (X/Z) and integer grid cells.
    /// Pure math, no scene/GameObject/UnityEngine dependency -- fully unit-testable,
    /// and (Phase 2E) engine-free so authoritative simulation state can depend on it
    /// directly. Namespace intentionally kept as Varynth.World.Grid (unchanged) even
    /// though the file now physically lives under Varynth.Core.Definitions/Common --
    /// namespace and assembly membership are independent in C#/Unity, and keeping the
    /// namespace stable avoided a many-file `using` churn across ~35 call sites for a
    /// change that is purely about assembly boundaries, not naming. Presentation-side
    /// Vector3/Vector2 convenience call sites now pass plain floats instead (see
    /// WorldPointer.ToCell, WorldPrototypeSceneBuilder, PlacementPresentationMath).
    /// </summary>
    public sealed class WorldGrid
    {
        public float CellSize { get; }
        public (float X, float Z) Origin { get; }

        public WorldGrid(float cellSize, (float X, float Z) origin)
        {
            if (cellSize <= 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cellSize), cellSize, "Cell size must be positive.");
            }

            CellSize = cellSize;
            Origin = origin;
        }

        public Varynth.Core.Common.GridCoordinate WorldToCell(float worldX, float worldZ)
        {
            var cellX = (int)System.Math.Floor((worldX - Origin.X) / CellSize);
            var cellZ = (int)System.Math.Floor((worldZ - Origin.Z) / CellSize);
            return new Varynth.Core.Common.GridCoordinate(cellX, cellZ);
        }

        public (float X, float Z) CellToWorldCenter(Varynth.Core.Common.GridCoordinate cell)
        {
            var centerX = Origin.X + (cell.X + 0.5f) * CellSize;
            var centerZ = Origin.Z + (cell.Z + 0.5f) * CellSize;
            return (centerX, centerZ);
        }
    }
}
