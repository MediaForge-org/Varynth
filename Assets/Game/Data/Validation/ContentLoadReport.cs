using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Diagnostics;

namespace Varynth.Data.Validation
{
    /// <summary>
    /// Collects structured content-load diagnostics and forwards each one to an
    /// injected IVarynthLogger -- a layer on top of Phase 1A diagnostics, not a
    /// competing logging/validation system.
    /// </summary>
    public sealed class ContentLoadReport
    {
        private readonly List<ContentLoadIssue> _issues = new List<ContentLoadIssue>();
        private readonly IVarynthLogger _logger;

        public ContentLoadReport(IVarynthLogger logger = null)
        {
            _logger = logger ?? NullLogger.Instance;
        }

        public IReadOnlyList<ContentLoadIssue> Issues => _issues;

        public int ErrorCount { get; private set; }
        public int WarningCount { get; private set; }

        public bool HasErrors => ErrorCount > 0;

        public void AddInfo(ContentSourceId? source, string filePath, ContentId? contentId, string message)
        {
            Add(LogSeverity.Info, source, filePath, contentId, message);
        }

        public void AddWarning(ContentSourceId? source, string filePath, ContentId? contentId, string message)
        {
            Add(LogSeverity.Warning, source, filePath, contentId, message);
        }

        public void AddError(ContentSourceId? source, string filePath, ContentId? contentId, string message)
        {
            Add(LogSeverity.Error, source, filePath, contentId, message);
        }

        private void Add(LogSeverity severity, ContentSourceId? source, string filePath, ContentId? contentId, string message)
        {
            _issues.Add(new ContentLoadIssue(severity, source, filePath, contentId, message));

            if (severity == LogSeverity.Error)
            {
                ErrorCount++;
            }
            else if (severity == LogSeverity.Warning)
            {
                WarningCount++;
            }

            var context = FormatContext(source, filePath, contentId);
            switch (severity)
            {
                case LogSeverity.Info:
                    _logger.Info(message, context);
                    break;
                case LogSeverity.Warning:
                    _logger.Warning(message, context);
                    break;
                case LogSeverity.Error:
                    _logger.Error(message, context);
                    break;
            }
        }

        private static string FormatContext(ContentSourceId? source, string filePath, ContentId? contentId)
        {
            var sourceText = source.HasValue ? source.Value.ToString() : "<unknown>";
            var fileText = filePath ?? "<unknown>";
            var idText = contentId.HasValue ? contentId.Value.ToString() : "<none>";
            return $"source={sourceText};file={fileText};id={idText}";
        }
    }
}
