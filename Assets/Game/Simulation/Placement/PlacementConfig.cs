namespace Varynth.World.Placement
{
    /// <summary>
    /// Prototype placement-rule thresholds -- mirrors SurfaceClassificationConfig's
    /// style (plain POCO, documented non-final values). No binding spec exists for
    /// a footprint height-variation rule; MaxFootprintHeightVariation is a documented
    /// prototype constant, deliberately slightly above CoastBandHeight (3) so gently
    /// sloped ground stays placeable.
    /// </summary>
    public sealed class PlacementConfig
    {
        public float MaxFootprintHeightVariation = 4f;
    }
}
