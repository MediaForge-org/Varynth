namespace Varynth.Core.Common
{
    /// <summary>
    /// The 8 fixed grid-adjacency directions a road segment can use. Deliberately not
    /// a float angle -- gameplay/routing identity stays integer-grid-based (see
    /// RoadDirectionExtensions.CostUnits, which is likewise integer, never float).
    /// </summary>
    public enum RoadDirection
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    public static class RoadDirectionExtensions
    {
        /// <summary>
        /// Fixed-point integer edge costs -- 1000 orthogonal, 1414 diagonal (a
        /// fixed-point stand-in for 1000*sqrt(2), a literal constant, never computed
        /// via Mathf.Sqrt/Sqrt2 at runtime). No float participates in deterministic
        /// road/routing state; this is the single authoritative source for segment
        /// cost and for the A* octile heuristic.
        /// </summary>
        public const int OrthogonalCostUnits = 1000;
        public const int DiagonalCostUnits = 1414;

        public static int CostUnits(this RoadDirection direction)
        {
            return direction.IsDiagonal() ? DiagonalCostUnits : OrthogonalCostUnits;
        }

        public static bool IsDiagonal(this RoadDirection direction)
        {
            switch (direction)
            {
                case RoadDirection.NE:
                case RoadDirection.SE:
                case RoadDirection.SW:
                case RoadDirection.NW:
                    return true;
                default:
                    return false;
            }
        }

        public static RoadDirection Opposite(this RoadDirection direction)
        {
            switch (direction)
            {
                case RoadDirection.N: return RoadDirection.S;
                case RoadDirection.NE: return RoadDirection.SW;
                case RoadDirection.E: return RoadDirection.W;
                case RoadDirection.SE: return RoadDirection.NW;
                case RoadDirection.S: return RoadDirection.N;
                case RoadDirection.SW: return RoadDirection.NE;
                case RoadDirection.W: return RoadDirection.E;
                default: return RoadDirection.SE;
            }
        }

        public static (int Dx, int Dz) ToDelta(this RoadDirection direction)
        {
            switch (direction)
            {
                case RoadDirection.N: return (0, 1);
                case RoadDirection.NE: return (1, 1);
                case RoadDirection.E: return (1, 0);
                case RoadDirection.SE: return (1, -1);
                case RoadDirection.S: return (0, -1);
                case RoadDirection.SW: return (-1, -1);
                case RoadDirection.W: return (-1, 0);
                default: return (-1, 1);
            }
        }

        /// <summary>
        /// True (with the resolved direction) only for the 8 unit deltas -- no float
        /// angles anywhere, per the explicit "keine Floatwinkel als Gameplay-
        /// Identität" requirement.
        /// </summary>
        public static bool TryFromDelta(int dx, int dz, out RoadDirection direction)
        {
            if (dx < -1 || dx > 1 || dz < -1 || dz > 1 || (dx == 0 && dz == 0))
            {
                direction = default;
                return false;
            }

            switch ((dx, dz))
            {
                case (0, 1): direction = RoadDirection.N; return true;
                case (1, 1): direction = RoadDirection.NE; return true;
                case (1, 0): direction = RoadDirection.E; return true;
                case (1, -1): direction = RoadDirection.SE; return true;
                case (0, -1): direction = RoadDirection.S; return true;
                case (-1, -1): direction = RoadDirection.SW; return true;
                case (-1, 0): direction = RoadDirection.W; return true;
                default: direction = RoadDirection.NW; return true;
            }
        }
    }
}
