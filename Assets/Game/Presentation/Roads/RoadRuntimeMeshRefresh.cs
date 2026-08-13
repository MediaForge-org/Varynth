using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Core.Simulation.Boundary;
using Varynth.World.Grid;
using Varynth.World.Roads;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// Phase 2E: the explicit runtime road-mesh update chain no longer reads the live
    /// authoritative RoadGraph (RoadNetworkState is owned exclusively by
    /// ManagedSimulation now, never exposed to Presentation -- point 3). Instead, for
    /// each island whose ISimulationRoadQueries.GetRoadStateVersion actually changed,
    /// this reconstructs a disposable RoadGraph replica from GetSnapshot().Roads (via
    /// the existing, unchanged RoadGraph.AddSegment, which already derives node
    /// connectivity masks itself) and feeds that into the existing, unchanged
    /// RoadMeshBuilder -- no mesh/junction-degree logic duplicated. Uses Presentation's
    /// own live WorldGrid/IWorldHeightSource (continuous Terrain sampling) for the
    /// actual visual terrain-following, not the coarse baked authoritative height
    /// source ManagedSimulation uses for validation.
    /// </summary>
    public static class RoadRuntimeMeshRefresh
    {
        public static void RefreshFromSnapshot(
            SimulationSnapshot snapshot,
            ISimulationRoadQueries roadQueries,
            int[] lastAppliedStateVersions,
            RoadNetworkDisplay[] displays,
            ContentRegistry<RoadDefinition> registry,
            Varynth.Core.Common.ContentId roadDefinitionId,
            WorldGrid grid,
            IWorldHeightSource heights)
        {
            if (displays == null || lastAppliedStateVersions == null || !registry.TryGet(roadDefinitionId, out var definition))
            {
                return;
            }

            for (var i = 0; i < displays.Length && i < lastAppliedStateVersions.Length; i++)
            {
                var version = roadQueries.GetRoadStateVersion(i);
                if (version == lastAppliedStateVersions[i])
                {
                    continue;
                }

                lastAppliedStateVersions[i] = version;

                if (displays[i] == null)
                {
                    continue;
                }

                var islandId = roadQueries.GetIslandId(i);
                var replica = new RoadGraph();
                foreach (var segment in snapshot.Roads)
                {
                    if (segment.Island == islandId)
                    {
                        replica.AddSegment(segment.SegmentId, segment.DefinitionId, segment.From, segment.To, segment.Direction, segment.Owner);
                    }
                }

                var mesh = RoadMeshBuilder.BuildIslandMesh(grid, replica, definition, heights, RoadVisualConfig.RenderClearance);
                displays[i].UpdateGeometry(mesh);
            }
        }
    }
}
