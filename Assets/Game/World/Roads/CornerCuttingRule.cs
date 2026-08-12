using Varynth.Core.Common;
using Varynth.World.Placement;
using Varynth.World.Surface;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Diagonal clearance for a prototype width-1 road: the classic pathfinding rule
    /// ("blocked only if both flanking cells are blocked") is too permissive for a
    /// road with real physical presence. Corrected/conservative rule: a diagonal
    /// segment is valid only if BOTH orthogonal flanking cells are passable/free --
    /// a single blocked flanking cell (Water/disallowed Coast/RockOrSteep/building)
    /// already invalidates the diagonal. A later width-aware corridor-clearance check
    /// may relax this once roads carry real width.
    /// </summary>
    public static class CornerCuttingRule
    {
        public static bool IsDiagonalClearanceBlocked(
            GridCoordinate from,
            RoadDirection direction,
            IslandSurfaceMap surface,
            IBuildingOccupancyQuery buildingOccupancy,
            bool allowsCoastPlacement)
        {
            if (!direction.IsDiagonal())
            {
                return false;
            }

            var (dx, dz) = direction.ToDelta();
            var flankingA = new GridCoordinate(from.X + dx, from.Z);
            var flankingB = new GridCoordinate(from.X, from.Z + dz);

            return IsCellBlocked(flankingA, surface, buildingOccupancy, allowsCoastPlacement)
                || IsCellBlocked(flankingB, surface, buildingOccupancy, allowsCoastPlacement);
        }

        private static bool IsCellBlocked(
            GridCoordinate cell, IslandSurfaceMap surface, IBuildingOccupancyQuery buildingOccupancy, bool allowsCoastPlacement)
        {
            if (!surface.TryGetFlags(cell, out var flags))
            {
                return true;
            }

            if ((flags & SurfaceCellFlags.Water) != 0) return true;
            if ((flags & SurfaceCellFlags.RockOrSteep) != 0) return true;
            if ((flags & SurfaceCellFlags.Coast) != 0 && !allowsCoastPlacement) return true;
            if (buildingOccupancy != null && buildingOccupancy.IsCellOccupied(cell)) return true;

            return false;
        }
    }
}
