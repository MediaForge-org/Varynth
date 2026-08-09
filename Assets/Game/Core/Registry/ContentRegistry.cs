using System;
using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Definitions;

namespace Varynth.Core.Registry
{
    /// <summary>
    /// Minimal in-memory ID -> Definition map. Deliberately does not load XML, mods,
    /// savegames, prefabs, or Addressables -- that is out of scope for this package
    /// and follows in later, dedicated packages.
    /// </summary>
    public sealed class ContentRegistry<T> where T : IContentDefinition
    {
        private readonly Dictionary<ContentId, T> _entries = new Dictionary<ContentId, T>();

        public int Count => _entries.Count;

        public IReadOnlyCollection<T> All => _entries.Values;

        public void Register(T definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }

            var id = definition.Id;
            if (_entries.ContainsKey(id))
            {
                throw new DuplicateContentIdException(id);
            }

            _entries.Add(id, definition);
        }

        public bool TryGet(ContentId id, out T definition)
        {
            return _entries.TryGetValue(id, out definition);
        }

        public T Get(ContentId id)
        {
            if (!_entries.TryGetValue(id, out var definition))
            {
                throw new KeyNotFoundException($"No content definition registered for id '{id}'.");
            }

            return definition;
        }
    }
}
