namespace Varynth.World.Roads
{
    /// <summary>
    /// Prototype road-placement thresholds -- mirrors PlacementConfig's style (plain
    /// POCO, documented non-final values). MaxSegmentSlopeDegrees stays a float
    /// deliberately: it's a presentation-adjacent tuning threshold compared against a
    /// float height sample, not part of the integer cost/routing state.
    /// </summary>
    public sealed class RoadPlacementConfig
    {
        public float MaxSegmentSlopeDegrees = 30f;
    }
}
