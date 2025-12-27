using System.Collections.Generic;
using System.IO;


namespace DoomMapGuessr.Helpers
{

    internal static class DirectoryStringExtensions
    {

        internal static IEnumerable<string> EnumerateDirectories(this string directory) => Directory.EnumerateDirectories(directory);

        internal static IEnumerable<string> EnumerateFiles(this string directory) => Directory.EnumerateFiles(directory);

    }

}
