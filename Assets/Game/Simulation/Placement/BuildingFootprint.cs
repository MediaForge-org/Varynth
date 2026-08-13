using System.Collections.Generic;
using Varynth.Core.Common;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Pure, integer-only footprint cell enumeration -- no floats, no dependency on
    /// world origin, deterministic. Handles the width/length swap at 90/270 degrees.
    /// Fully unit-testable without a scene (mirrors WorldGridTests' style).
    /// </summary>
    public static class BuildingFootprint
    {
        public static IReadOnlyList<GridCoordinate> GetOccupiedCells(GridCoordinate origin, int width, int length, BuildingRotation rotation)
        {
            var effectiveWidth = rotation.SwapsWidthAndLength() ? length : width;
            var effectiveLength = rotation.SwapsWidthAndLength() ? width : length;

            var cells = new List<GridCoordinate>(effectiveWidth * effectiveLength);
            for (var dz = 0; dz < effectiveLength; dz++)
            {
                for (var dx = 0; dx < effectiveWidth; dx++)
                {
                    cells.Add(new GridCoordinate(origin.X + dx, origin.Z + dz));
                }
            }

            return cells;
        }
    }
}
