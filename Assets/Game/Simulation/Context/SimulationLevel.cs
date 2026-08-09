using System;

namespace Varynth.Core.Simulation.Context
{
    /// <summary>
    /// Simulation detail level a given tick runs at. Presentation/Rendering is
    /// deliberately NOT a value here -- it is a separate layer entirely (see
    /// ARCHITECTURE.md). No concrete tick frequencies are attached to these values;
    /// that mapping is a later, deliberately deferred decision.
    /// </summary>
    public enum SimulationLevel
    {
        ActiveNear,
        ActiveFar,
        Background
    }

    /// <summary>
    /// Bitmask a system uses to declare which SimulationLevels it participates in.
    /// A plain enum flags check is zero-allocation and avoids any collection/enumerator
    /// overhead in the scheduler's tick hot path.
    /// </summary>
    [Flags]
    public enum SimulationLevelMask
    {
        None = 0,
        ActiveNear = 1 << 0,
        ActiveFar = 1 << 1,
        Background = 1 << 2,
        All = ActiveNear | ActiveFar | Background
    }

    public static class SimulationLevelExtensions
    {
        public static SimulationLevelMask ToMask(this SimulationLevel level)
        {
            switch (level)
            {
                case SimulationLevel.ActiveNear:
                    return SimulationLevelMask.ActiveNear;
                case SimulationLevel.ActiveFar:
                    return SimulationLevelMask.ActiveFar;
                case SimulationLevel.Background:
                    return SimulationLevelMask.Background;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, "Unknown SimulationLevel.");
            }
        }

        /// <summary>
        /// A valid mask has at least one defined level bit set and no bits outside the
        /// defined set (SimulationLevelMask.All). Both "supports nothing" and
        /// "garbage/undefined bits" are treated as invalid -- almost certainly a
        /// misconfiguration, not an intentional state.
        /// </summary>
        public static bool IsValid(this SimulationLevelMask mask)
        {
            return mask != SimulationLevelMask.None && (mask & ~SimulationLevelMask.All) == 0;
        }
    }
}
