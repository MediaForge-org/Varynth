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
                    Name = "TestIsland_Large",
                    Center = new Vector2(0f, 0f),
                    TerrainWidth = 260f,
                    TerrainLength = 260f,
                    Seed = BaseSeed + 1,
                    IslandRadius01 = 0.62f,
                    CoastNoiseStrength = 0.14f,
                    Octaves = 5,
                    Persistence = 0.45f,
                    Lacunarity = 2.0f,
                    MaxResourceCandidates = 5
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
