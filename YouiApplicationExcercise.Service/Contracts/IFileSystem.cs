using System.IO;

namespace YouiApplicationExcercise.Service.Contracts
{
    public interface IFileSystem
    {
        StreamReader OpenFileReader(string path, FileMode mode);
        bool FileExists(string path);
    }
}
