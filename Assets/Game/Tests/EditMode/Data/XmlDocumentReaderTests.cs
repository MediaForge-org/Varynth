using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Tests.EditMode.Data
{
    public class XmlDocumentReaderTests
    {
        private string _tempDir;

        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "varynth-xmlreader-tests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, recursive: true);
            }
        }

        private string WriteFile(string fileName, string contents)
        {
            var path = Path.Combine(_tempDir, fileName);
            File.WriteAllText(path, contents);
            return path;
        }

        [Test]
        public void TryLoad_ValidMinimalXml_Succeeds()
        {
            var path = WriteFile("valid.xml", "<content></content>");
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsTrue(success);
            Assert.IsNotNull(document);
            Assert.AreEqual(0, report.ErrorCount);
        }

        [Test]
        public void TryLoad_MalformedXml_FailsAndReportsError()
        {
            var path = WriteFile("malformed.xml", "<content><unclosed></content>");
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsFalse(success);
            Assert.IsNull(document);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryLoad_WrongRootElement_FailsAndReportsError()
        {
            var path = WriteFile("wrong-root.xml", "<somethingElse></somethingElse>");
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsFalse(success);
            Assert.IsNull(document);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryLoad_DoctypeDeclaration_IsRejected()
        {
            var path = WriteFile("doctype.xml", "<!DOCTYPE content [<!ELEMENT content ANY>]><content></content>");
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsFalse(success);
            Assert.IsNull(document);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryLoad_ExternalEntity_IsRejectedWithoutLeakingFileContents()
        {
            var secretPath = WriteFile("secret.txt", "super-secret-value");
            var xxePayload =
                "<!DOCTYPE content [<!ENTITY xxe SYSTEM \"file:///" + secretPath.Replace("\\", "/") + "\">]>" +
                "<content>&xxe;</content>";
            var path = WriteFile("xxe.xml", xxePayload);
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsFalse(success);
            Assert.IsNull(document);
            foreach (var issue in report.Issues)
            {
                StringAssert.DoesNotContain("super-secret-value", issue.Message);
            }
        }

        [Test]
        public void TryLoad_DoctypeFreeDocument_StillLoads()
        {
            var path = WriteFile("regression.xml", "<content><item id=\"a.b\" /></content>");
            var report = new ContentLoadReport();

            var success = XmlDocumentReader.TryLoad(path, null, "content", report, out var document);

            Assert.IsTrue(success);
            Assert.IsNotNull(document.Root);
            Assert.AreEqual(1, document.Root.Elements().Count());
        }
    }
}
