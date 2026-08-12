using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Pure static single-segment road validation, mirrors PlacementValidator's
    /// philosophy (returns every applicable issue, not just the first). Operates on
    /// one island's data (surface/graph/heights) -- RoadNetworkState resolves which
    /// island a cell belongs to and rejects cross-island requests before calling in.
    /// DuplicateSegment applies only to a genuine direct single-segment validation
    /// call -- the atomic route-commit path (RoadNetworkState.TryBuildPath) treats an
    /// already-existing edge as a no-op and never calls this for it, so existing
    /// roads are never flagged as duplicates during a legitimate route-through.
    /// </summary>
    public static class RoadPlacementValidator
    {
        public static RoadPlacementValidationResult ValidateSegment(
            GridCoordinate from,
            GridCoordinate to,
            RoadDirection direction,
            IslandSurfaceMap surface,
            RoadGraph graph,
            WorldGrid grid,
            IWorldHeightSource heights,
            RoadDefinition definition,
            IBuildingOccupancyQuery buildingOccupancy,
            RoadPlacementConfig config)
        {
            var issues = RoadPlacementIssue.None;

            if (!surface.TryGetFlags(from, out var fromFlags) || !surface.TryGetFlags(to, out var toFlags))
            {
                issues |= RoadPlacementIssue.OutsideSurfaceMap;
            }
            else
            {
                CheckCellFlags(fromFlags, definition, ref issues);
                CheckCellFlags(toFlags, definition, ref issues);
            }

            if (buildingOccupancy != null && (buildingOccupancy.IsCellOccupied(from) || buildingOccupancy.IsCellOccupied(to)))
            {
                issues |= RoadPlacementIssue.BuildingOccupied;
            }

            if (graph.HasSegmentBetween(from, to))
            {
                issues |= RoadPlacementIssue.DuplicateSegment;
            }

            if (direction.IsDiagonal())
            {
                if (definition.AllowsDiagonalSegments)
                {
                    if (CornerCuttingRule.IsDiagonalClearanceBlocked(from, direction, surface, buildingOccupancy, definition.AllowsCoastPlacement))
                    {
                        issues |= RoadPlacementIssue.CornerCut;
                    }

                    if (DiagonalCrossingRule.IsOppositeDiagonalPresent(from, to, graph))
                    {
                        issues |= RoadPlacementIssue.DiagonalCrossing;
                    }
                }
                else
                {
                    issues |= RoadPlacementIssue.CornerCut;
                }
            }

            if (heights != null && grid != null && TryGetSlopeDegrees(from, to, grid, heights, out var slopeDegrees)
                && slopeDegrees > config.MaxSegmentSlopeDegrees)
            {
                issues |= RoadPlacementIssue.SlopeTooSteep;
            }

            return issues == RoadPlacementIssue.None ? RoadPlacementValidationResult.Valid : RoadPlacementValidationResult.Invalid(issues);
        }

        private static void CheckCellFlags(SurfaceCellFlags flags, RoadDefinition definition, ref RoadPlacementIssue issues)
        {
            if ((flags & SurfaceCellFlags.Water) != 0) issues |= RoadPlacementIssue.Water;
            if ((flags & SurfaceCellFlags.Coast) != 0 && !definition.AllowsCoastPlacement) issues |= RoadPlacementIssue.Coast;
            if ((flags & SurfaceCellFlags.RockOrSteep) != 0) issues |= RoadPlacementIssue.RockOrSteep;
        }

        private static bool TryGetSlopeDegrees(GridCoordinate from, GridCoordinate to, WorldGrid grid, IWorldHeightSource heights, out float slopeDegrees)
        {
            slopeDegrees = 0f;

            var fromCenter = grid.CellToWorldCenter(from);
            var toCenter = grid.CellToWorldCenter(to);

            if (!heights.TryGetHeight(fromCenter.x, fromCenter.y, out var fromHeight)
                || !heights.TryGetHeight(toCenter.x, toCenter.y, out var toHeight))
            {
                return false;
            }

            var horizontalDistance = UnityEngine.Vector2.Distance(fromCenter, toCenter);
            if (horizontalDistance <= 0f)
            {
                return false;
            }

            slopeDegrees = UnityEngine.Mathf.Atan(UnityEngine.Mathf.Abs(toHeight - fromHeight) / horizontalDistance) * UnityEngine.Mathf.Rad2Deg;
            return true;
        }
    }
}
