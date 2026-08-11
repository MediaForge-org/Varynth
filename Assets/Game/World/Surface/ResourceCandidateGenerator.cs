using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Terrain;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Deterministic (seed + fixed salt, never UnityEngine.Random) selection of a
    /// small number of resource/mine slot candidates per island. Prefers
    /// RockOrSteep land (mines belong in rocky/high terrain), falling back to any
    /// eligible Land cell if a small island doesn't have enough rocky cells.
    /// Generation-time only -- not a per-frame computation.
    /// </summary>
    public static class ResourceCandidateGenerator
    {
        private const int SeedSalt = 90001;

        public static IReadOnlyList<ResourceSlotCandidate> Generate(
            IslandSurfaceMap map,
            RectInt cellBounds,
            int seed,
            int maxCandidates,
            int minSpacingCells)
        {
            var eligible = new List<(GridCoordinate cell, float score, bool rocky)>();

            for (var cz = cellBounds.yMin; cz < cellBounds.yMax; cz++)
            {
                for (var cx = cellBounds.xMin; cx < cellBounds.xMax; cx++)
                {
                    var cell = new GridCoordinate(cx, cz);
                    if (!map.TryGetFlags(cell, out var flags))
                    {
                        continue;
                    }

                    if ((flags & SurfaceCellFlags.Land) == 0)
                    {
                        continue;
                    }

                    var rocky = (flags & SurfaceCellFlags.RockOrSteep) != 0;
                    var score = IslandHeightmapGenerator.Hash(cx, cz, seed + SeedSalt);
                    eligible.Add((cell, score, rocky));
                }
            }

            var ordered = eligible
                .OrderByDescending(e => e.rocky)
                .ThenByDescending(e => e.score)
                .ToList();

            var picked = new List<ResourceSlotCandidate>();
            var minSpacingSquared = minSpacingCells * minSpacingCells;

            foreach (var candidate in ordered)
            {
                if (picked.Count >= maxCandidates)
                {
                    break;
                }

                var tooClose = false;
                foreach (var existing in picked)
                {
                    var dx = existing.Cell.X - candidate.cell.X;
                    var dz = existing.Cell.Z - candidate.cell.Z;
                    if (dx * dx + dz * dz < minSpacingSquared)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (tooClose)
                {
                    continue;
                }

                picked.Add(new ResourceSlotCandidate(candidate.cell, candidate.score));
            }

            return picked;
        }
    }
}
