using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.World.Grid;
using Varynth.World.Placement;
using Varynth.World.Surface;
using Varynth.World.Terrain;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Pure, deterministic 8-directional A* over one island's cells. Integer costs
    /// only (1000 orthogonal / 1414 diagonal, RoadDirectionExtensions.CostUnits) and
    /// an integer octile heuristic -- no float anywhere in the search's cost/priority
    /// state (round-1 correction 1). Total-order tie-breaking (f, h, X, Z,
    /// directionOrdinal) guarantees identical inputs always produce a bit-for-bit
    /// identical path, never dependent on Dictionary/heap iteration order (round-1
    /// correction 2). Already-existing road edges are always legal/traversable at
    /// normal cost, never re-validated or penalized (round-1 correction 4) -- a route
    /// can freely use/extend existing network instead of detouring around it. Every
    /// new (not-yet-existing) candidate edge is validated through the exact same
    /// RoadPlacementValidator path used for direct placement, so corner-cutting/
    /// diagonal-crossing/terrain rules are enforced identically inside the search.
    /// Chosen over Bresenham/supercover: real obstacle avoidance "for free" by
    /// excluding invalid edges from expansion, room for later cost-weighted
    /// preferences, without a separate detour layer. This is a small, bounded-
    /// per-island textbook A* -- explicitly not a hierarchical/chunked pathfinding
    /// engine (that remains a separate, deliberately deferred later scaling topic,
    /// not solved by anything here).
    /// </summary>
    public static class RoadRouter
    {
        private readonly struct OpenEntry
        {
            public readonly GridCoordinate Cell;
            public readonly int G;
            public readonly int F;
            public readonly int H;
            public readonly RoadDirection IncomingDirection;

            public OpenEntry(GridCoordinate cell, int g, int f, int h, RoadDirection incomingDirection)
            {
                Cell = cell;
                G = g;
                F = f;
                H = h;
                IncomingDirection = incomingDirection;
            }
        }

        public static bool TryFindRoute(
            GridCoordinate start,
            GridCoordinate end,
            RectInt cellBounds,
            IslandSurfaceMap surface,
            RoadGraph graph,
            WorldGrid grid,
            IWorldHeightSource heights,
            RoadDefinition definition,
            IBuildingOccupancyQuery buildingOccupancy,
            RoadPlacementConfig config,
            out IReadOnlyList<GridCoordinate> path)
        {
            if (start.Equals(end))
            {
                path = new List<GridCoordinate> { start };
                return true;
            }

            var bestG = new Dictionary<GridCoordinate, int> { [start] = 0 };
            var cameFrom = new Dictionary<GridCoordinate, GridCoordinate>();
            var closed = new HashSet<GridCoordinate>();
            var open = new List<OpenEntry> { new OpenEntry(start, 0, Octile(start, end), Octile(start, end), RoadDirection.N) };

            while (open.Count > 0)
            {
                var bestIndex = FindBestIndex(open);
                var current = open[bestIndex];
                open.RemoveAt(bestIndex);

                if (bestG.TryGetValue(current.Cell, out var knownBest) && current.G > knownBest)
                {
                    continue; // stale entry
                }

                if (current.Cell.Equals(end))
                {
                    path = ReconstructPath(cameFrom, start, end);
                    return true;
                }

                closed.Add(current.Cell);

                for (var d = 0; d < 8; d++)
                {
                    var direction = (RoadDirection)d;
                    var (dx, dz) = direction.ToDelta();
                    var neighbor = new GridCoordinate(current.Cell.X + dx, current.Cell.Z + dz);

                    if (!cellBounds.Contains(new Vector2Int(neighbor.X, neighbor.Z)) || closed.Contains(neighbor))
                    {
                        continue;
                    }

                    if (!IsEdgeLegal(current.Cell, neighbor, direction, surface, graph, grid, heights, definition, buildingOccupancy, config))
                    {
                        continue;
                    }

                    var tentativeG = current.G + direction.CostUnits();
                    if (!bestG.TryGetValue(neighbor, out var existingG) || tentativeG < existingG)
                    {
                        bestG[neighbor] = tentativeG;
                        cameFrom[neighbor] = current.Cell;
                        var h = Octile(neighbor, end);
                        open.Add(new OpenEntry(neighbor, tentativeG, tentativeG + h, h, direction));
                    }
                }
            }

            path = null;
            return false;
        }

        private static bool IsEdgeLegal(
            GridCoordinate from, GridCoordinate to, RoadDirection direction,
            IslandSurfaceMap surface, RoadGraph graph, WorldGrid grid, IWorldHeightSource heights,
            RoadDefinition definition, IBuildingOccupancyQuery buildingOccupancy, RoadPlacementConfig config)
        {
            if (graph.HasSegmentBetween(from, to))
            {
                return true; // existing road: always traversable, never re-validated/penalized
            }

            var result = RoadPlacementValidator.ValidateSegment(from, to, direction, surface, graph, grid, heights, definition, buildingOccupancy, config);
            return result.IsValid;
        }

        // Integer octile heuristic: 1000*max(dx,dz) + 414*min(dx,dz) -- the integer
        // form of the standard admissible octile distance for 8-directional grids
        // (414 approximates 1414-1000, the extra cost of a diagonal step over two
        // orthogonal steps). Never over-estimates.
        private static int Octile(GridCoordinate a, GridCoordinate b)
        {
            var dx = System.Math.Abs(a.X - b.X);
            var dz = System.Math.Abs(a.Z - b.Z);
            var max = System.Math.Max(dx, dz);
            var min = System.Math.Min(dx, dz);
            return 1000 * max + 414 * min;
        }

        private static int FindBestIndex(List<OpenEntry> open)
        {
            var bestIndex = 0;
            for (var i = 1; i < open.Count; i++)
            {
                if (IsBetter(open[i], open[bestIndex]))
                {
                    bestIndex = i;
                }
            }

            return bestIndex;
        }

        // Total order: f -> h -> X -> Z -> incoming direction ordinal. Never depends
        // on Dictionary/heap iteration order -- identical state always yields the
        // identical expansion order.
        private static bool IsBetter(OpenEntry a, OpenEntry b)
        {
            if (a.F != b.F) return a.F < b.F;
            if (a.H != b.H) return a.H < b.H;
            if (a.Cell.X != b.Cell.X) return a.Cell.X < b.Cell.X;
            if (a.Cell.Z != b.Cell.Z) return a.Cell.Z < b.Cell.Z;
            return a.IncomingDirection < b.IncomingDirection;
        }

        private static List<GridCoordinate> ReconstructPath(Dictionary<GridCoordinate, GridCoordinate> cameFrom, GridCoordinate start, GridCoordinate end)
        {
            var reversed = new List<GridCoordinate> { end };
            var current = end;
            while (!current.Equals(start))
            {
                current = cameFrom[current];
                reversed.Add(current);
            }

            reversed.Reverse();
            return reversed;
        }
    }
}
