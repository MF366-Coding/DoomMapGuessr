using System.IO;

using Avalonia.Data;


namespace DoomMapGuessr.Services
{

    public sealed class ApplicationCache(
        ApplicationSettings applicationSettings,
        string cacheDirName = "AppCache"
    )
    {

        public string AppCacheDirectory => Path.Join(applicationSettings.DirectoryPath, cacheDirName);

        public void Add(string id, byte[] data) => File.WriteAllBytes(Path.Join(AppCacheDirectory, id), data);

        public void Clear()
        {

            foreach (string file in Directory.EnumerateFiles(AppCacheDirectory))
                File.Delete(file);

            foreach (string dir in Directory.EnumerateDirectories(AppCacheDirectory))
                File.Delete(dir);

        }

        public Optional<byte[]> Get(string id)
        {

            string filepath = Path.Join(AppCacheDirectory, id);

            return !File.Exists(filepath) ? Optional<byte[]>.Empty : File.ReadAllBytes(filepath);

        }

    }

}
