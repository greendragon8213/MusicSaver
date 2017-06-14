using System.Threading.Tasks;
using Logic.Models;

namespace Logic.Abstract
{
    public interface ISongDownloader
    {
        //Song GetSong(string songName);
        Task<Song> GetSongAsync(string songName);
    }
}
