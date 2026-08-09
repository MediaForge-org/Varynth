using System;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Stable identifier for a content source (Core, an official addon, a mod, a test
    /// fixture, ...). Unlike <see cref="ContentId"/>, a single segment is legitimate here
    /// (e.g. "core", "test") -- content-source identity and content-item identity follow
    /// related but distinct rules, so this is its own small value type rather than a
    /// relaxed ContentId. Reuses the same shared dotted-identifier structural validation
    /// and the same <see cref="ContentIdFormatException"/> as ContentId/LocalizationKey.
    /// </summary>
    public readonly struct ContentSourceId : IEquatable<ContentSourceId>
    {
        private readonly string _value;

        private ContentSourceId(string value)
        {
            _value = value;
        }

        public bool IsDefault => _value is null;

        public static ContentSourceId Parse(string value)
        {
            if (!DottedIdentifier.IsValid(value, minSegments: 1, out var reason))
            {
                throw new ContentIdFormatException(value, reason);
            }

            return new ContentSourceId(value);
        }

        public static bool TryParse(string value, out ContentSourceId sourceId)
        {
            if (DottedIdentifier.IsValid(value, minSegments: 1, out _))
            {
                sourceId = new ContentSourceId(value);
                return true;
            }

            sourceId = default;
            return false;
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }

        public bool Equals(ContentSourceId other)
        {
            return string.Equals(_value, other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is ContentSourceId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value is null ? 0 : _value.GetHashCode();
        }

        public static bool operator ==(ContentSourceId left, ContentSourceId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ContentSourceId left, ContentSourceId right)
        {
            return !left.Equals(right);
        }
    }
}
