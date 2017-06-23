using System.Threading.Tasks;
using Logic.Exceptions;

namespace Logic.Abstract
{
    public interface ISongUrlProvider
    {

        /// <summary>
        /// Gets song url.
        /// </summary>
        /// <param name="songName">Artist name and song name</param>
        /// <returns>Returns song file url.</returns>
        /// <exception cref="SongNotFoundException">Thrown if song url was not found.</exception>
        Task<string> GetSongUrlAsync(string songName);
    }
}
