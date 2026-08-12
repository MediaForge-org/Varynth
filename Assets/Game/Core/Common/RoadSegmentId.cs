using System;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Stable identity for one road segment. Backed by ulong, same idiom as
    /// BuildingInstanceId/GameTick -- assigned deterministically by whichever world
    /// state owns the counter (RoadNetworkState), never self-generating.
    ///
    /// Lives in Varynth.Core.Common (not Varynth.World.Roads) deliberately:
    /// RemoveRoadCommand (Varynth.Core.Simulation) needs to carry a RoadSegmentId, and
    /// Varynth.Core.Simulation must never reference Varynth.World (only the reverse
    /// edge exists) -- so this type has to live at or below Core.Simulation's own
    /// dependency level, exactly where BuildingInstanceId already lives.
    /// </summary>
    public readonly struct RoadSegmentId : IEquatable<RoadSegmentId>, IComparable<RoadSegmentId>
    {
        public static readonly RoadSegmentId None = new RoadSegmentId(0);

        private readonly ulong _value;

        private RoadSegmentId(ulong value)
        {
            _value = value;
        }

        public static RoadSegmentId FromRaw(ulong value)
        {
            return new RoadSegmentId(value);
        }

        public ulong Value => _value;

        public bool IsNone => _value == 0;

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(RoadSegmentId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is RoadSegmentId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public int CompareTo(RoadSegmentId other)
        {
            return _value.CompareTo(other._value);
        }

        public static bool operator ==(RoadSegmentId left, RoadSegmentId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RoadSegmentId left, RoadSegmentId right)
        {
            return !left.Equals(right);
        }
    }
}
