using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Data.Loading;
using Varynth.Data.Validation;

namespace Varynth.Tests.EditMode.Data
{
    /// <summary>
    /// Test-only loader for &lt;testDefinition id="..." nameKey="..." /&gt;, parsing into the
    /// existing Phase 1A TestDefinition type. Reports unknown attributes as Info (forward-
    /// compatible policy), missing/invalid id or nameKey as Error.
    /// </summary>
    internal sealed class TestDefinitionXmlLoader : IContentDefinitionLoader<TestDefinition>
    {
        public string RootElementName => "testDefinition";

        public bool TryLoad(XElement element, ContentSourceId source, string filePath, ContentLoadReport report, out TestDefinition definition)
        {
            definition = null;

            var idAttribute = element.Attribute("id")?.Value;
            if (!ContentId.TryParse(idAttribute, out var id))
            {
                report.AddError(source, filePath, null, $"<testDefinition> has a missing or invalid 'id' attribute: '{idAttribute}'.");
                return false;
            }

            var nameKeyAttribute = element.Attribute("nameKey")?.Value;
            if (!LocalizationKey.TryParse(nameKeyAttribute, out _))
            {
                report.AddError(source, filePath, id, $"<testDefinition id='{id}'> has a missing or invalid 'nameKey' attribute: '{nameKeyAttribute}'.");
                return false;
            }

            foreach (var attribute in element.Attributes())
            {
                if (attribute.Name.LocalName != "id" && attribute.Name.LocalName != "nameKey")
                {
                    report.AddInfo(source, filePath, id, $"Unknown attribute '{attribute.Name.LocalName}' on <testDefinition> ignored.");
                }
            }

            definition = new TestDefinition(idAttribute, nameKeyAttribute);
            return true;
        }
    }
}
