using Varynth.Core.Common;
using Varynth.Core.Registry;

namespace Varynth.Core.Definitions
{
    /// <summary>
    /// A reference to another content definition, held as a stable ContentId rather than
    /// an object copy (Recipe -> Good, Residence -> Need, Research -> Unlock, ...).
    /// Resolution is explicit and typed against a specific ContentRegistry&lt;T&gt; --
    /// resolving against the wrong registry type simply fails to find the id, it never
    /// silently returns something of the wrong kind.
    /// </summary>
    public readonly struct ContentReference<T> where T : class, IContentDefinition
    {
        public ContentId Id { get; }

        private ContentReference(ContentId id)
        {
            Id = id;
        }

        public static ContentReference<T> To(ContentId id)
        {
            return new ContentReference<T>(id);
        }

        public bool TryResolve(ContentRegistry<T> registry, out T definition)
        {
            return registry.TryGet(Id, out definition);
        }
    }
}
