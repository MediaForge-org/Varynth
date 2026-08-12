using Varynth.Core.Common;

namespace Varynth.World.Roads
{
    /// <summary>
    /// Small, neutral, read-only query so Building validation can check for an
    /// existing road without ArchipelagoPlacementState holding a persistent reference
    /// to RoadNetworkState (the two world-state systems never own/reference each other
    /// -- this interface is composed at the call site instead, same as
    /// IBuildingOccupancyQuery for the opposite direction).
    /// </summary>
    public interface IRoadOccupancyQuery
    {
        bool IsCellRoadOccupied(GridCoordinate cell);
    }
}
