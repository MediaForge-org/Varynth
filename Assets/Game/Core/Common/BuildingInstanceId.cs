using System;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Stable identity for one placed building instance. Backed by ulong, same idiom
    /// as GameTick -- deliberately NOT a Guid. Unlike PlayerId (created once at
    /// profile-init time, outside any deterministic tick), a BuildingInstanceId is
    /// assigned at the moment a placement command is applied: a genuinely
    /// deterministic, reproducible event that must produce the same id given the same
    /// command sequence (host and a future client alike). A Guid.NewGuid() per
    /// placement would be non-deterministic across machines; a sequential,
    /// world-state-owned counter is deterministic by construction and trivially
    /// save/load-friendly (persist the next-id counter). This type never
    /// self-generates -- the counter lives in whichever world state assigns ids.
    /// </summary>
    public readonly struct BuildingInstanceId : IEquatable<BuildingInstanceId>, IComparable<BuildingInstanceId>
    {
        /// <summary>Sentinel for "no instance" -- matches the occupancy-map unoccupied value.</summary>
        public static readonly BuildingInstanceId None = new BuildingInstanceId(0);

        private readonly ulong _value;

        private BuildingInstanceId(ulong value)
        {
            _value = value;
        }

        public static BuildingInstanceId FromRaw(ulong value)
        {
            return new BuildingInstanceId(value);
        }

        public ulong Value => _value;

        public bool IsNone => _value == 0;

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(BuildingInstanceId other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is BuildingInstanceId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public int CompareTo(BuildingInstanceId other)
        {
            return _value.CompareTo(other._value);
        }

        public static bool operator ==(BuildingInstanceId left, BuildingInstanceId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BuildingInstanceId left, BuildingInstanceId right)
        {
            return !left.Equals(right);
        }
    }
}
