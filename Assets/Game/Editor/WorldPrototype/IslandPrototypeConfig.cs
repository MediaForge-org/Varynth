using UnityEngine;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Plain data describing one prototype island for the Editor scene builder to
    /// generate. Deliberately decoupled from the build logic itself (Phase 2B §9/§31)
    /// so island parameters are not baked as private constants inline in
    /// WorldPrototypeSceneBuilder, and so a future data-driven/mod loading path has
    /// somewhere to plug in without restructuring the builder.
    ///
    /// <see cref="Name"/> is a debug/prototype label only -- not a canonical
    /// savegame/content ID and not an Occidentia lore name.
    /// </summary>
    public sealed class IslandPrototypeConfig
    {
        public string Name;
        public Vector2 Center;
        public float TerrainWidth;
        public float TerrainLength;
        public int Seed;
        public float IslandRadius01;
        public float CoastNoiseStrength;
        public int Octaves;
        public float Persistence;
        public float Lacunarity;

        /// <summary>Prototype tier used only to size resource-candidate counts (brief §24).</summary>
        public int MaxResourceCandidates;
    }
}
