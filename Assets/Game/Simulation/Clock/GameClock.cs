namespace Varynth.Core.Simulation.Clock
{
    /// <summary>
    /// Deterministic simulation clock. Ticks are discrete steps advanced explicitly by
    /// a caller -- never Time.deltaTime, DateTime.Now, Stopwatch, or engine frame rate.
    /// A future Unity/presentation driver translates real elapsed time into calls to
    /// Advance()/Step(); that translation is a separate adapter, not part of this type.
    /// </summary>
    public sealed class GameClock
    {
        public GameTick CurrentTick { get; private set; } = GameTick.Zero;

        public bool IsPaused { get; private set; }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        /// <summary>
        /// Normal advance path. Being paused is a normal, controllable runtime state --
        /// not an error -- so this returns false and leaves the tick unchanged instead
        /// of throwing. Overflow still throws (via GameTick.Add), since that is not a
        /// state any caller intends.
        /// </summary>
        public bool Advance(ulong deltaTicks = 1)
        {
            if (IsPaused)
            {
                return false;
            }

            CurrentTick = CurrentTick.Add(deltaTicks);
            return true;
        }

        /// <summary>
        /// Explicit, separately-named bypass of pause for manual/test stepping. Still
        /// overflow-checked.
        /// </summary>
        public void Step(ulong deltaTicks = 1)
        {
            CurrentTick = CurrentTick.Add(deltaTicks);
        }
    }
}
