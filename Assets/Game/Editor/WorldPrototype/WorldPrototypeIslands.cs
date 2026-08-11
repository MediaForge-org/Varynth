using System.Collections.Generic;
using UnityEngine;

namespace Varynth.Tooling.Editor.WorldPrototype
{
    /// <summary>
    /// Default set of Phase 2B / Varynth 0.1.1 prototype islands. Generic placeholder
    /// names only -- not Occidentia lore, not guaranteed savegame IDs (brief §9/§31).
    /// Centers are spaced with enough margin that no two terrain+water footprints
    /// overlap; this is a documented prototype layout, not a final map.
    /// Seed/radius/coast-noise/octave/persistence/lacunarity values are deliberately
    /// varied per island so their contour, relief and flat-area character differ
    /// visibly rather than reading as four resized copies of the same shape.
    /// </summary>
    public static class WorldPrototypeIslands
    {
        private const int BaseSeed = 20260809;

        public static List<IslandPrototypeConfig> GetDefaultConfigs()
        {
            return new List<IslandPrototypeConfig>
            {
                new IslandPrototypeConfig
                {
                    // Enlarged and flattened for Phase 2C / Varynth 0.2.0's building
                    // placement sandbox (Docs/04_IMPLEMENTATION/PHASE_2C...): the prior
                    // 260x260 footprint could not fit all 3 prototype buildings side by
                    // side. Widening the terrain (heightmap resolution stays the fixed
                    // 257 shared constant -- see HeightmapResolution) automatically
                    // spreads the same normalized relief noise over a larger physical
                    // area, which flattens real-world slope in degrees without any new
                    // terrain code. Octaves/Persistence reduced and IslandRadius01
                    // raised for a wider, calmer interior plateau; the radial falloff
                    // band near the coastline still yields a natural steep/rocky rim,
                    // preserved on purpose so invalid-placement testing (RockOrSteep,
                    // Coast) remains possible. This is a sandbox-only tuning value, not
                    // a final Varynth island size (see DECISIONS.md).
                    Name = "TestIsland_Large",
                    Center = new Vector2(0f, 0f),
                    TerrainWidth = 440f,
                    TerrainLength = 440f,
                    Seed = BaseSeed + 1,
                    IslandRadius01 = 0.70f,
                    CoastNoiseStrength = 0.12f,
                    Octaves = 3,
                    Persistence = 0.30f,
                    Lacunarity = 2.0f,
                    MaxResourceCandidates = 8
                },
                new IslandPrototypeConfig
                {
                    Name = "TestIsland_Medium",
                    Center = new Vector2(520f, 40f),
                    TerrainWidth = 170f,
                    TerrainLength = 170f,
                    Seed = BaseSeed + 2,
                    IslandRadius01 = 0.55f,
                    CoastNoiseStrength = 0.20f,
                    Octaves = 4,
                    Persistence = 0.55f,
                    Lacunarity = 2.1f,
                    MaxResourceCandidates = 3
                },
                new IslandPrototypeConfig
                {
                    Name = "TestIsland_Small",
                    Center = new Vector2(60f, 480f),
                    TerrainWidth = 110f,
                    TerrainLength = 110f,
                    Seed = BaseSeed + 3,
                    IslandRadius01 = 0.50f,
                    CoastNoiseStrength = 0.10f,
                    Octaves = 3,
                    Persistence = 0.35f,
                    Lacunarity = 1.9f,
                    MaxResourceCandidates = 2
                },
                new IslandPrototypeConfig
                {
                    Name = "TestIsland_Coastal",
                    Center = new Vector2(560f, 470f),
                    TerrainWidth = 200f,
                    TerrainLength = 140f,
                    Seed = BaseSeed + 4,
                    IslandRadius01 = 0.58f,
                    CoastNoiseStrength = 0.26f,
                    Octaves = 4,
                    Persistence = 0.60f,
                    Lacunarity = 2.2f,
                    MaxResourceCandidates = 3
                }
            };
        }
    }
}
