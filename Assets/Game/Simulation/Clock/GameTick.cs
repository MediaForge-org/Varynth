using System;

namespace Varynth.Core.Simulation.Clock
{
    /// <summary>
    /// Stable, integer-based simulation tick counter. Deliberately not a raw ulong
    /// everywhere -- same value-type-with-identity idiom as ContentId/ContentSourceId.
    /// Negative ticks are impossible by construction (backed by ulong). Arithmetic is
    /// checked -- an overflow throws OverflowException rather than silently wrapping.
    /// </summary>
    public readonly struct GameTick : IEquatable<GameTick>, IComparable<GameTick>
    {
        public static readonly GameTick Zero = new GameTick(0);

        private readonly ulong _value;

        private GameTick(ulong value)
        {
            _value = value;
        }

        public static GameTick FromRaw(ulong value)
        {
            return new GameTick(value);
        }

        public ulong Value => _value;

        public GameTick Add(ulong delta)
        {
            checked
            {
                return new GameTick(_value + delta);
            }
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public bool Equals(GameTick other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is GameTick other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public int CompareTo(GameTick other)
        {
            return _value.CompareTo(other._value);
        }

        public static bool operator ==(GameTick left, GameTick right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameTick left, GameTick right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(GameTick left, GameTick right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(GameTick left, GameTick right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(GameTick left, GameTick right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(GameTick left, GameTick right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
