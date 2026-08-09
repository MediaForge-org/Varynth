namespace Varynth.Core.Diagnostics
{
    /// <summary>
    /// No-op logger. Safe default so consumers never need to null-check a logger.
    /// </summary>
    public sealed class NullLogger : IVarynthLogger
    {
        public static readonly NullLogger Instance = new NullLogger();

        private NullLogger()
        {
        }

        public void Info(string message, string context = null)
        {
        }

        public void Warning(string message, string context = null)
        {
        }

        public void Error(string message, string context = null)
        {
        }
    }
}
