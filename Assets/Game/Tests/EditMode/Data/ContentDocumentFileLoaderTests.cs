using System.IO;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Data.Loading;
using Varynth.Data.Sources;
using Varynth.Data.Validation;

namespace Varynth.Tests.EditMode.Data
{
    public class ContentDocumentFileLoaderTests
    {
        private string _tempDir;
        private static readonly ContentSource TestSource =
            new ContentSource(ContentSourceId.Parse("test"), ContentSourceType.Test, string.Empty);

        [SetUp]
        public void SetUp()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "VarynthContentDocumentFileLoaderTests_" + System.Guid.NewGuid().ToString("N"));
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

        [Test]
        public void LoadFromDirectory_RealXmlFiles_ReturnsContentDocuments()
        {
            File.WriteAllText(Path.Combine(_tempDir, "a.xml"), "<content><buildingDefinition id=\"bld.a\" nameKey=\"bld.a.name\" footprintWidth=\"1\" footprintLength=\"1\" prototypeVisualId=\"house\" /></content>");
            File.WriteAllText(Path.Combine(_tempDir, "b.xml"), "<content><buildingDefinition id=\"bld.b\" nameKey=\"bld.b.name\" footprintWidth=\"1\" footprintLength=\"1\" prototypeVisualId=\"house\" /></content>");

            var report = new ContentLoadReport();
            var documents = ContentDocumentFileLoader.LoadFromDirectory(_tempDir, TestSource, report);

            Assert.AreEqual(2, documents.Count);
            Assert.AreEqual(0, report.ErrorCount);
        }

        [Test]
        public void LoadFromDirectory_MissingDirectory_ReturnsEmpty_NoError()
        {
            var report = new ContentLoadReport();
            var documents = ContentDocumentFileLoader.LoadFromDirectory(Path.Combine(_tempDir, "does_not_exist"), TestSource, report);

            Assert.AreEqual(0, documents.Count);
            Assert.AreEqual(0, report.ErrorCount);
        }

        [Test]
        public void LoadFromDirectory_NonXmlFiles_AreIgnored()
        {
            File.WriteAllText(Path.Combine(_tempDir, "notes.txt"), "not xml");
            File.WriteAllText(Path.Combine(_tempDir, "a.xml"), "<content><buildingDefinition id=\"bld.a\" nameKey=\"bld.a.name\" footprintWidth=\"1\" footprintLength=\"1\" prototypeVisualId=\"house\" /></content>");

            var report = new ContentLoadReport();
            var documents = ContentDocumentFileLoader.LoadFromDirectory(_tempDir, TestSource, report);

            Assert.AreEqual(1, documents.Count);
        }
    }
}
