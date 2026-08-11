using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Definitions.Buildings;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Loader for &lt;buildingDefinition id="..." nameKey="..." footprintWidth="..."
    /// footprintLength="..." prototypeVisualId="..." [allowsCoastPlacement="..."] /&gt;.
    /// Follows the same shape/error-reporting policy as the existing (test-only)
    /// TestDefinitionXmlLoader -- unknown attributes reported as Info (forward-compatible),
    /// missing/invalid required attributes as Error.
    /// </summary>
    public sealed class BuildingDefinitionXmlLoader : IContentDefinitionLoader<BuildingDefinition>
    {
        public string RootElementName => "buildingDefinition";

        public bool TryLoad(XElement element, ContentSourceId source, string filePath, ContentLoadReport report, out BuildingDefinition definition)
        {
            definition = null;

            var idAttribute = element.Attribute("id")?.Value;
            if (!ContentId.TryParse(idAttribute, out var id))
            {
                report.AddError(source, filePath, null, $"<buildingDefinition> has a missing or invalid 'id' attribute: '{idAttribute}'.");
                return false;
            }

            var nameKeyAttribute = element.Attribute("nameKey")?.Value;
            if (!LocalizationKey.TryParse(nameKeyAttribute, out var nameKey))
            {
                report.AddError(source, filePath, id, $"<buildingDefinition id='{id}'> has a missing or invalid 'nameKey' attribute: '{nameKeyAttribute}'.");
                return false;
            }

            var footprintWidthAttribute = element.Attribute("footprintWidth")?.Value;
            if (!int.TryParse(footprintWidthAttribute, out var footprintWidth) || footprintWidth <= 0)
            {
                report.AddError(source, filePath, id, $"<buildingDefinition id='{id}'> has a missing or invalid 'footprintWidth' attribute: '{footprintWidthAttribute}'.");
                return false;
            }

            var footprintLengthAttribute = element.Attribute("footprintLength")?.Value;
            if (!int.TryParse(footprintLengthAttribute, out var footprintLength) || footprintLength <= 0)
            {
                report.AddError(source, filePath, id, $"<buildingDefinition id='{id}'> has a missing or invalid 'footprintLength' attribute: '{footprintLengthAttribute}'.");
                return false;
            }

            var prototypeVisualId = element.Attribute("prototypeVisualId")?.Value;
            if (string.IsNullOrWhiteSpace(prototypeVisualId))
            {
                report.AddError(source, filePath, id, $"<buildingDefinition id='{id}'> has a missing or empty 'prototypeVisualId' attribute.");
                return false;
            }

            var allowsCoastAttribute = element.Attribute("allowsCoastPlacement")?.Value;
            var allowsCoastPlacement = false;
            if (allowsCoastAttribute != null && !bool.TryParse(allowsCoastAttribute, out allowsCoastPlacement))
            {
                report.AddError(source, filePath, id, $"<buildingDefinition id='{id}'> has an invalid 'allowsCoastPlacement' attribute: '{allowsCoastAttribute}'.");
                return false;
            }

            foreach (var attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case "id":
                    case "nameKey":
                    case "footprintWidth":
                    case "footprintLength":
                    case "prototypeVisualId":
                    case "allowsCoastPlacement":
                        continue;
                    default:
                        report.AddInfo(source, filePath, id, $"Unknown attribute '{attribute.Name.LocalName}' on <buildingDefinition> ignored.");
                        break;
                }
            }

            definition = new BuildingDefinition(id, nameKey, footprintWidth, footprintLength, prototypeVisualId, allowsCoastPlacement);
            return true;
        }
    }
}
