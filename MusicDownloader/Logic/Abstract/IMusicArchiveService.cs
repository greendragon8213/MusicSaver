using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IMusicArchiveService
    {
        Task<string> CreateMusicArchive(List<string> songsList, string path);
        Stream GetStream(string path);
        void DeleteFile(string path);
    }
}
