using System;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Compact per-cell surface classification. One byte per classified cell (not a
    /// GameObject/ScriptableObject/heavy Dictionary entry per cell), so later systems
    /// don't need to recompute terrain height/slope just to know what a cell is.
    /// This is a technical prototype classification -- it says "terraintechnically
    /// buildable", never a final gameplay rule (see PHASE_2B doc §buildability).
    /// </summary>
    [Flags]
    public enum SurfaceCellFlags : byte
    {
        None = 0,
        Water = 1 << 0,
        Coast = 1 << 1,
        Land = 1 << 2,
        RockOrSteep = 1 << 3,
        Buildable = 1 << 4,
        VegetationCandidate = 1 << 5,
        ResourceCandidate = 1 << 6
    }
}
