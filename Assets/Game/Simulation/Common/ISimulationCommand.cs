using Varynth.Core.Simulation.Clock;

namespace Varynth.Core.Simulation.Common
{
    /// <summary>
    /// Minimal command-boundary seam: Input -> Command -> Simulation. No concrete
    /// commands (BuildBuildingCommand, TradeCommand, ...) exist yet, and this is not
    /// wired into ISimulationSystem/SimulationScheduler in Phase 1C -- Tick() only
    /// ever takes a SimulationContext. This interface exists purely so a later package
    /// can introduce real commands without simulation code needing to depend directly
    /// on local input/UI, which would block a future optional co-op DLC.
    /// </summary>
    public interface ISimulationCommand
    {
        PlayerId IssuedBy { get; }

        GameTick IssuedAtTick { get; }
    }
}
