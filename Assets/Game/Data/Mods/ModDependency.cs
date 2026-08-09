using Varynth.Core.Common;

namespace Varynth.Data.Mods
{
    public readonly struct ModDependency
    {
        public ContentSourceId Id { get; }
        public bool Optional { get; }

        public ModDependency(ContentSourceId id, bool optional)
        {
            Id = id;
            Optional = optional;
        }
    }
}
