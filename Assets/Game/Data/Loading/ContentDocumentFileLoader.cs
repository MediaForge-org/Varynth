using System.Collections.Generic;
using Varynth.Data.Sources;
using Varynth.Data.Validation;
using Varynth.Data.Xml;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// The missing "file path list -&gt; ContentDocument list" glue: combines the
    /// already-existing, already-hardened ContentFileDiscovery (deterministic
    /// ordinal-sorted .xml discovery) and XmlDocumentReader (XXE-safe parsing) into
    /// ContentDocuments a DefinitionLoadPipeline&lt;T&gt; can consume. No new file-I/O or
    /// XML-security code -- both building blocks already existed, tested, unused.
    /// </summary>
    public static class ContentDocumentFileLoader
    {
        private const string ContentRootElementName = "content";

        public static IReadOnlyList<ContentDocument> LoadFromDirectory(string rootPath, ContentSource source, ContentLoadReport report)
        {
            var documents = new List<ContentDocument>();
            var filePaths = ContentFileDiscovery.DiscoverXmlFiles(rootPath);

            foreach (var filePath in filePaths)
            {
                if (XmlDocumentReader.TryLoad(filePath, source.Id, ContentRootElementName, report, out var document))
                {
                    documents.Add(new ContentDocument(source, filePath, document));
                }
            }

            return documents;
        }
    }
}
