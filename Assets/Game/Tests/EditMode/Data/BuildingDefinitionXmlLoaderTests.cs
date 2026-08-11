using System.Xml.Linq;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Data.Loading;
using Varynth.Data.Sources;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Tests.EditMode.Data
{
    public class BuildingDefinitionXmlLoaderTests
    {
        private static readonly ContentSource TestSource =
            new ContentSource(ContentSourceId.Parse("test"), ContentSourceType.Test, "/test");

        private static ContentDocument Doc(string filePath, string xml)
        {
            return new ContentDocument(TestSource, filePath, XDocument.Parse(xml));
        }

        [Test]
        public void ValidDefinition_LandsInRegistry_WithExpectedFields()
        {
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><buildingDefinition id=\"bld.prototype.house\" nameKey=\"bld.prototype.house.name\" " +
                    "footprintWidth=\"2\" footprintLength=\"2\" prototypeVisualId=\"house\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.IsTrue(registry.TryGet(ContentId.Parse("bld.prototype.house"), out var definition));
            Assert.AreEqual(2, definition.FootprintWidth);
            Assert.AreEqual(2, definition.FootprintLength);
            Assert.AreEqual("house", definition.PrototypeVisualId);
            Assert.IsFalse(definition.AllowsCoastPlacement);
        }

        [Test]
        public void AllowsCoastPlacement_Attribute_IsParsed()
        {
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><buildingDefinition id=\"bld.prototype.harbor\" nameKey=\"bld.prototype.harbor.name\" " +
                    "footprintWidth=\"3\" footprintLength=\"2\" prototypeVisualId=\"harbor\" allowsCoastPlacement=\"true\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            registry.TryGet(ContentId.Parse("bld.prototype.harbor"), out var definition);
            Assert.IsTrue(definition.AllowsCoastPlacement);
        }

        [Test]
        public void MissingFootprintWidth_IsRejected()
        {
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><buildingDefinition id=\"bld.prototype.house\" nameKey=\"bld.prototype.house.name\" " +
                    "footprintLength=\"2\" prototypeVisualId=\"house\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void MissingPrototypeVisualId_IsRejected()
        {
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><buildingDefinition id=\"bld.prototype.house\" nameKey=\"bld.prototype.house.name\" " +
                    "footprintWidth=\"2\" footprintLength=\"2\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
        }

        [Test]
        public void UnknownAttribute_ReportedAsInfo_DefinitionStillRegisters()
        {
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><buildingDefinition id=\"bld.prototype.house\" nameKey=\"bld.prototype.house.name\" " +
                    "footprintWidth=\"2\" footprintLength=\"2\" prototypeVisualId=\"house\" extra=\"x\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.AreEqual(0, report.ErrorCount);
        }
    }
}
