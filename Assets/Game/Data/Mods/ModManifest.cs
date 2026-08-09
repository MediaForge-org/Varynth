using System;
using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Core.Definitions;

namespace Varynth.Data.Mods
{
    /// <summary>
    /// What a mod declares about itself: identity, version, display name key, and its
    /// dependency/ordering relationships to other content sources. Purely a data holder --
    /// turning this into a loadable ContentSource is ContentSource.FromModManifest's job.
    /// </summary>
    public sealed class ModManifest
    {
        private static readonly IReadOnlyList<ModDependency> EmptyDependencies = Array.Empty<ModDependency>();
        private static readonly IReadOnlyList<ContentSourceId> EmptyIds = Array.Empty<ContentSourceId>();

        public ContentSourceId Id { get; }
        public string Version { get; }
        public LocalizationKey NameKey { get; }
        public IReadOnlyList<ModDependency> Dependencies { get; }
        public IReadOnlyList<ContentSourceId> LoadAfter { get; }

        public ModManifest(
            ContentSourceId id,
            string version,
            LocalizationKey nameKey,
            IReadOnlyList<ModDependency> dependencies = null,
            IReadOnlyList<ContentSourceId> loadAfter = null)
        {
            if (id.IsDefault)
            {
                throw new ArgumentException("Mod manifest id must be a valid, parsed ContentSourceId.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("Mod manifest version must not be empty.", nameof(version));
            }

            Id = id;
            Version = version;
            NameKey = nameKey;
            Dependencies = dependencies ?? EmptyDependencies;
            LoadAfter = loadAfter ?? EmptyIds;
        }
    }
}
