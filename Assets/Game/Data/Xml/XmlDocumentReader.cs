using System.IO;
using System.Xml;
using System.Xml.Linq;
using Varynth.Core.Common;
using Varynth.Data.Validation;

namespace Varynth.Data.Xml
{
    /// <summary>
    /// The single hardened entry point for turning a file on disk into a syntactically
    /// valid XDocument. Content XML (core, mod, or test) is treated as untrusted
    /// declarative input: no implicit XDocument.Load defaults are relied on. DOCTYPE
    /// processing is prohibited outright (also the primary XXE defense, since entities
    /// cannot be declared without a DTD), the resolver is disabled as defense in depth,
    /// and document size is bounded and configurable. No other code path in this
    /// assembly is allowed to call XDocument.Load/XDocument.Parse directly.
    /// </summary>
    public static class XmlDocumentReader
    {
        public const long DefaultMaxDocumentSizeChars = 10_000_000;

        public static bool TryLoad(
            string filePath,
            ContentSourceId? source,
            string expectedRootName,
            ContentLoadReport report,
            out XDocument document,
            long maxDocumentSizeChars = DefaultMaxDocumentSizeChars)
        {
            document = null;

            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
                MaxCharactersInDocument = maxDocumentSizeChars,
                CloseInput = true
            };

            try
            {
                using (var stream = File.OpenRead(filePath))
                using (var reader = XmlReader.Create(stream, settings))
                {
                    var loaded = XDocument.Load(reader, LoadOptions.SetLineInfo);

                    var rootName = loaded.Root?.Name.LocalName;
                    if (rootName != expectedRootName)
                    {
                        report.AddError(source, filePath, null,
                            $"Unexpected root element: expected '{expectedRootName}' but found '{rootName ?? "<none>"}'.");
                        return false;
                    }

                    document = loaded;
                    return true;
                }
            }
            catch (XmlException ex)
            {
                report.AddError(source, filePath, null, $"Failed to parse XML: {ex.Message}");
                return false;
            }
            catch (IOException ex)
            {
                report.AddError(source, filePath, null, $"Failed to read file: {ex.Message}");
                return false;
            }
        }
    }
}
