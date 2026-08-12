using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.World.Grid;
using Varynth.World.Roads;

namespace Varynth.Presentation.Roads
{
    /// <summary>
    /// The explicit runtime road-mesh update chain (round-2 correction 9): after a
    /// successful RoadCommandHandler mutation, consumes each island's dirty-cell set
    /// and, for every island actually touched, rebuilds that island's mesh and
    /// writes it into the persistent runtime RoadNetworkDisplay Mesh instance (never
    /// AssetDatabase, never a fresh Mesh allocation on the MeshFilter reference --
    /// see RoadNetworkDisplay.UpdateGeometry).
    /// </summary>
    public static class RoadRuntimeMeshRefresh
    {
        public static void RefreshAffectedIslands(
            RoadNetworkState state,
            RoadNetworkDisplay[] displays,
            ContentRegistry<RoadDefinition> registry,
            ContentId roadDefinitionId,
            WorldGrid grid)
        {
            if (displays == null || !registry.TryGet(roadDefinitionId, out var definition))
            {
                return;
            }

            for (var i = 0; i < state.IslandCount && i < displays.Length; i++)
            {
                var dirty = state.ConsumeDirtyCells(i);
                if (dirty.Count == 0 || displays[i] == null)
                {
                    continue;
                }

                var mesh = RoadMeshBuilder.BuildIslandMesh(grid, state.GetGraph(i), definition, state.GetHeights(i), RoadVisualConfig.RenderClearance);
                displays[i].UpdateGeometry(mesh);
            }
        }
    }
}
