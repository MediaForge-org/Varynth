using NUnit.Framework;
using Varynth.Core.Diagnostics;

namespace Varynth.Tests.EditMode
{
    public class DiagnosticsTests
    {
        [Test]
        public void CollectingLogger_RecordsInfoWarningError()
        {
            var logger = new CollectingLogger();

            logger.Info("info message", "ctx-info");
            logger.Warning("warning message", "ctx-warning");
            logger.Error("error message", "ctx-error");

            Assert.AreEqual(3, logger.Entries.Count);

            Assert.AreEqual(LogSeverity.Info, logger.Entries[0].Severity);
            Assert.AreEqual("info message", logger.Entries[0].Message);
            Assert.AreEqual("ctx-info", logger.Entries[0].Context);

            Assert.AreEqual(LogSeverity.Warning, logger.Entries[1].Severity);
            Assert.AreEqual(LogSeverity.Error, logger.Entries[2].Severity);
        }

        [Test]
        public void NullLogger_DoesNotThrow()
        {
            IVarynthLogger logger = NullLogger.Instance;

            Assert.DoesNotThrow(() =>
            {
                logger.Info("info");
                logger.Warning("warning");
                logger.Error("error");
            });
        }

        [Test]
        public void LoggerAbstraction_IsSwappable()
        {
            var collecting = new CollectingLogger();

            UseLogger(collecting);

            Assert.AreEqual(1, collecting.Entries.Count);
        }

        private static void UseLogger(IVarynthLogger logger)
        {
            logger.Info("swap test");
        }
    }
}
