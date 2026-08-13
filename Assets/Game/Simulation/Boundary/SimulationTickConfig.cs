namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Fixed simulation tick rate, prototype values (documented non-final, per the
    /// project's established "prototype constant" convention -- CellSize=4,
    /// SlopeThreshold=30, etc.). TicksPerSecond=20 sits at the upper end of the
    /// brief's own "10-20 Hz" example range for smoother-feeling responsiveness.
    /// MaxCatchUpTicksPerFrame guards against a spiral of death under sustained
    /// frame-time overload -- see FixedTickAccumulator for the exact clamp behavior.
    /// </summary>
    public sealed class SimulationTickConfig
    {
        public double TicksPerSecond { get; }
        public int MaxCatchUpTicksPerFrame { get; }

        public double TickDurationSeconds => 1.0 / TicksPerSecond;

        public SimulationTickConfig(double ticksPerSecond = 20.0, int maxCatchUpTicksPerFrame = 10)
        {
            TicksPerSecond = ticksPerSecond;
            MaxCatchUpTicksPerFrame = maxCatchUpTicksPerFrame;
        }
    }
}
