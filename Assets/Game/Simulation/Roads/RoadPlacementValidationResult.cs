namespace Varynth.World.Roads
{
    /// <summary>Mirrors Varynth.World.Placement.PlacementValidationResult's shape.</summary>
    public readonly struct RoadPlacementValidationResult
    {
        public bool IsValid { get; }
        public RoadPlacementIssue Issues { get; }

        private RoadPlacementValidationResult(bool isValid, RoadPlacementIssue issues)
        {
            IsValid = isValid;
            Issues = issues;
        }

        public static readonly RoadPlacementValidationResult Valid = new RoadPlacementValidationResult(true, RoadPlacementIssue.None);

        public static RoadPlacementValidationResult Invalid(RoadPlacementIssue issues)
        {
            return new RoadPlacementValidationResult(false, issues);
        }
    }
}
