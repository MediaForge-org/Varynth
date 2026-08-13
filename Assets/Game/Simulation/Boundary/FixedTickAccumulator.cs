namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Pure, stateless fixed-step tick math (mirrors the CameraRigMath precedent:
    /// math extracted out of the MonoBehaviour so it's testable without a scene).
    /// UnitySimulationDriver is the only caller; it owns the actual accumulated-time
    /// state and feeds it in/out each frame.
    /// </summary>
    public static class FixedTickAccumulator
    {
        /// <summary>
        /// How many discrete ticks are due given accumulatedSeconds of real time
        /// since the last call. Clamped to MaxCatchUpTicksPerFrame; under that clamp,
        /// the excess accumulated time is discarded entirely (not carried forward) --
        /// the standard fixed-step spiral-of-death guard: under sustained overload,
        /// simulated time deliberately falls behind real time rather than the
        /// accumulator (and thus the catch-up backlog) growing without bound.
        /// </summary>
        public static int ComputeDueTicks(double accumulatedSeconds, SimulationTickConfig config, out double remainingSeconds)
        {
            if (accumulatedSeconds <= 0.0)
            {
                remainingSeconds = accumulatedSeconds < 0.0 ? 0.0 : accumulatedSeconds;
                return 0;
            }

            var tickDuration = config.TickDurationSeconds;
            var dueTicks = (int)(accumulatedSeconds / tickDuration);

            if (dueTicks > config.MaxCatchUpTicksPerFrame)
            {
                dueTicks = config.MaxCatchUpTicksPerFrame;
                remainingSeconds = 0.0;
            }
            else
            {
                remainingSeconds = accumulatedSeconds - dueTicks * tickDuration;
            }

            return dueTicks;
        }

        /// <summary>0..1 fraction of the way into the next not-yet-due tick -- unused visually in 0.2.3 (buildings/roads are static) but computed/tested for future interpolation (people/vehicles/ships).</summary>
        public static float ComputeInterpolationAlpha(double remainingSeconds, SimulationTickConfig config)
        {
            var alpha = (float)(remainingSeconds / config.TickDurationSeconds);
            if (alpha < 0f) return 0f;
            if (alpha > 1f) return 1f;
            return alpha;
        }
    }
}
