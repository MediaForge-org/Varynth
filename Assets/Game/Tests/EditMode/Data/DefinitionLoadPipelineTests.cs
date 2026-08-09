using System.Xml.Linq;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Data.Loading;
using Varynth.Data.Sources;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Tests.EditMode.Data
{
    public class DefinitionLoadPipelineTests
    {
        private static readonly ContentSource TestSource =
            new ContentSource(ContentSourceId.Parse("test"), ContentSourceType.Test, "/test");

        private static ContentDocument Doc(ContentSource source, string filePath, string xml)
        {
            return new ContentDocument(source, filePath, XDocument.Parse(xml));
        }

        [Test]
        public void ValidDefinition_LandsInRegistry()
        {
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(TestSource, "a.xml", "<content><testDefinition id=\"good.meridia.coffee\" nameKey=\"good.coffee.name\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.IsTrue(registry.TryGet(ContentId.Parse("good.meridia.coffee"), out _));
        }

        [Test]
        public void DuplicateIdAcrossDocuments_FirstWins_SecondRejected()
        {
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(TestSource, "a.xml", "<content><testDefinition id=\"good.meridia.coffee\" nameKey=\"good.coffee.name\" /></content>"),
                Doc(TestSource, "b.xml", "<content><testDefinition id=\"good.meridia.coffee\" nameKey=\"good.coffee.duplicate\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
            registry.TryGet(ContentId.Parse("good.meridia.coffee"), out var kept);
            Assert.AreEqual("good.coffee.name", kept.NameKey.ToString());
        }

        [Test]
        public void StructurallyInvalidElement_NeverReachesRegistry()
        {
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(TestSource, "a.xml", "<content><testDefinition id=\"BAD ID\" nameKey=\"good.coffee.name\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void UnknownAttribute_IsReportedAsInfo_ButDefinitionStillRegisters()
        {
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(TestSource, "a.xml",
                    "<content><testDefinition id=\"good.meridia.coffee\" nameKey=\"good.coffee.name\" extra=\"x\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.GreaterOrEqual(CountInfo(report), 1);
        }

        [Test]
        public void UnknownElementTag_IsReported()
        {
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(TestSource, "a.xml", "<content><somethingElse id=\"x.y\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.GreaterOrEqual(CountInfo(report), 1);
        }

        [Test]
        public void ModSource_RegisteringOwnNamespace_Succeeds()
        {
            var modSource = new ContentSource(ContentSourceId.Parse("author.modname"), ContentSourceType.Mod, "/mods/author.modname");
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(modSource, "mod.xml",
                    "<content><testDefinition id=\"author.modname.good.blueberry\" nameKey=\"good.blueberry.name\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.AreEqual(0, report.ErrorCount);
        }

        [Test]
        public void ModSource_RegisteringForeignNamespace_IsRejected()
        {
            var modSource = new ContentSource(ContentSourceId.Parse("author.modname"), ContentSourceType.Mod, "/mods/author.modname");
            var pipeline = new DefinitionLoadPipeline<TestDefinition>(new TestDefinitionXmlLoader());
            var documents = new[]
            {
                Doc(modSource, "mod.xml",
                    "<content><testDefinition id=\"good.meridia.something\" nameKey=\"good.something.name\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
        }

        private static int CountInfo(ContentLoadReport report)
        {
            var count = 0;
            foreach (var issue in report.Issues)
            {
                if (issue.Severity == Varynth.Core.Diagnostics.LogSeverity.Info)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
