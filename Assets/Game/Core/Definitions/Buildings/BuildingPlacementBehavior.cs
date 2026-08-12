namespace Varynth.Core.Definitions.Buildings
{
    /// <summary>
    /// How a building may be placed in the sandbox: a single click, or a drag/repeat
    /// gesture that plans multiple non-overlapping copies at once (Phase 2D). Every
    /// read site branches on this value, never on a definition id/name -- keeps the
    /// distinction data-driven and moddable.
    /// </summary>
    public enum BuildingPlacementBehavior
    {
        Single,
        DragRepeat
    }
}
