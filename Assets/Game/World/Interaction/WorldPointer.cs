using System;
using System.Collections.Generic;
using UnityEngine;
using Varynth.Core.Common;
using Varynth.World.Grid;

namespace Varynth.World.Interaction
{
    /// <summary>
    /// Pure spatial/raycast helper. Reads no input itself -- the caller (Presentation
    /// layer) supplies the Ray, e.g. from Camera.ScreenPointToRay(Mouse.current.position).
    /// Targets a small, explicitly registered list of Colliders (one per island's
    /// TerrainCollider) rather than a LayerMask/generic Physics.Raycast, so the raycast
    /// unambiguously never hits water or any other future collider that happens to
    /// share a layer -- even if one is added later. A deterministic linear scan over
    /// every registered collider is acceptable for this island count (brief §17); when
    /// multiple islands' colliders could geometrically be hit, the closest valid hit wins.
    /// </summary>
    public sealed class WorldPointer
    {
        private readonly WorldGrid _grid;
        private readonly IReadOnlyList<Collider> _terrainColliders;

        public WorldPointer(WorldGrid grid, IReadOnlyList<Collider> terrainColliders)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _terrainColliders = terrainColliders ?? throw new ArgumentNullException(nameof(terrainColliders));
        }

        public bool TryRaycast(Ray ray, out Vector3 worldPosition)
        {
            var found = false;
            var closestDistance = float.PositiveInfinity;
            worldPosition = default;

            for (var i = 0; i < _terrainColliders.Count; i++)
            {
                var collider = _terrainColliders[i];
                if (collider == null)
                {
                    continue;
                }

                if (collider.Raycast(ray, out var hit, float.PositiveInfinity) && hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    worldPosition = hit.point;
                    found = true;
                }
            }

            return found;
        }

        public GridCoordinate ToCell(Vector3 worldPosition)
        {
            return _grid.WorldToCell(worldPosition);
        }
    }
}
