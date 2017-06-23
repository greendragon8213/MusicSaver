using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Logic.Abstract;
using Logic.Exceptions;
using Logic.Models;

namespace Logic.Implementation
{
    public class SongDownloader : ISongDownloader
    {
        private readonly WebClient _webClient = new WebClient();
        private readonly ISongUrlProvider _songUrlProvider;

        public SongDownloader(ISongUrlProvider songUrlProvider)
        {
            _songUrlProvider = songUrlProvider;
        }
        
        public async Task<Song> GetSongAsync(string songName)
        {
            if (string.IsNullOrEmpty(songName))
            {
                throw new ArgumentNullException(nameof(songName));
            }

            string songUrl = await _songUrlProvider.GetSongUrlAsync(songName);
            
            byte[] songBytes;
            try
            {
                songBytes = await _webClient.DownloadDataTaskAsync(songUrl);
            }
            catch (Exception)
            {
                throw new SongNotFoundException("Cannot download song by url.");
            }

            return new Song()
            {
                FullName = songName,
                EntryBytes = songBytes,
                FileExtension = Path.GetExtension(songUrl)
            };
        }
    }
}
