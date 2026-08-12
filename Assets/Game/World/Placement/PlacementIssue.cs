using System;

namespace Varynth.World.Placement
{
    /// <summary>
    /// All applicable validation failures for one placement attempt, not just the
    /// first one found -- structured result, never a bare bool. Extensible later
    /// (road/harbor/research/ownership checks all add flags here without changing
    /// the shape of PlacementValidationResult).
    /// </summary>
    [Flags]
    public enum PlacementIssue
    {
        None = 0,
        OutsideSurfaceMap = 1 << 0,
        NotBuildable = 1 << 1,
        Water = 1 << 2,
        Coast = 1 << 3,
        RockOrSteep = 1 << 4,
        AlreadyOccupied = 1 << 5,
        HeightVariationTooLarge = 1 << 6,

        /// <summary>
        /// A cell in the footprint is already occupied by a road segment (Phase 2D).
        /// Checked via an injected IRoadOccupancyQuery, never a direct reference to
        /// RoadNetworkState -- see BuildingPlacementCommandHandler/PlacementValidator.
        /// </summary>
        RoadOccupied = 1 << 7
    }
}
