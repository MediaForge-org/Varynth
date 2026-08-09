using System;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Data.Mods;
using Varynth.Data.Validation;

namespace Varynth.Tests.EditMode.Data
{
    public class ModManifestXmlReaderTests
    {
        private string _tempDir;

        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "varynth-manifest-tests-" + Guid.NewGuid().ToString("N"));
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
        public void TryRead_ValidManifest_ParsesAllFields()
        {
            var xml = @"<mod id=""author.modname"" version=""1.0.0"" nameKey=""mod.author.modname.name"">
                <dependencies>
                    <dependency id=""someother.mod"" />
                    <dependency id=""anothermod"" optional=""true"" />
                </dependencies>
                <loadAfter>
                    <source id=""core"" />
                </loadAfter>
            </mod>";
            var document = XDocument.Parse(xml);
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryRead(document, "manifest.xml", report, out var manifest);

            Assert.IsTrue(success, string.Join("; ", MessagesOf(report)));
            Assert.AreEqual(ContentSourceId.Parse("author.modname"), manifest.Id);
            Assert.AreEqual("1.0.0", manifest.Version);
            Assert.AreEqual(LocalizationKey.Parse("mod.author.modname.name"), manifest.NameKey);
            Assert.AreEqual(2, manifest.Dependencies.Count);
            Assert.AreEqual(1, manifest.LoadAfter.Count);
            Assert.AreEqual(ContentSourceId.Parse("core"), manifest.LoadAfter[0]);
        }

        [Test]
        public void TryRead_InvalidModId_FailsAndReportsError()
        {
            var document = XDocument.Parse(@"<mod id=""Invalid Id"" version=""1.0.0"" nameKey=""mod.x.name"" />");
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryRead(document, "manifest.xml", report, out var manifest);

            Assert.IsFalse(success);
            Assert.IsNull(manifest);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryRead_MissingVersion_FailsAndReportsError()
        {
            var document = XDocument.Parse(@"<mod id=""author.modname"" nameKey=""mod.x.name"" />");
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryRead(document, "manifest.xml", report, out var manifest);

            Assert.IsFalse(success);
            Assert.IsNull(manifest);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryRead_RequiredAndOptionalDependencies_AreDistinguished()
        {
            var xml = @"<mod id=""author.modname"" version=""1.0.0"" nameKey=""mod.x.name"">
                <dependencies>
                    <dependency id=""hard.dep"" />
                    <dependency id=""soft.dep"" optional=""true"" />
                </dependencies>
            </mod>";
            var document = XDocument.Parse(xml);
            var report = new ContentLoadReport();

            ModManifestXmlReader.TryRead(document, "manifest.xml", report, out var manifest);

            Assert.AreEqual(2, manifest.Dependencies.Count);
            var hard = manifest.Dependencies[0];
            var soft = manifest.Dependencies[1];
            Assert.AreEqual(ContentSourceId.Parse("hard.dep"), hard.Id);
            Assert.IsFalse(hard.Optional);
            Assert.AreEqual(ContentSourceId.Parse("soft.dep"), soft.Id);
            Assert.IsTrue(soft.Optional);
        }

        [Test]
        public void TryRead_InvalidOptionalAttribute_DefaultsFalseAndReportsWarning()
        {
            var document = XDocument.Parse(@"<mod id=""author.modname"" version=""1.0.0"" nameKey=""mod.x.name"">
                <dependencies>
                    <dependency id=""some.dep"" optional=""maybe"" />
                </dependencies>
            </mod>");
            var report = new ContentLoadReport();

            ModManifestXmlReader.TryRead(document, "manifest.xml", report, out var manifest);

            Assert.IsFalse(manifest.Dependencies[0].Optional);
            Assert.AreEqual(1, report.WarningCount);
        }

        [Test]
        public void TryReadFromFile_ValidManifest_LoadsThroughHardenedReader()
        {
            var path = WriteFile("mod.xml", @"<mod id=""author.modname"" version=""1.0.0"" nameKey=""mod.x.name"" />");
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryReadFromFile(path, report, out var manifest);

            Assert.IsTrue(success);
            Assert.AreEqual(ContentSourceId.Parse("author.modname"), manifest.Id);
        }

        [Test]
        public void TryReadFromFile_DoctypeManifest_IsRejected()
        {
            var path = WriteFile("mod-doctype.xml",
                "<!DOCTYPE mod [<!ELEMENT mod ANY>]><mod id=\"author.modname\" version=\"1.0.0\" nameKey=\"mod.x.name\" />");
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryReadFromFile(path, report, out var manifest);

            Assert.IsFalse(success);
            Assert.IsNull(manifest);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void TryReadFromFile_XxeManifest_IsRejectedWithoutLeakingFileContents()
        {
            var secretPath = WriteFile("secret.txt", "super-secret-value");
            var xxePayload =
                "<!DOCTYPE mod [<!ENTITY xxe SYSTEM \"file:///" + secretPath.Replace("\\", "/") + "\">]>" +
                "<mod id=\"author.modname\" version=\"&xxe;\" nameKey=\"mod.x.name\" />";
            var path = WriteFile("mod-xxe.xml", xxePayload);
            var report = new ContentLoadReport();

            var success = ModManifestXmlReader.TryReadFromFile(path, report, out var manifest);

            Assert.IsFalse(success);
            Assert.IsNull(manifest);
            foreach (var issue in report.Issues)
            {
                StringAssert.DoesNotContain("super-secret-value", issue.Message);
            }
        }

        private static string[] MessagesOf(ContentLoadReport report)
        {
            var messages = new string[report.Issues.Count];
            for (var i = 0; i < report.Issues.Count; i++)
            {
                messages[i] = report.Issues[i].Message;
            }

            return messages;
        }
    }
}
