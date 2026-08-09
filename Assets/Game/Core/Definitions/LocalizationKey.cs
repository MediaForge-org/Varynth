using System;
using Varynth.Core.Common;

namespace Varynth.Core.Definitions
{
    /// <summary>
    /// Stable reference to a localized string (e.g. "quest.story.occidentia.001.title").
    /// Definitions store only this key, never literal display text -- resolution to
    /// actual text happens exclusively in the presentation layer.
    /// Reuses the same dotted-identifier structural rule as <see cref="ContentId"/>.
    /// </summary>
    public readonly struct LocalizationKey : IEquatable<LocalizationKey>
    {
        private readonly string _value;

        private LocalizationKey(string value)
        {
            _value = value;
        }

        public bool IsDefault => _value is null;

        public static LocalizationKey Parse(string value)
        {
            if (!DottedIdentifier.IsValid(value, out var reason))
            {
                throw new ContentIdFormatException(value, reason);
            }

            return new LocalizationKey(value);
        }

        public static bool TryParse(string value, out LocalizationKey key)
        {
            if (DottedIdentifier.IsValid(value, out _))
            {
                key = new LocalizationKey(value);
                return true;
            }

            key = default;
            return false;
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }

        public bool Equals(LocalizationKey other)
        {
            return string.Equals(_value, other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is LocalizationKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value is null ? 0 : _value.GetHashCode();
        }

        public static bool operator ==(LocalizationKey left, LocalizationKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LocalizationKey left, LocalizationKey right)
        {
            return !left.Equals(right);
        }
    }
}
