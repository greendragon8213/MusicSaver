using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Logic.Abstract;
using Logic.Exceptions;

namespace Logic.Implementation
{
    public class ZfFmSongUrlProvider : ISongUrlProvider
    {
        private const string MusicSearchLink = "https://m.zf.fm/mp3/search?keywords=";
        private readonly HtmlWeb _htmlWeb = new HtmlWeb();
        
        public async Task<string> GetSongUrlAsync(string songName)
        {
            HtmlDocument searchSongsPage = await Task.Run(() => _htmlWeb.Load(MusicSearchLink + songName));
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
