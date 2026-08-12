using Varynth.Core.Common;

namespace Varynth.Core.Definitions.Roads
{
    /// <summary>
    /// Minimal road content definition (Phase 2D) -- mirrors BuildingDefinition's
    /// shape. No cost/upkeep/speed-modifier/reach-modifier/tier fields; matches
    /// DATA_SCHEMA.md's own RoadDefinition sketch treating those as later,
    /// Balancing-Bible-sourced concerns (kein Stau-/Kreuzungsmodell either).
    /// </summary>
    public sealed class RoadDefinition : IContentDefinition
    {
        public ContentId Id { get; }
        public LocalizationKey NameKey { get; }

        /// <summary>Presentation-layer visual lookup key, same pattern as BuildingDefinition.PrototypeVisualId.</summary>
        public string PrototypeVisualId { get; }

        /// <summary>Prototype value: a single-cell-wide road. Documented non-final.</summary>
        public int LogicalWidthCells { get; }

        /// <summary>
        /// Data-driven form of "AllowedSegmentTypes" for the one 0.2.1 prototype road
        /// tier -- extensible later to a richer flags type without changing this
        /// definition's outer shape.
        /// </summary>
        public bool AllowsDiagonalSegments { get; }

        /// <summary>Mirrors BuildingDefinition.AllowsCoastPlacement -- false by default; a future coast/harbor road opts in via data.</summary>
        public bool AllowsCoastPlacement { get; }

        public RoadDefinition(
            ContentId id,
            LocalizationKey nameKey,
            string prototypeVisualId,
            int logicalWidthCells = 1,
            bool allowsDiagonalSegments = true,
            bool allowsCoastPlacement = false)
        {
            Id = id;
            NameKey = nameKey;
            PrototypeVisualId = prototypeVisualId ?? string.Empty;
            LogicalWidthCells = logicalWidthCells;
            AllowsDiagonalSegments = allowsDiagonalSegments;
            AllowsCoastPlacement = allowsCoastPlacement;
        }
    }
}
