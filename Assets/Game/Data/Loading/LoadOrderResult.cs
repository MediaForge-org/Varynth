using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Data.Sources;

namespace Varynth.Data.Loading
{
    public enum LoadOrderExclusionReason
    {
        DuplicateSourceId,
        MissingDependency,
        DependencyExcluded,
        CycleMember,
        BlockedByCycle
    }

    /// <summary>
    /// One excluded content source plus why. Carries the full ContentSource (not just
    /// the id) so a later bootstrap package can implement an abort-vs-skip policy
    /// (e.g. "abort if any excluded source has Type == Core") without needing to
    /// change the resolver.
    /// </summary>
    public readonly struct LoadOrderExclusion
    {
        public ContentSource Source { get; }
        public LoadOrderExclusionReason Reason { get; }
        public string Detail { get; }

        public LoadOrderExclusion(ContentSource source, LoadOrderExclusionReason reason, string detail)
        {
            Source = source;
            Reason = reason;
            Detail = detail;
        }
    }

    public sealed class LoadOrderResult
    {
        public IReadOnlyList<ContentSourceId> OrderedSourceIds { get; }
        public IReadOnlyList<LoadOrderExclusion> Exclusions { get; }

        public LoadOrderResult(IReadOnlyList<ContentSourceId> orderedSourceIds, IReadOnlyList<LoadOrderExclusion> exclusions)
        {
            OrderedSourceIds = orderedSourceIds;
            Exclusions = exclusions;
        }
    }
}
