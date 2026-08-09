using System;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Thrown when a raw string fails the dotted-identifier structural validation
    /// used by both <see cref="ContentId"/> and LocalizationKey. Never thrown silently --
    /// invalid identifiers are always a hard failure, never a silent correction.
    /// </summary>
    public sealed class ContentIdFormatException : FormatException
    {
        public string RawValue { get; }
        public string Reason { get; }

        public ContentIdFormatException(string rawValue, string reason)
            : base(BuildMessage(rawValue, reason))
        {
            RawValue = rawValue;
            Reason = reason;
        }

        private static string BuildMessage(string rawValue, string reason)
        {
            var display = rawValue ?? "<null>";
            return $"Invalid content identifier '{display}': {reason}";
        }
    }
}
