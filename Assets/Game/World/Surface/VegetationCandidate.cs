using Varynth.Core.Common;

namespace Varynth.World.Surface
{
    /// <summary>
    /// "This cell could in principle carry vegetation" -- placement/suitability
    /// foundation only. No tree instances, no forestry system, no wood production
    /// (brief §26/§52).
    /// </summary>
    public readonly struct VegetationCandidate
    {
        public GridCoordinate Cell { get; }

        public VegetationCandidate(GridCoordinate cell)
        {
            Cell = cell;
        }
    }
}
