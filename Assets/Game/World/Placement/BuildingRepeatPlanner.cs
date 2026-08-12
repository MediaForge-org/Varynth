using System.Collections.Generic;
using Varynth.Core.Common;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Pure, deterministic origin generator for drag/repeat building placement. One
    /// combined algorithm serves both a linear drag (a row of houses) and a
    /// rectangular/area drag (a block of houses) -- when the resolved column or row
    /// count is 1, the result is simply a line; no separate line/area code paths.
    /// Steps by the *effective* (post-rotation) footprint size, not by 1 cell, so
    /// generated origins are always edge-to-edge and never overlap by construction.
    /// </summary>
    public static class BuildingRepeatPlanner
    {
        public static IReadOnlyList<GridCoordinate> PlanOrigins(
            GridCoordinate start, GridCoordinate end, int effectiveWidth, int effectiveLength)
        {
            var dx = end.X - start.X;
            var dz = end.Z - start.Z;

            var countX = System.Math.Max(1, System.Math.Abs(dx) / effectiveWidth + 1);
            var countZ = System.Math.Max(1, System.Math.Abs(dz) / effectiveLength + 1);

            var signX = dx < 0 ? -1 : 1;
            var signZ = dz < 0 ? -1 : 1;

            var origins = new List<GridCoordinate>(countX * countZ);
            for (var j = 0; j < countZ; j++)
            {
                for (var i = 0; i < countX; i++)
                {
                    origins.Add(new GridCoordinate(
                        start.X + i * effectiveWidth * signX,
                        start.Z + j * effectiveLength * signZ));
                }
            }

            return origins;
        }
    }
}
