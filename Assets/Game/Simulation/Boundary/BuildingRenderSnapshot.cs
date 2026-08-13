using Varynth.Core.Common;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Everything Unity needs to render one building instance -- no Transform,
    /// GameObject, Mesh, or Material (Unity resolves those from DefinitionId itself).
    /// Readonly struct of small value types only -- ContentId is the one non-blittable
    /// field (string-backed), used deliberately as an ID, never a display string
    /// (Phase 2E point 24).
    /// </summary>
    public readonly struct BuildingRenderSnapshot
    {
        public BuildingInstanceId InstanceId { get; }
        public ContentId DefinitionId { get; }
        public IslandId Island { get; }
        public GridCoordinate Origin { get; }
        public BuildingRotation Rotation { get; }
        public PlayerId Owner { get; }

        public BuildingRenderSnapshot(BuildingInstanceId instanceId, ContentId definitionId, IslandId island, GridCoordinate origin, BuildingRotation rotation, PlayerId owner)
        {
            InstanceId = instanceId;
            DefinitionId = definitionId;
            Island = island;
            Origin = origin;
            Rotation = rotation;
            Owner = owner;
        }
    }
}
