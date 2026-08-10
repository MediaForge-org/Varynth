using System;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;

namespace Varynth.World.Interaction
{
    /// <summary>
    /// Pure spatial/raycast helper. Reads no input itself -- the caller (Presentation
    /// layer) supplies the Ray, e.g. from Camera.ScreenPointToRay(Mouse.current.position).
    /// Targets an explicit Collider (the terrain's TerrainCollider) rather than a
    /// LayerMask alone, so the raycast unambiguously never hits water or any other
    /// future collider that happens to share a layer -- even if one is added later.
    /// </summary>
    public sealed class WorldPointer
    {
        private readonly WorldGrid _grid;
        private readonly Collider _terrainCollider;

        public WorldPointer(WorldGrid grid, Collider terrainCollider)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _terrainCollider = terrainCollider ?? throw new ArgumentNullException(nameof(terrainCollider));
        }

        public bool TryRaycast(Ray ray, out Vector3 worldPosition)
        {
            if (_terrainCollider.Raycast(ray, out var hit, float.PositiveInfinity))
            {
                worldPosition = hit.point;
                return true;
            }

            worldPosition = default;
            return false;
        }

        public GridCoordinate ToCell(Vector3 worldPosition)
        {
            return _grid.WorldToCell(worldPosition);
        }
    }
}
