using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Data.Validation;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// A hand-written, type-safe parser for exactly one XML element shape into exactly
    /// one definition type. No reflection, no dynamic type resolution -- every supported
    /// definition type gets its own loader, registered explicitly by calling code.
    /// </summary>
    public interface IContentDefinitionLoader<T> where T : class, IContentDefinition
    {
        string RootElementName { get; }

        bool TryLoad(XElement element, ContentSourceId source, string filePath, ContentLoadReport report, out T definition);
    }
}
