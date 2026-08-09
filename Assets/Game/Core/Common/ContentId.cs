using System;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Stable, immutable technical identifier for datengetrieben content
    /// (e.g. "res.occidentia.t1.f1", "good.meridia.coffee", "mygreatmod.good.blueberry").
    /// Validates structure only -- no hardcoded whitelist of domain prefixes -- so
    /// core content and future mod namespaces remain equally representable.
    /// The string form is the stable savegame/reference representation; a savegame
    /// stores this ToString() value plus mutable state, never a copy of the definition.
    /// </summary>
    public readonly struct ContentId : IEquatable<ContentId>
    {
        private readonly string _value;

        private ContentId(string value)
        {
            _value = value;
        }

        public bool IsDefault => _value is null;

        public static ContentId Parse(string value)
        {
            if (!DottedIdentifier.IsValid(value, out var reason))
            {
                throw new ContentIdFormatException(value, reason);
            }

            return new ContentId(value);
        }

        public static bool TryParse(string value, out ContentId contentId)
        {
            if (DottedIdentifier.IsValid(value, out _))
            {
                contentId = new ContentId(value);
                return true;
            }

            contentId = default;
            return false;
        }

        public override string ToString()
        {
            return _value ?? string.Empty;
        }

        public bool Equals(ContentId other)
        {
            return string.Equals(_value, other._value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is ContentId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value is null ? 0 : _value.GetHashCode();
        }

        public static bool operator ==(ContentId left, ContentId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ContentId left, ContentId right)
        {
            return !left.Equals(right);
        }
    }
}
