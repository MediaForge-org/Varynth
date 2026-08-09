using System;
using System.Collections.Generic;
using System.IO;

namespace Varynth.Data.Loading
{
    /// <summary>
    /// Enumerates content XML files under a source's root path, in a deterministic
    /// (ordinal-sorted) order -- never raw filesystem/directory-enumeration order, which
    /// is not guaranteed stable across platforms or runs.
    /// </summary>
    public static class ContentFileDiscovery
    {
        public static IReadOnlyList<string> DiscoverXmlFiles(string rootPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
            {
                return Array.Empty<string>();
            }

            var files = new List<string>(Directory.EnumerateFiles(rootPath, "*.xml", SearchOption.AllDirectories));
            files.Sort(StringComparer.Ordinal);
            return files;
        }
    }
}
