using Varynth.Core.Common;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Small, neutral, read-only query so Road validation can check for an existing
    /// building without RoadNetworkState holding a persistent reference to
    /// ArchipelagoPlacementState (the two world-state systems never own/reference each
    /// other -- this interface is composed at the call site instead).
    /// </summary>
    public interface IBuildingOccupancyQuery
    {
        bool IsCellOccupied(GridCoordinate cell);
    }
}
