using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Pure static placement validation. Checks every applicable rule and returns all
    /// matching issues at once (not just the first failure) -- "strukturiertes
    /// Ergebnis... nicht nur bool" per the brief.
    /// </summary>
    public static class PlacementValidator
    {
        public static PlacementValidationResult Validate(
            IReadOnlyList<GridCoordinate> footprintCells,
            IslandSurfaceMap surface,
            IslandOccupancyMap occupancy,
            IWorldHeightSource heights,
            WorldGrid grid,
            BuildingDefinition definition,
            PlacementConfig config,
            IRoadOccupancyQuery roadOccupancy = null)
        {
            var issues = PlacementIssue.None;
            var minHeight = float.MaxValue;
            var maxHeight = float.MinValue;
            var anyHeightSample = false;

            foreach (var cell in footprintCells)
            {
                if (!surface.TryGetFlags(cell, out var flags))
                {
                    issues |= PlacementIssue.OutsideSurfaceMap;
                    continue;
                }

                if ((flags & SurfaceCellFlags.Water) != 0)
                {
                    issues |= PlacementIssue.Water;
                }

                if ((flags & SurfaceCellFlags.Coast) != 0 && !definition.AllowsCoastPlacement)
                {
                    issues |= PlacementIssue.Coast;
                }

                if ((flags & SurfaceCellFlags.RockOrSteep) != 0)
                {
                    issues |= PlacementIssue.RockOrSteep;
                }

                if ((flags & SurfaceCellFlags.Buildable) == 0
                    && (flags & SurfaceCellFlags.Coast) == 0)
                {
                    issues |= PlacementIssue.NotBuildable;
                }

                if (occupancy.TryGetOccupant(cell, out _))
                {
                    issues |= PlacementIssue.AlreadyOccupied;
                }

                if (roadOccupancy != null && roadOccupancy.IsCellRoadOccupied(cell))
                {
                    issues |= PlacementIssue.RoadOccupied;
                }

                var center = grid.CellToWorldCenter(cell);
                if (heights.TryGetHeight(center.x, center.y, out var height))
                {
                    if (height < minHeight) minHeight = height;
                    if (height > maxHeight) maxHeight = height;
                    anyHeightSample = true;
                }
            }

            if (anyHeightSample && (maxHeight - minHeight) > config.MaxFootprintHeightVariation)
            {
                issues |= PlacementIssue.HeightVariationTooLarge;
            }

            return issues == PlacementIssue.None
                ? PlacementValidationResult.Valid
                : PlacementValidationResult.Invalid(issues);
        }
    }
}
