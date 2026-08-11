namespace Varynth.Core.Common
{
    /// <summary>
    /// The 4 orthogonal placement rotations a building's footprint can be snapped to.
    /// Prototype value: no binding rotation-snap increment exists in the spec (only
    /// that rotation/mirroring must exist "wenn Modell erlaubt") -- 90-degree, 4-state
    /// snapping is a documented, non-final prototype choice.
    /// </summary>
    public enum BuildingRotation
    {
        Deg0 = 0,
        Deg90 = 90,
        Deg180 = 180,
        Deg270 = 270
    }

    public static class BuildingRotationExtensions
    {
        public static float ToDegrees(this BuildingRotation rotation)
        {
            return (float)rotation;
        }

        /// <summary>
        /// True for the two rotations where a footprint's width/length axes swap
        /// (a 3x2 footprint occupies 2x3 cells at 90/270 degrees).
        /// </summary>
        public static bool SwapsWidthAndLength(this BuildingRotation rotation)
        {
            return rotation == BuildingRotation.Deg90 || rotation == BuildingRotation.Deg270;
        }

        public static BuildingRotation Next(this BuildingRotation rotation)
        {
            switch (rotation)
            {
                case BuildingRotation.Deg0: return BuildingRotation.Deg90;
                case BuildingRotation.Deg90: return BuildingRotation.Deg180;
                case BuildingRotation.Deg180: return BuildingRotation.Deg270;
                default: return BuildingRotation.Deg0;
            }
        }
    }
}
