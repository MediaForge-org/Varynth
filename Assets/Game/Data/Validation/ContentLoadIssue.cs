using Varynth.Core.Common;
using Varynth.Core.Diagnostics;

namespace Varynth.Data.Validation
{
    /// <summary>
    /// One structured content-load diagnostic entry. Reuses Phase 1A's LogSeverity
    /// instead of a second, parallel severity enum, and adds the Source/File/ContentId
    /// context that IVarynthLogger's single free-text "context" string can't carry.
    /// </summary>
    public readonly struct ContentLoadIssue
    {
        public LogSeverity Severity { get; }
        public ContentSourceId? Source { get; }
        public string FilePath { get; }
        public ContentId? ContentId { get; }
        public string Message { get; }

        public ContentLoadIssue(LogSeverity severity, ContentSourceId? source, string filePath, ContentId? contentId, string message)
        {
            Severity = severity;
            Source = source;
            FilePath = filePath;
            ContentId = contentId;
            Message = message;
        }
    }
}
