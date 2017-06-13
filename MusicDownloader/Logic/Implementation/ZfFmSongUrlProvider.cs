using System;
using HtmlAgilityPack;
using Logic.Abstract;
using Logic.Exceptions;

namespace Logic.Implementation
{
    public class ZfFmSongUrlProvider : ISongUrlProvider
    {
        private const string MusicSearchLink = "https://m.zf.fm/mp3/search?keywords=";
        //private const string BaseUrl = "https://zf.fm";
        
        public string GetSongUrl(string songName)
        {
            HtmlDocument searchSongsPage = new HtmlWeb().Load(MusicSearchLink + songName);
            HtmlNode mp3FileFirstNode = searchSongsPage.DocumentNode.SelectSingleNode("//li[@class='tracks-item']");
            try
            {
                return mp3FileFirstNode.GetAttributeValue("data-url", "");
            }
            catch (Exception)
            {
                throw new SongNotFoundException("Cannot get song url.");
            }
        }
    }
}
