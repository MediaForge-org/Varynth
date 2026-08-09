using System;
using System.Collections.Generic;
using Varynth.Core.Definitions;
using Varynth.Core.Registry;
using Varynth.Data.Sources;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Runs one IContentDefinitionLoader&lt;T&gt; over an ordered set of already-parsed
    /// content documents, building a ContentRegistry&lt;T&gt;. A structurally invalid
    /// element, a duplicate id, or a mod defining outside its own namespace are all
    /// reported and simply never reach the registry -- the rest of the load continues.
    ///
    /// Known simplification: since Phase 1B has exactly one concrete (test-only)
    /// definition type, any &lt;content&gt; child whose tag isn't this loader's
    /// RootElementName is reported as an unrecognized element. A future package with
    /// multiple concrete loaders will need a small explicit per-run registration table
    /// (still no reflection) so "not mine" and "unknown to the whole system" can be told
    /// apart; not needed yet with a single loader.
    /// </summary>
    public sealed class DefinitionLoadPipeline<T> where T : class, IContentDefinition
    {
        private readonly IContentDefinitionLoader<T> _loader;

        public DefinitionLoadPipeline(IContentDefinitionLoader<T> loader)
        {
            _loader = loader;
        }

        public ContentRegistry<T> Load(IEnumerable<ContentDocument> documents, ContentLoadReport report)
        {
            var registry = new ContentRegistry<T>();

            foreach (var contentDocument in documents)
            {
                var root = contentDocument.Document.Root;
                if (root == null)
                {
                    continue;
                }

                foreach (var element in root.Elements())
                {
                    if (element.Name.LocalName != _loader.RootElementName)
                    {
                        report.AddInfo(contentDocument.Source.Id, contentDocument.FilePath, null,
                            $"Unrecognized content element '<{element.Name.LocalName}>' -- not handled by this loader.");
                        continue;
                    }

                    if (!_loader.TryLoad(element, contentDocument.Source.Id, contentDocument.FilePath, report, out var definition))
                    {
                        continue;
                    }

                    if (!IsOwnedByMod(definition, contentDocument.Source, out var ownershipError))
                    {
                        report.AddError(contentDocument.Source.Id, contentDocument.FilePath, definition.Id, ownershipError);
                        continue;
                    }

                    try
                    {
                        registry.Register(definition);
                    }
                    catch (DuplicateContentIdException ex)
                    {
                        report.AddError(contentDocument.Source.Id, contentDocument.FilePath, ex.Id,
                            $"Duplicate content id '{ex.Id}' -- definition rejected, first registration kept.");
                    }
                }
            }

            return registry;
        }

        private static bool IsOwnedByMod(T definition, ContentSource source, out string error)
        {
            error = null;

            if (source.Type != ContentSourceType.Mod)
            {
                return true;
            }

            var idText = definition.Id.ToString();
            var prefix = source.Id.ToString();

            if (idText == prefix || idText.StartsWith(prefix + ".", StringComparison.Ordinal))
            {
                return true;
            }

            error = $"Mod '{source.Id}' attempted to define id '{idText}' outside its own namespace; rejected.";
            return false;
        }
    }
}
