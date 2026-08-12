using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Roads;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Loader for &lt;roadDefinition id="..." nameKey="..." prototypeVisualId="..."
    /// [logicalWidthCells="..."] [allowsDiagonalSegments="..."] [allowsCoastPlacement="..."] /&gt;.
    /// Same shape/error-reporting policy as BuildingDefinitionXmlLoader.
    /// </summary>
    public sealed class RoadDefinitionXmlLoader : IContentDefinitionLoader<RoadDefinition>
    {
        public string RootElementName => "roadDefinition";

        public bool TryLoad(XElement element, ContentSourceId source, string filePath, ContentLoadReport report, out RoadDefinition definition)
        {
            definition = null;

            var idAttribute = element.Attribute("id")?.Value;
            if (!ContentId.TryParse(idAttribute, out var id))
            {
                report.AddError(source, filePath, null, $"<roadDefinition> has a missing or invalid 'id' attribute: '{idAttribute}'.");
                return false;
            }

            var nameKeyAttribute = element.Attribute("nameKey")?.Value;
            if (!LocalizationKey.TryParse(nameKeyAttribute, out var nameKey))
            {
                report.AddError(source, filePath, id, $"<roadDefinition id='{id}'> has a missing or invalid 'nameKey' attribute: '{nameKeyAttribute}'.");
                return false;
            }

            var prototypeVisualId = element.Attribute("prototypeVisualId")?.Value;
            if (string.IsNullOrWhiteSpace(prototypeVisualId))
            {
                report.AddError(source, filePath, id, $"<roadDefinition id='{id}'> has a missing or empty 'prototypeVisualId' attribute.");
                return false;
            }

            var widthAttribute = element.Attribute("logicalWidthCells")?.Value;
            var logicalWidthCells = 1;
            if (widthAttribute != null && (!int.TryParse(widthAttribute, out logicalWidthCells) || logicalWidthCells <= 0))
            {
                report.AddError(source, filePath, id, $"<roadDefinition id='{id}'> has an invalid 'logicalWidthCells' attribute: '{widthAttribute}'.");
                return false;
            }

            var diagonalAttribute = element.Attribute("allowsDiagonalSegments")?.Value;
            var allowsDiagonalSegments = true;
            if (diagonalAttribute != null && !bool.TryParse(diagonalAttribute, out allowsDiagonalSegments))
            {
                report.AddError(source, filePath, id, $"<roadDefinition id='{id}'> has an invalid 'allowsDiagonalSegments' attribute: '{diagonalAttribute}'.");
                return false;
            }

            var coastAttribute = element.Attribute("allowsCoastPlacement")?.Value;
            var allowsCoastPlacement = false;
            if (coastAttribute != null && !bool.TryParse(coastAttribute, out allowsCoastPlacement))
            {
                report.AddError(source, filePath, id, $"<roadDefinition id='{id}'> has an invalid 'allowsCoastPlacement' attribute: '{coastAttribute}'.");
                return false;
            }

            foreach (var attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case "id":
                    case "nameKey":
                    case "prototypeVisualId":
                    case "logicalWidthCells":
                    case "allowsDiagonalSegments":
                    case "allowsCoastPlacement":
                        continue;
                    default:
                        report.AddInfo(source, filePath, id, $"Unknown attribute '{attribute.Name.LocalName}' on <roadDefinition> ignored.");
                        break;
                }
            }

            definition = new RoadDefinition(id, nameKey, prototypeVisualId, logicalWidthCells, allowsDiagonalSegments, allowsCoastPlacement);
            return true;
        }
    }
}
