using System;

namespace Varynth.World.Roads
{
    /// <summary>
    /// All applicable road-segment validation failures at once -- structured result,
    /// mirrors Varynth.World.Placement.PlacementIssue's shape/philosophy.
    /// </summary>
    [Flags]
    public enum RoadPlacementIssue
    {
        None = 0,
        OutsideSurfaceMap = 1 << 0,
        Water = 1 << 1,
        Coast = 1 << 2,
        RockOrSteep = 1 << 3,
        BuildingOccupied = 1 << 4,
        DuplicateSegment = 1 << 5,
        SlopeTooSteep = 1 << 6,
        CornerCut = 1 << 7,
        DiagonalCrossing = 1 << 8,
        DifferentIsland = 1 << 9
    }
}
