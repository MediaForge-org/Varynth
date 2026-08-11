using System;
using System.Collections.Generic;

namespace Varynth.World.Terrain
{
    /// <summary>
    /// Dispatches height queries to whichever explicitly-registered island terrain's
    /// bounds contain the query point. Built once from a fixed list (never
    /// FindObjectsOfType); a deterministic linear scan is acceptable for the small
    /// (single-digit to low-teens) number of islands this prototype targets.
    ///
    /// TryGetHeight is the authoritative query API wherever "no terrain here" (open
    /// water between islands) is a real possibility. GetHeightAt exists only to
    /// satisfy IWorldHeightSource for callers that assume height always exists; it
    /// throws outside every registered terrain's bounds rather than returning 0f,
    /// because 0 is also sea level -- silently returning it there would make "no
    /// terrain" indistinguishable from "a legitimate sea-level sample".
    /// </summary>
    public sealed class CompositeWorldHeightSource : IWorldHeightSource
    {
        private readonly IReadOnlyList<UnityTerrainHeightSource> _sources;

        public CompositeWorldHeightSource(IReadOnlyList<UnityTerrainHeightSource> sources)
        {
            _sources = sources ?? throw new ArgumentNullException(nameof(sources));
        }

        public float GetHeightAt(float worldX, float worldZ)
        {
            if (TryGetHeight(worldX, worldZ, out var height))
            {
                return height;
            }

            throw new InvalidOperationException(
                $"No registered terrain covers world position ({worldX}, {worldZ}). Use TryGetHeight when the query may legitimately fall outside every island (e.g. open water).");
        }

        public bool TryGetHeight(float worldX, float worldZ, out float height)
        {
            for (var i = 0; i < _sources.Count; i++)
            {
                if (_sources[i].TryGetHeight(worldX, worldZ, out height))
                {
                    return true;
                }
            }

            height = default;
            return false;
        }
    }
}
