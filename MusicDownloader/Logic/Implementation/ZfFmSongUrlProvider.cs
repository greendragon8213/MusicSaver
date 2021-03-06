﻿using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Logic.Abstract;
using Logic.Exceptions;
using Logic.Resources;

namespace Logic.Implementation
{
    public class ZfFmSongUrlProvider : ISongUrlProvider
    {
        private const string MusicSearchLink = "https://m.zf.fm/mp3/search?keywords=";
        private readonly HtmlWeb _htmlWeb = new HtmlWeb();

        public async Task<string> GetSongUrlAsync(string songName)
        {
            try
            {
                HtmlDocument searchSongsPage = await Task.Run(() => _htmlWeb.Load(MusicSearchLink + songName));
                HtmlNode mp3FileFirstNode = searchSongsPage.DocumentNode.SelectSingleNode("//li[@class='tracks-item']");
            
                return mp3FileFirstNode.GetAttributeValue("data-url", "");
            }
            catch (Exception)
            {
                //ToDo timeout exception
                throw new SongNotFoundException(ExceptionMessages.CannotGetSongUrl);
            }
        }
    }
}
