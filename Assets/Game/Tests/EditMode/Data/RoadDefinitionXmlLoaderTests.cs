using System.Xml.Linq;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Data.Loading;
using Varynth.Data.Sources;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Tests.EditMode.Data
{
    public class RoadDefinitionXmlLoaderTests
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
            var pipeline = new DefinitionLoadPipeline<RoadDefinition>(new RoadDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><roadDefinition id=\"road.prototype.basic\" nameKey=\"road.prototype.basic.name\" " +
                    "prototypeVisualId=\"road\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(1, registry.Count);
            Assert.IsTrue(registry.TryGet(ContentId.Parse("road.prototype.basic"), out var definition));
            Assert.AreEqual("road", definition.PrototypeVisualId);
            Assert.AreEqual(1, definition.LogicalWidthCells);
            Assert.IsTrue(definition.AllowsDiagonalSegments);
            Assert.IsFalse(definition.AllowsCoastPlacement);
        }

        [Test]
        public void AllAttributes_AreParsed()
        {
            var pipeline = new DefinitionLoadPipeline<RoadDefinition>(new RoadDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml",
                    "<content><roadDefinition id=\"road.prototype.basic\" nameKey=\"road.prototype.basic.name\" " +
                    "prototypeVisualId=\"road\" logicalWidthCells=\"2\" allowsDiagonalSegments=\"false\" allowsCoastPlacement=\"true\" /></content>")
            };
            var registry = pipeline.Load(documents, new ContentLoadReport());

            registry.TryGet(ContentId.Parse("road.prototype.basic"), out var definition);
            Assert.AreEqual(2, definition.LogicalWidthCells);
            Assert.IsFalse(definition.AllowsDiagonalSegments);
            Assert.IsTrue(definition.AllowsCoastPlacement);
        }

        [Test]
        public void MissingPrototypeVisualId_IsRejected()
        {
            var pipeline = new DefinitionLoadPipeline<RoadDefinition>(new RoadDefinitionXmlLoader());
            var documents = new[]
            {
                Doc("a.xml", "<content><roadDefinition id=\"road.prototype.basic\" nameKey=\"road.prototype.basic.name\" /></content>")
            };
            var report = new ContentLoadReport();

            var registry = pipeline.Load(documents, report);

            Assert.AreEqual(0, registry.Count);
            Assert.AreEqual(1, report.ErrorCount);
        }
    }
}
