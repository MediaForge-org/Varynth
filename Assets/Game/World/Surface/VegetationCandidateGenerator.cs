using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Deterministic list of every cell already carrying the VegetationCandidate
    /// flag (set by SurfaceMapGenerator). A pure function of the already-deterministic
    /// surface map -- no additional randomness/salt needed for this foundational
    /// "which cells could carry vegetation" list. No scatter/density/thinning system
    /// yet (brief §26/§52 -- placement foundation only).
    /// </summary>
    public static class VegetationCandidateGenerator
    {
        public static IReadOnlyList<VegetationCandidate> Generate(IslandSurfaceMap map, RectInt cellBounds)
        {
            var results = new List<VegetationCandidate>();

            for (var cz = cellBounds.yMin; cz < cellBounds.yMax; cz++)
            {
                for (var cx = cellBounds.xMin; cx < cellBounds.xMax; cx++)
                {
                    var cell = new GridCoordinate(cx, cz);
                    if (map.TryGetFlags(cell, out var flags) && (flags & SurfaceCellFlags.VegetationCandidate) != 0)
                    {
                        results.Add(new VegetationCandidate(cell));
                    }
                }
            }

            return results;
        }
    }
}
