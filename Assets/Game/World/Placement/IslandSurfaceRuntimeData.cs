using UnityEngine;

namespace Varynth.World.Placement
{
    /// <summary>
    /// Serialized, GUID-stable runtime data source for one island's surface
    /// classification -- built once by WorldPrototypeSceneBuilder at scene-build time,
    /// consumed at Play/Player-build time. Fixes a real gap: WorldPrototypeSceneBuilder
    /// and its IslandBuildResult/IslandSurfaceMap live in the Editor-only
    /// Varynth.Tooling.Editor assembly, which does not exist in a Player build --
    /// runtime placement code needs its own serialized data, not an Editor-only type.
    /// Flags is a direct copy of the SurfaceCellFlags[] array already computed by
    /// SurfaceMapGenerator at generation time -- no re-classification at runtime.
    /// </summary>
    public sealed class IslandSurfaceRuntimeData : ScriptableObject
    {
        [SerializeField] private int _originCellX;
        [SerializeField] private int _originCellZ;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private byte[] _flags;

        public int OriginCellX => _originCellX;
        public int OriginCellZ => _originCellZ;
        public int Width => _width;
        public int Height => _height;
        public byte[] Flags => _flags;

        public void SetData(int originCellX, int originCellZ, int width, int height, byte[] flags)
        {
            _originCellX = originCellX;
            _originCellZ = originCellZ;
            _width = width;
            _height = height;
            _flags = flags;
        }
    }
}
