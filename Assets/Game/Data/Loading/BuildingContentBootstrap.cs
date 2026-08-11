using Varynth.Core.Common;
using Varynth.Core.Definitions.Buildings;
using Varynth.Core.Registry;
using Varynth.Data.Sources;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Ties ContentDocumentFileLoader + DefinitionLoadPipeline&lt;BuildingDefinition&gt;
    /// together into one call. This assembly has noEngineReferences: true, so it
    /// cannot resolve Application.streamingAssetsPath itself -- the caller (Presentation,
    /// which does have engine references) resolves the real on-disk content root and
    /// passes it in as a plain string. The first real (non-test) exercise of the whole
    /// Phase 1B content pipeline end-to-end.
    /// </summary>
    public static class BuildingContentBootstrap
    {
        private static readonly ContentSource PrototypeSource =
            new ContentSource(ContentSourceId.Parse("core"), ContentSourceType.Core, string.Empty);

        public static ContentRegistry<BuildingDefinition> LoadRegistry(string contentRootPath, ContentLoadReport report = null)
        {
            report = report ?? new ContentLoadReport();

            var documents = ContentDocumentFileLoader.LoadFromDirectory(contentRootPath, PrototypeSource, report);
            var pipeline = new DefinitionLoadPipeline<BuildingDefinition>(new BuildingDefinitionXmlLoader());
            return pipeline.Load(documents, report);
        }
    }
}
