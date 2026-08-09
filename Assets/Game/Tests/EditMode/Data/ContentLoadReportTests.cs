using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Diagnostics;
using Varynth.Data.Validation;

namespace Varynth.Tests.EditMode.Data
{
    public class ContentLoadReportTests
    {
        [Test]
        public void AddWarning_IsCollectedWithSeverity()
        {
            var report = new ContentLoadReport();

            report.AddWarning(ContentSourceId.Parse("core"), "file.xml", ContentId.Parse("good.meridia.coffee"), "something odd");

            Assert.AreEqual(1, report.Issues.Count);
            Assert.AreEqual(1, report.WarningCount);
            Assert.AreEqual(0, report.ErrorCount);
            Assert.AreEqual(LogSeverity.Warning, report.Issues[0].Severity);
        }

        [Test]
        public void AddError_IsCollectedWithSeverity()
        {
            var report = new ContentLoadReport();

            report.AddError(ContentSourceId.Parse("core"), "file.xml", null, "something broke");

            Assert.AreEqual(1, report.Issues.Count);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.HasErrors);
        }

        [Test]
        public void Context_SourceFileAndContentId_ArePreserved()
        {
            var report = new ContentLoadReport();
            var source = ContentSourceId.Parse("author.modname");
            var contentId = ContentId.Parse("good.meridia.coffee");

            report.AddError(source, "file.xml", contentId, "bad thing");

            var issue = report.Issues[0];
            Assert.AreEqual(source, issue.Source);
            Assert.AreEqual("file.xml", issue.FilePath);
            Assert.AreEqual(contentId, issue.ContentId);
            Assert.AreEqual("bad thing", issue.Message);
        }

        [Test]
        public void Context_UnknownSourceOrContentId_StaysNull()
        {
            var report = new ContentLoadReport();

            report.AddInfo(null, null, null, "fyi");

            var issue = report.Issues[0];
            Assert.IsFalse(issue.Source.HasValue);
            Assert.IsFalse(issue.ContentId.HasValue);
        }
    }
}
