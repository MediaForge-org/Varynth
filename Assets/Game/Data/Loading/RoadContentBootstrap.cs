using Varynth.Core.Common;
using Varynth.Core.Definitions.Roads;
using Varynth.Core.Registry;
using Varynth.Data.Sources;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Ties ContentDocumentFileLoader + DefinitionLoadPipeline&lt;RoadDefinition&gt;
    /// together -- mirrors BuildingContentBootstrap exactly, reusing the same
    /// engine-free primitives.
    /// </summary>
    public static class RoadContentBootstrap
    {
        private static readonly ContentSource PrototypeSource =
            new ContentSource(ContentSourceId.Parse("core"), ContentSourceType.Core, string.Empty);

        public static ContentRegistry<RoadDefinition> LoadRegistry(string contentRootPath, ContentLoadReport report = null)
        {
            report = report ?? new ContentLoadReport();

            var documents = ContentDocumentFileLoader.LoadFromDirectory(contentRootPath, PrototypeSource, report);
            var pipeline = new DefinitionLoadPipeline<RoadDefinition>(new RoadDefinitionXmlLoader());
            return pipeline.Load(documents, report);
        }
    }
}
