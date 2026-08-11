namespace Varynth.World.Placement
{
    public readonly struct PlacementValidationResult
    {
        public bool IsValid { get; }
        public PlacementIssue Issues { get; }

        public PlacementValidationResult(bool isValid, PlacementIssue issues)
        {
            IsValid = isValid;
            Issues = issues;
        }

        public static readonly PlacementValidationResult Valid = new PlacementValidationResult(true, PlacementIssue.None);

        public static PlacementValidationResult Invalid(PlacementIssue issues)
        {
            return new PlacementValidationResult(false, issues);
        }
    }
}
