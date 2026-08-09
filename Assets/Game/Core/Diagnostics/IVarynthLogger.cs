namespace Varynth.Core.Diagnostics
{
    /// <summary>
    /// Minimal, engine-agnostic logging abstraction. Core code logs through this
    /// interface instead of calling UnityEngine.Debug.Log directly, so a Unity
    /// Console adapter, a test-collecting logger, a file logger, or mod diagnostics
    /// can all be plugged in later without touching Core code. No telemetry/analytics.
    /// </summary>
    public interface IVarynthLogger
    {
        void Info(string message, string context = null);

        void Warning(string message, string context = null);

        void Error(string message, string context = null);
    }
}
