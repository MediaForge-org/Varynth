using Varynth.Core.Common;

namespace Varynth.World.Surface
{
    /// <summary>
    /// "A resource/mine slot could plausibly go here" -- nothing more. No resource
    /// type, no amount, no ownership, no production. Suitability is a bare 0..1
    /// prototype eligibility score, not a game-balance value (brief §23/§51).
    /// </summary>
    public readonly struct ResourceSlotCandidate
    {
        public GridCoordinate Cell { get; }
        public float Suitability { get; }

        public ResourceSlotCandidate(GridCoordinate cell, float suitability)
        {
            Cell = cell;
            Suitability = suitability;
        }
    }
}
