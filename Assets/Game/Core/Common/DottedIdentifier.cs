using System.Text.RegularExpressions;

namespace Varynth.Core.Common
{
    /// <summary>
    /// Shared structural validation for dot-segmented, lowercase content identifiers.
    /// Validates shape only, never a fixed set of allowed domain prefixes -- Varynth is
    /// moddable and future content categories (core or mod-namespaced) must stay possible.
    /// </summary>
    internal static class DottedIdentifier
    {
        private static readonly Regex SegmentPattern = new Regex(
            "^[a-z0-9]+(?:[_-][a-z0-9]+)*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static bool IsValid(string value, out string reason)
        {
            return IsValid(value, minSegments: 2, out reason);
        }

        public static bool IsValid(string value, int minSegments, out string reason)
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

            if (ContainsWhitespace(value))
            {
                reason = "value contains embedded whitespace";
                return false;
            }

            var segments = value.Split('.');
            if (segments.Length < minSegments)
            {
                reason = minSegments <= 1
                    ? "value must contain at least one segment"
                    : $"value must contain at least {minSegments} dot-separated segments";
                return false;
            }

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

        private static bool ContainsWhitespace(string value)
        {
            foreach (var c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
