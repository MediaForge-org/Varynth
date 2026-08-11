using Varynth.Core.Common;

namespace Varynth.Core.Definitions.Buildings
{
    /// <summary>
    /// Minimal building content definition -- the first real (non-test)
    /// IContentDefinition in the project. Deliberately scoped to only what building
    /// placement needs (Phase 2C): footprint, rotation support (implied -- every
    /// building can rotate through BuildingRotation), a presentation-layer visual
    /// lookup key, and the one placement-rule extensibility flag placement validation
    /// currently needs. No cost/upkeep/workforce/goods/upgrade fields -- those are
    /// separate concerns layered on in later packages once economy exists
    /// (DATA_SCHEMA.md's own BuildingDefinition sketch treats them as separate too).
    /// </summary>
    public sealed class BuildingDefinition : IContentDefinition
    {
        public ContentId Id { get; }
        public LocalizationKey NameKey { get; }
        public int FootprintWidth { get; }
        public int FootprintLength { get; }

        /// <summary>
        /// Presentation-layer lookup key for the blockout mesh/material to show for
        /// this definition (ghost preview and real placed instances alike). Deliberately
        /// just a string, not a Prefab/Material/GameObject reference -- keeps this type
        /// engine-reference-free and is the "Prefab Reference Boundary" a later real
        /// asset-reference system (Addressables key, ContentReference&lt;PrefabDefinition&gt;, ...)
        /// can replace without changing this type's shape.
        /// </summary>
        public string PrototypeVisualId { get; }

        /// <summary>
        /// Placement-rule extensibility point: false by default (no building may occupy
        /// a Coast cell) per the brief's explicit "keine Coast Cells, außer ein
        /// späteres Gebäude erlaubt das ausdrücklich" -- a future harbor-style building
        /// would set this true.
        /// </summary>
        public bool AllowsCoastPlacement { get; }

        public BuildingDefinition(
            ContentId id,
            LocalizationKey nameKey,
            int footprintWidth,
            int footprintLength,
            string prototypeVisualId,
            bool allowsCoastPlacement = false)
        {
            Id = id;
            NameKey = nameKey;
            FootprintWidth = footprintWidth;
            FootprintLength = footprintLength;
            PrototypeVisualId = prototypeVisualId ?? string.Empty;
            AllowsCoastPlacement = allowsCoastPlacement;
        }
    }
}
