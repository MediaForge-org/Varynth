using System;

namespace Varynth.World.Surface
{
    /// <summary>
    /// Phase 2B / Varynth 0.1.1 prototype thresholds for surface classification.
    /// Deliberately not a final balancing config -- documented as such in
    /// PHASE_2B_ISLAND_TERRAIN_FOUNDATION.md. A cell can be high-altitude but flat
    /// and still buildable: RockOrSteep/Buildable are gated by slope, not raw height.
    /// </summary>
    [Serializable]
    public sealed class SurfaceClassificationConfig
    {
        public float SeaLevelWorldY = 0f;
        public float CoastBandHeight = 3f;
        public float SlopeThresholdDegrees = 30f;
    }
}
