namespace Varynth.World.Roads
{
    /// <summary>
    /// Single shared source of truth for road *rendering* values that must stay
    /// consistent across every class that builds road-adjacent geometry
    /// (RoadMeshBuilder, RoadPreviewDisplay, RoadRuntimeMeshRefresh's call site) --
    /// specifically to avoid the earlier bug pattern of scattered, independently
    /// hand-picked magic Y-offset literals (0.05f / 0.04f / 0.06f in different
    /// classes) that had no documented relationship to each other. Presentation-only
    /// (float, Unity-adjacent); the deterministic gameplay graph never reads this.
    /// </summary>
    public static class RoadVisualConfig
    {
        /// <summary>
        /// World-unit vertical clearance every road-mesh vertex is rendered above its
        /// own real sampled terrain height. Kept small and constant (never scaled up
        /// as a band-aid for undersampling -- see MaxTessellationSpacing for that).
        /// </summary>
        public const float RenderClearance = 0.05f;

        /// <summary>
        /// Maximum world-unit spacing between adjacent road-mesh sample points, both
        /// along a segment's length and across its width. Chosen finer than this
        /// project's Terrain heightmap sample spacing at prototype island sizes
        /// (roughly 0.4-1.7 m depending on island size/resolution) so the road's own
        /// piecewise-linear surface tracks real terrain relief closely enough that
        /// terrain never pokes up through the road mesh between samples -- the actual
        /// root cause of the "turquoise diamond notches" visual bug (undersampled
        /// road quads vs. the terrain's own much finer triangulation, with the
        /// Placement Grid's translucent cyan overlay showing through the resulting
        /// gaps). Previously only the segment's length was subdivided (2-2.83 m
        /// spacing) and the width was never subdivided at all (a single flat
        /// left-to-right span up to 2*halfWidth wide) -- both axes are now subdivided.
        /// </summary>
        public const float MaxTessellationSpacing = 1f;
    }
}
