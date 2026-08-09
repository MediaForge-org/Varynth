using Varynth.Core.Common;

namespace Varynth.Core.Definitions
{
    /// <summary>
    /// Smallest common shape shared by every datengetrieben content definition.
    /// Concrete definition types (ResidenceDefinition, GoodDefinition, ...) follow in
    /// later packages -- this package only needs enough surface to make
    /// ContentRegistry&lt;T&gt; and its tests meaningful.
    /// </summary>
    public interface IContentDefinition
    {
        ContentId Id { get; }

        LocalizationKey NameKey { get; }
    }
}
