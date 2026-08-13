using Varynth.Core.Common;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Rejects a diagonal segment that would geometrically cross an existing
    /// opposite-orientation diagonal inside the same grid square (the classic "X"
    /// case: a "/" pair and a "\" pair of the same square never share a node, so the
    /// graph could otherwise treat them as unrelated while their meshes visually
    /// intersect). Deterministic, integer-grid-based: derives the candidate's grid
    /// square and orientation, and checks whether the opposite-orientation pair of
    /// that same square already has a segment.
    /// </summary>
    public static class DiagonalCrossingRule
    {
        public static bool IsOppositeDiagonalPresent(GridCoordinate from, GridCoordinate to, RoadGraph graph)
        {
            if (!TryGetSquareAndOrientation(from, to, out var squareX, out var squareZ, out var isForwardSlash))
            {
                return false;
            }

            var (oppA, oppB) = isForwardSlash
                ? (new GridCoordinate(squareX, squareZ + 1), new GridCoordinate(squareX + 1, squareZ))
                : (new GridCoordinate(squareX, squareZ), new GridCoordinate(squareX + 1, squareZ + 1));

            return graph.HasSegmentBetween(oppA, oppB);
        }

        private static bool TryGetSquareAndOrientation(GridCoordinate from, GridCoordinate to, out int squareX, out int squareZ, out bool isForwardSlash)
        {
            var dx = to.X - from.X;
            var dz = to.Z - from.Z;

            if (dx == 1 && dz == 1)
            {
                squareX = from.X; squareZ = from.Z; isForwardSlash = true; return true;
            }
            if (dx == -1 && dz == -1)
            {
                squareX = to.X; squareZ = to.Z; isForwardSlash = true; return true;
            }
            if (dx == -1 && dz == 1)
            {
                squareX = to.X; squareZ = from.Z; isForwardSlash = false; return true;
            }
            if (dx == 1 && dz == -1)
            {
                squareX = from.X; squareZ = to.Z; isForwardSlash = false; return true;
            }

            squareX = 0; squareZ = 0; isForwardSlash = false;
            return false;
        }
    }
}
