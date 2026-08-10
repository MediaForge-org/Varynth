using UnityEngine;

namespace Varynth.World.Terrain
{
    /// <summary>
    /// The one concrete IWorldHeightSource implementation for this prototype,
    /// a thin adapter over UnityEngine.Terrain.SampleHeight.
    /// </summary>
    public sealed class UnityTerrainHeightSource : IWorldHeightSource
    {
        private readonly UnityEngine.Terrain _terrain;

        public UnityTerrainHeightSource(UnityEngine.Terrain terrain)
        {
            _terrain = terrain != null ? terrain : throw new System.ArgumentNullException(nameof(terrain));
        }

        public float GetHeightAt(float worldX, float worldZ)
        {
            var samplePosition = new Vector3(worldX, 0f, worldZ);
            return _terrain.SampleHeight(samplePosition) + _terrain.transform.position.y;
        }

        public bool TryGetHeight(float worldX, float worldZ, out float height)
        {
            var terrainPosition = _terrain.transform.position;
            var size = _terrain.terrainData.size;

            var localX = worldX - terrainPosition.x;
            var localZ = worldZ - terrainPosition.z;

            if (localX < 0f || localX > size.x || localZ < 0f || localZ > size.z)
            {
                height = default;
                return false;
            }

            height = GetHeightAt(worldX, worldZ);
            return true;
        }
    }
}
