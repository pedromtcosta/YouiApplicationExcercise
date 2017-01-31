using System;
using System.IO;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Service
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public StreamReader OpenFileReader(string path, FileMode mode)
        {
            return new StreamReader(File.Open(path, mode));
        }

        public void WriteAllTextToFile(string file, string text)
        {
            File.WriteAllText(file, text);
        }
    }
}
