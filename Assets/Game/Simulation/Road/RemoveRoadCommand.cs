using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Road
{
    /// <summary>Single-segment removal (prototype UX decision, see plan B10).</summary>
    public sealed class RemoveRoadCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }
        public RoadSegmentId TargetSegment { get; }

        public RemoveRoadCommand(PlayerId issuedBy, GameTick issuedAtTick, RoadSegmentId targetSegment)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
            TargetSegment = targetSegment;
        }
    }
}
