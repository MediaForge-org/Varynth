using System.Collections.Generic;
using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Data.Mods
{
    /// <summary>
    /// Parses the minimal mod manifest schema:
    /// &lt;mod id="author.modname" version="1.0.0" nameKey="mod.author.modname.name"&gt;
    ///   &lt;dependencies&gt;
    ///     &lt;dependency id="someother.mod" /&gt;
    ///     &lt;dependency id="anothermod" optional="true" /&gt;
    ///   &lt;/dependencies&gt;
    ///   &lt;loadAfter&gt;
    ///     &lt;source id="core" /&gt;
    ///   &lt;/loadAfter&gt;
    /// &lt;/mod&gt;
    ///
    /// TryRead only ever does semantic parsing of an already-loaded, already-hardened
    /// XDocument -- it never opens a file or calls XDocument.Load itself, so it cannot
    /// accidentally bypass XmlDocumentReader's hardening. It stays internal; the only
    /// public entry point is TryReadFromFile.
    /// </summary>
    public static class ModManifestXmlReader
    {
        private const string RootElementName = "mod";

        public static bool TryReadFromFile(string manifestPath, ContentLoadReport report, out ModManifest manifest)
        {
            manifest = null;

            if (!XmlDocumentReader.TryLoad(manifestPath, source: null, expectedRootName: RootElementName, report, out var document))
            {
                return false;
            }

            return TryRead(document, manifestPath, report, out manifest);
        }

        internal static bool TryRead(XDocument document, string filePath, ContentLoadReport report, out ModManifest manifest)
        {
            manifest = null;

            var root = document?.Root;
            if (root == null || root.Name.LocalName != RootElementName)
            {
                report.AddError(null, filePath, null,
                    $"Unexpected root element: expected '{RootElementName}' but found '{root?.Name.LocalName ?? "<none>"}'.");
                return false;
            }

            var idAttribute = root.Attribute("id")?.Value;
            if (!ContentSourceId.TryParse(idAttribute, out var id))
            {
                report.AddError(null, filePath, null, $"Mod manifest has a missing or invalid 'id' attribute: '{idAttribute}'.");
                return false;
            }

            var version = root.Attribute("version")?.Value;
            if (string.IsNullOrWhiteSpace(version))
            {
                report.AddError(id, filePath, null, "Mod manifest is missing a non-empty 'version' attribute.");
                return false;
            }

            var nameKeyAttribute = root.Attribute("nameKey")?.Value;
            if (!LocalizationKey.TryParse(nameKeyAttribute, out var nameKey))
            {
                report.AddError(id, filePath, null, $"Mod manifest has a missing or invalid 'nameKey' attribute: '{nameKeyAttribute}'.");
                return false;
            }

            var dependencies = ReadDependencies(root, id, filePath, report);
            var loadAfter = ReadLoadAfter(root, id, filePath, report);

            manifest = new ModManifest(id, version, nameKey, dependencies, loadAfter);
            return true;
        }

        private static List<ModDependency> ReadDependencies(XElement root, ContentSourceId id, string filePath, ContentLoadReport report)
        {
            var result = new List<ModDependency>();
            var dependenciesElement = root.Element("dependencies");
            if (dependenciesElement == null)
            {
                return result;
            }

            foreach (var dependencyElement in dependenciesElement.Elements("dependency"))
            {
                var dependencyIdAttribute = dependencyElement.Attribute("id")?.Value;
                if (!ContentSourceId.TryParse(dependencyIdAttribute, out var dependencyId))
                {
                    report.AddError(id, filePath, null, $"Mod manifest has an invalid dependency id: '{dependencyIdAttribute}'.");
                    continue;
                }

                var optional = false;
                var optionalAttribute = dependencyElement.Attribute("optional")?.Value;
                if (optionalAttribute != null && !bool.TryParse(optionalAttribute, out optional))
                {
                    report.AddWarning(id, filePath, null,
                        $"Dependency '{dependencyId}' has an invalid 'optional' value '{optionalAttribute}', defaulting to false.");
                    optional = false;
                }

                result.Add(new ModDependency(dependencyId, optional));
            }

            return result;
        }

        private static List<ContentSourceId> ReadLoadAfter(XElement root, ContentSourceId id, string filePath, ContentLoadReport report)
        {
            var result = new List<ContentSourceId>();
            var loadAfterElement = root.Element("loadAfter");
            if (loadAfterElement == null)
            {
                return result;
            }

            foreach (var sourceElement in loadAfterElement.Elements("source"))
            {
                var sourceIdAttribute = sourceElement.Attribute("id")?.Value;
                if (!ContentSourceId.TryParse(sourceIdAttribute, out var sourceId))
                {
                    report.AddError(id, filePath, null, $"Mod manifest has an invalid loadAfter source id: '{sourceIdAttribute}'.");
                    continue;
                }

                result.Add(sourceId);
            }

            return result;
        }
    }
}
