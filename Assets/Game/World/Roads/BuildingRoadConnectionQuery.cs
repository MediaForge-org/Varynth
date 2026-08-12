using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.World.Placement;

namespace Varynth.World.Roads
{
    /// <summary>
    /// "Ist dieses Gebäude an das Straßennetz angeschlossen?" -- only real
    /// side/edge-neighbor adjacency counts, never a diagonal-only corner touch
    /// (explicit decision, matches the user's own stated preference). Computed live
    /// against the current RoadNetworkState every call, never cached -- reports
    /// false again the moment the connecting segment is removed. Rotation is already
    /// baked into the footprint cells (same BuildingFootprint call the ghost/
    /// validator use), so a rotated building's correct edge is checked without
    /// special-casing.
    /// </summary>
    public static class BuildingRoadConnectionQuery
    {
        public static bool IsConnected(BuildingInstance instance, BuildingDefinition definition, RoadNetworkState roads)
        {
            var cells = BuildingFootprint.GetOccupiedCells(instance.Origin, definition.FootprintWidth, definition.FootprintLength, instance.Rotation);
            var occupied = new System.Collections.Generic.HashSet<GridCoordinate>(cells);

            foreach (var cell in cells)
            {
                // Only the 4 orthogonal (edge/side) neighbors -- diagonal-only corner
                // touch is deliberately excluded. Checking every occupied cell's
                // orthogonal neighbors and skipping ones still inside the footprint
                // automatically restricts this to the footprint's outer edge, with
                // no separate "is this an edge cell" detection needed.
                if (TryCheckNeighbor(new GridCoordinate(cell.X + 1, cell.Z), occupied, roads)) return true;
                if (TryCheckNeighbor(new GridCoordinate(cell.X - 1, cell.Z), occupied, roads)) return true;
                if (TryCheckNeighbor(new GridCoordinate(cell.X, cell.Z + 1), occupied, roads)) return true;
                if (TryCheckNeighbor(new GridCoordinate(cell.X, cell.Z - 1), occupied, roads)) return true;
            }

            return false;
        }

        private static bool TryCheckNeighbor(GridCoordinate neighbor, System.Collections.Generic.HashSet<GridCoordinate> occupied, RoadNetworkState roads)
        {
            return !occupied.Contains(neighbor) && roads.IsCellRoadOccupied(neighbor);
        }
    }
}
