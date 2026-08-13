using System.Collections.Generic;
using Varynth.Core.Common;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Narrow, engine-free read/preview surface for road placement (Phase 2E point
    /// 2). No live RoadGraph exposed anywhere (point 3) -- mesh rebuild reads
    /// GetSnapshot().Roads instead and reconstructs a disposable replica via the
    /// existing RoadGraph.AddSegment. GetRoadStateVersion is the gate Presentation
    /// polls to know whether that reconstruction is even necessary this frame.
    /// </summary>
    public interface ISimulationRoadQueries
    {
        /// <summary>Local, read-only route preview -- never mutates state.</summary>
        bool TryFindRoadRoute(ContentId roadDefinitionId, GridCoordinate start, GridCoordinate end, out IReadOnlyList<GridCoordinate> path);

        bool TryFindIslandIndex(GridCoordinate cell, out int islandIndex);

        /// <summary>Bumped only on a real segment add/remove for that island -- independent of GameTick (Phase 2E point 5).</summary>
        int GetRoadStateVersion(int islandIndex);

        /// <summary>Stable gameplay identity for the given array-lookup index -- lets Presentation filter GetSnapshot().Roads down to one island's segments.</summary>
        IslandId GetIslandId(int islandIndex);
    }
}
