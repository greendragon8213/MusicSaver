using System.Threading.Tasks;
using Logic.Exceptions;
using Logic.Models;

namespace Logic.Abstract
{
    public interface ISongDownloader
    {
        /// <summary>
        /// Gets song.
        /// </summary>
        /// <param name="songName">Artist name and song name</param>
        /// <returns>Returns song.</returns>
        /// <exception cref="SongNotFoundException">Thrown if song url was not found or cannot cownload song by found url.</exception>
        Task<Song> GetSongAsync(string songName);
    }
}
