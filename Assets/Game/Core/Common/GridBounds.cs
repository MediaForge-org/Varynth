namespace Varynth.Core.Common
{
    /// <summary>
    /// Engine-free replacement for UnityEngine.RectInt in authoritative simulation
    /// state (Phase 2E) -- a plain integer cell rectangle, half-open on the max side
    /// (matches RectInt.Contains semantics: [MinX, MaxX) x [MinZ, MaxZ)).
    /// </summary>
    public readonly struct GridBounds
    {
        public int OriginX { get; }
        public int OriginZ { get; }
        public int Width { get; }
        public int Height { get; }

        public int MinX => OriginX;
        public int MinZ => OriginZ;
        public int MaxX => OriginX + Width;
        public int MaxZ => OriginZ + Height;

        public GridBounds(int originX, int originZ, int width, int height)
        {
            OriginX = originX;
            OriginZ = originZ;
            Width = width;
            Height = height;
        }

        public bool Contains(int x, int z) => x >= MinX && x < MaxX && z >= MinZ && z < MaxZ;

        public bool Contains(GridCoordinate cell) => Contains(cell.X, cell.Z);
    }
}
