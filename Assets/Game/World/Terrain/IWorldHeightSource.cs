namespace Varynth.World.Terrain
{
    /// <summary>
    /// Abstraction over "how tall is the world surface at this X/Z" so downstream
    /// code (grid rendering, highlight, pointer) never depends on UnityEngine.Terrain
    /// directly. Keeps the terrain technology swappable later.
    /// </summary>
    public interface IWorldHeightSource
    {
        float GetHeightAt(float worldX, float worldZ);

        bool TryGetHeight(float worldX, float worldZ, out float height);
    }
}
