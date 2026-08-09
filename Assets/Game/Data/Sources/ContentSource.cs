using System;
using System.Collections.Generic;
using Varynth.Core.Common;
using Varynth.Data.Mods;

namespace Varynth.Data.Sources
{
    /// <summary>
    /// Plain data describing where content comes from and how it participates in load
    /// ordering. Deliberately not a god-class: it knows nothing about XML, files on disk
    /// beyond its own RootPath string, or how to actually load anything -- that is
    /// Loading/DefinitionLoadPipeline's job.
    /// </summary>
    public sealed class ContentSource
    {
        private static readonly IReadOnlyList<ContentSourceId> EmptyIds = Array.Empty<ContentSourceId>();

        public ContentSourceId Id { get; }
        public ContentSourceType Type { get; }
        public string RootPath { get; }
        public int Priority { get; }
        public IReadOnlyList<ContentSourceId> RequiredDependencies { get; }
        public IReadOnlyList<ContentSourceId> OptionalDependencies { get; }
        public IReadOnlyList<ContentSourceId> LoadAfter { get; }

        public ContentSource(
            ContentSourceId id,
            ContentSourceType type,
            string rootPath,
            int priority = 0,
            IReadOnlyList<ContentSourceId> requiredDependencies = null,
            IReadOnlyList<ContentSourceId> optionalDependencies = null,
            IReadOnlyList<ContentSourceId> loadAfter = null)
        {
            if (id.IsDefault)
            {
                throw new ArgumentException("Content source id must be a valid, parsed ContentSourceId.", nameof(id));
            }

            Id = id;
            Type = type;
            RootPath = rootPath ?? string.Empty;
            Priority = priority;
            RequiredDependencies = requiredDependencies ?? EmptyIds;
            OptionalDependencies = optionalDependencies ?? EmptyIds;
            LoadAfter = loadAfter ?? EmptyIds;
        }

        public static ContentSource FromModManifest(ModManifest manifest, string rootPath, int priority = 0)
        {
            if (manifest is null)
            {
                throw new ArgumentNullException(nameof(manifest));
            }

            var required = new List<ContentSourceId>();
            var optional = new List<ContentSourceId>();

            foreach (var dependency in manifest.Dependencies)
            {
                if (dependency.Optional)
                {
                    optional.Add(dependency.Id);
                }
                else
                {
                    required.Add(dependency.Id);
                }
            }

            return new ContentSource(manifest.Id, ContentSourceType.Mod, rootPath, priority, required, optional, manifest.LoadAfter);
        }
    }
}
