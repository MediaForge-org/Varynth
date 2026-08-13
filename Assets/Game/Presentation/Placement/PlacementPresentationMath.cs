using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;
using Varynth.World.Terrain;

namespace Varynth.Presentation.Placement
{
    /// <summary>
    /// Shared world-space transform math for a footprint's building-preview shape --
    /// used identically by the ghost preview and by real placed-instance spawning, so
    /// the two never visually disagree. Buildings stay upright: only yaw comes from
    /// BuildingRotation, position.y is a single terrain height sample at the
    /// footprint's world center (never tilted to a terrain normal).
    /// </summary>
    public static class PlacementPresentationMath
    {
        public const float BuildingPreviewHeight = 4f;

        public static (Vector3 position, Quaternion rotation, Vector3 scale) ComputeBuildingTransform(
            GridCoordinate origin,
            int effectiveWidthCells,
            int effectiveLengthCells,
            BuildingRotation rotation,
            WorldGrid grid,
            IWorldHeightSource heights)
        {
            var worldWidth = effectiveWidthCells * grid.CellSize;
            var worldLength = effectiveLengthCells * grid.CellSize;
            var centerX = grid.Origin.X + (origin.X + effectiveWidthCells * 0.5f) * grid.CellSize;
            var centerZ = grid.Origin.Z + (origin.Z + effectiveLengthCells * 0.5f) * grid.CellSize;
            var centerY = heights.TryGetHeight(centerX, centerZ, out var height) ? height : 0f;

            var position = new Vector3(centerX, centerY, centerZ);
            var yaw = Quaternion.Euler(0f, rotation.ToDegrees(), 0f);
            var scale = new Vector3(worldWidth, BuildingPreviewHeight, worldLength);
            return (position, yaw, scale);
        }
    }
}
