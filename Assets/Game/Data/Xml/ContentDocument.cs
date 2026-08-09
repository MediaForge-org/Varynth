using System.Xml.Linq;
using Varynth.Data.Sources;

namespace Varynth.Data.Xml
{
    /// <summary>
    /// A syntactically-valid, already-parsed XML document plus the ContentSource it came
    /// from and the file it was read from. The result of steps 4-5 of the content load
    /// pipeline (discovery + syntactic parse), ready for per-definition-type parsing.
    /// </summary>
    public readonly struct ContentDocument
    {
        public ContentSource Source { get; }
        public string FilePath { get; }
        public XDocument Document { get; }

        public ContentDocument(ContentSource source, string filePath, XDocument document)
        {
            Source = source;
            FilePath = filePath;
            Document = document;
        }
    }
}
