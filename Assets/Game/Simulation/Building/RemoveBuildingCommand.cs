using Varynth.Core.Common;
using Varynth.Core.Simulation.Clock;
using Varynth.Core.Simulation.Common;

namespace Varynth.Core.Simulation.Building
{
    /// <summary>
    /// See PlaceBuildingCommand -- same shape/rationale. Targets an existing placed
    /// instance by its stable BuildingInstanceId, never a GameObject reference.
    /// </summary>
    public sealed class RemoveBuildingCommand : ISimulationCommand
    {
        public PlayerId IssuedBy { get; }
        public GameTick IssuedAtTick { get; }
        public BuildingInstanceId TargetInstanceId { get; }

        public RemoveBuildingCommand(PlayerId issuedBy, GameTick issuedAtTick, BuildingInstanceId targetInstanceId)
        {
            IssuedBy = issuedBy;
            IssuedAtTick = issuedAtTick;
            TargetInstanceId = targetInstanceId;
        }
    }
}
