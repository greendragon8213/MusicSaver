using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface ISongUrlProvider
    {
        //string GetSongUrl(string songName);
        Task<string> GetSongUrlAsync(string songName);
    }
}
