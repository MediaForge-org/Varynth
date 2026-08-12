using Varynth.Core.Common;

namespace Varynth.World.Roads
{
    /// <summary>
    /// A road node's identity IS its GridCoordinate -- no separate node-id wrapper
    /// (unlike buildings, a road node is 1:1 with "a road exists at this cell", so
    /// the cell coordinate already is the stable key). ConnectedDirectionsMask is one
    /// bit per RoadDirection (N=bit0 .. NW=bit7), giving O(1) "which of the 8
    /// neighbors is connected" without walking the segment dictionary -- used by
    /// junction-mesh classification and the building/road connection query.
    /// </summary>
    public sealed class RoadNode
    {
        public GridCoordinate Cell { get; }
        public byte ConnectedDirectionsMask { get; internal set; }

        public RoadNode(GridCoordinate cell)
        {
            Cell = cell;
            ConnectedDirectionsMask = 0;
        }

        public bool IsConnected(RoadDirection direction)
        {
            return (ConnectedDirectionsMask & (1 << (int)direction)) != 0;
        }

        public int ConnectedDirectionCount()
        {
            var count = 0;
            var mask = ConnectedDirectionsMask;
            while (mask != 0)
            {
                count += mask & 1;
                mask >>= 1;
            }

            return count;
        }
    }
}
