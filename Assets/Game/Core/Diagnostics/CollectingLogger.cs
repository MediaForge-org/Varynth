using System.Collections.Generic;

namespace Varynth.Core.Diagnostics
{
    public enum LogSeverity
    {
        Info,
        Warning,
        Error
    }

    public readonly struct LogEntry
    {
        public LogSeverity Severity { get; }
        public string Message { get; }
        public string Context { get; }

        public LogEntry(LogSeverity severity, string message, string context)
        {
            Severity = severity;
            Message = message;
            Context = context;
        }
    }

    /// <summary>
    /// In-memory logger used by tests (and available to future editor/debug tooling)
    /// to verify that Core code actually goes through IVarynthLogger rather than
    /// calling UnityEngine.Debug.Log directly.
    /// </summary>
    public sealed class CollectingLogger : IVarynthLogger
    {
        private readonly List<LogEntry> _entries = new List<LogEntry>();

        public IReadOnlyList<LogEntry> Entries => _entries;

        public void Info(string message, string context = null)
        {
            _entries.Add(new LogEntry(LogSeverity.Info, message, context));
        }

        public void Warning(string message, string context = null)
        {
            _entries.Add(new LogEntry(LogSeverity.Warning, message, context));
        }

        public void Error(string message, string context = null)
        {
            _entries.Add(new LogEntry(LogSeverity.Error, message, context));
        }
    }
}
