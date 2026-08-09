using Varynth.Core.Common;
using Varynth.Core.Definitions;

namespace Varynth.Tests.EditMode
{
    /// <summary>
    /// A second, distinct test-only definition type -- exists purely so tests can prove
    /// ContentReference&lt;T&gt;/ContentRegistry&lt;T&gt; type-safety: an id registered in a
    /// ContentRegistry&lt;TestDefinition&gt; must not resolve against a
    /// ContentRegistry&lt;OtherTestDefinition&gt;, even though both implement IContentDefinition.
    /// </summary>
    internal sealed class OtherTestDefinition : IContentDefinition
    {
        public ContentId Id { get; }
        public LocalizationKey NameKey { get; }

        public OtherTestDefinition(string id, string nameKey)
        {
            Id = ContentId.Parse(id);
            NameKey = LocalizationKey.Parse(nameKey);
        }
    }
}
