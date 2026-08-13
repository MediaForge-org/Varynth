using Varynth.Core.Common;
using Varynth.World.Placement;

namespace Varynth.Core.Simulation.Boundary
{
    /// <summary>
    /// Narrow, engine-free read/preview surface for building placement (Phase 2E
    /// point 2). Presentation depends on this interface, never on the concrete
    /// ManagedSimulation class -- a future NativeSimulationBridge implements the same
    /// interface. Read-only by construction (no mutating members); confirmed
    /// mutation always goes through ISimulation.Submit(PlaceBuildingCommand/...).
    /// </summary>
    public interface ISimulationPlacementQueries
    {
        /// <summary>Local, read-only preview validation for ghost/drag-preview feedback -- never mutates state.</summary>
        PlacementValidationResult ValidateBuildingPlacement(ContentId definitionId, GridCoordinate origin, BuildingRotation rotation);

        /// <summary>
        /// Which island (if any) a cell belongs to. Returns a plain array-lookup
        /// index (Phase 2E point 8: an index may exist for array lookups -- it is not
        /// gameplay identity, IslandId is), matching the existing scene-builder-order
        /// Player Placement Grid array this already drives.
        /// </summary>
        bool TryFindIslandIndex(GridCoordinate cell, out int islandIndex);

        /// <summary>Read-only occupancy lookup -- used for e.g. resolving which instance a Delete-press under the cursor targets.</summary>
        bool TryGetOccupantAt(GridCoordinate cell, out BuildingInstanceId occupant);

        /// <summary>
        /// Authoritative-state-derived read (optional Presentation feedback, e.g. a
        /// CONNECTED/NOT CONNECTED label) -- internally resolves the instance,
        /// definition, and current road state without ever exposing a live
        /// RoadNetworkState to Presentation. False for an unknown instance id.
        /// </summary>
        bool IsBuildingConnectedToRoad(BuildingInstanceId instanceId);
    }
}
