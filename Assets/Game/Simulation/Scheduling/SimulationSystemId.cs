using System;
using System.Text.RegularExpressions;

namespace Varynth.Core.Simulation.Scheduling
{
    /// <summary>
    /// Stable technical identifier for a simulation system (e.g. "population",
    /// "simulation.trade"). Its own type and its own validation -- deliberately not
    /// ContentId and not calling into Varynth.Core.Definitions' internal
    /// DottedIdentifier helper, even though the character rules happen to look similar.
    /// A system id is not a moddable content identifier; this is a different domain.
    /// No automatic normalization/correction of an invalid id.
    /// </summary>
    public readonly struct SimulationSystemId : IEquatable<SimulationSystemId>
    {
        private static readonly Regex SegmentPattern = new Regex(
            "^[a-z0-9]+(?:[_-][a-z0-9]+)*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly string _value;

        private SimulationSystemId(string value)
        {
            _value = value;
        }

        public bool IsDefault => _value is null;

        public static SimulationSystemId Parse(string value)
        {
            if (!IsValid(value, out var reason))
            {
                throw new ArgumentException($"Invalid simulation system id '{value ?? "<null>"}': {reason}", nameof(value));
            }

            return new SimulationSystemId(value);
        }

        public static bool TryParse(string value, out SimulationSystemId id)
        {
            if (IsValid(value, out _))
            {
                id = new SimulationSystemId(value);
                return true;
            }

            id = default;
            return false;
        }

        private static bool IsValid(string value, out string reason)
        {
            if (value is null)
            {
                reason = "value is null";
                return false;
            }

            if (value.Length == 0)
            {
                reason = "value is empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                reason = "value is whitespace-only";
                return false;
            }

            foreach (var c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    reason = "value contains embedded whitespace";
                    return false;
                }
            }

            var segments = value.Split('.');
            foreach (var segment in segments)
            {
                if (segment.Length == 0)
                {
                    reason = "value contains an empty segment (leading, trailing, or consecutive dot)";
                    return false;
                }

                if (!SegmentPattern.IsMatch(segment))
                {
                    reason = $"segment '{segment}' contains characters outside [a-z0-9_-] or has invalid leading/trailing separators";
                    return false;
                }
            }

            reason = null;
            return true;
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }

        public bool Equals(SimulationSystemId other)
        {
            return string.Equals(_value, other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is SimulationSystemId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value is null ? 0 : _value.GetHashCode();
        }

        public static bool operator ==(SimulationSystemId left, SimulationSystemId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SimulationSystemId left, SimulationSystemId right)
        {
            return !left.Equals(right);
        }
    }
}
