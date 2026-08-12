using System.Collections.Generic;
using System.Linq;
using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Road
{
    /// <summary>
    /// One command per confirmed route, never one command per segment -- the
    /// route/shape logic is already fully resolved by RoadRouter in Presentation
    /// before this command is constructed. Every field type is engine-reference-free
    /// (no Mouse/Camera/GameObject/Transform/Material/UI is representable even by
    /// accident). Origins are defensively copied at construction -- mutating the
    /// caller's original list afterward can never retroactively change this
    /// command's data.
    /// </summary>
    public sealed class BuildRoadCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }
        public ContentId RoadDefinitionId { get; }
        public IReadOnlyList<GridCoordinate> OrderedPath { get; }

        public BuildRoadCommand(
            PlayerId issuedBy,
            GameTick issuedAtTick,
            ContentId roadDefinitionId,
            IReadOnlyList<GridCoordinate> orderedPath)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
            RoadDefinitionId = roadDefinitionId;
            OrderedPath = (orderedPath ?? System.Array.Empty<GridCoordinate>()).ToArray();
        }
    }
}
