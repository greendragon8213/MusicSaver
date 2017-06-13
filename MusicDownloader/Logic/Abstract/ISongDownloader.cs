using Logic.Models;

namespace Logic.Abstract
{
    public interface ISongDownloader
    {
        Song GetSong(string songName);
    }
}
