using System;
using System.IO;
using System.Net;
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
        
        public Song GetSong(string songName)
        {
            if (string.IsNullOrEmpty(songName))
            {
                return null;
            }

            string songUrl = _songUrlProvider.GetSongUrl(songName);
            
            byte[] songBytes;
            try
            {
                songBytes = _webClient.DownloadData(songUrl);
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
