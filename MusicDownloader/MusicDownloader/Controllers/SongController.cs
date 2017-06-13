using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using Logic.Abstract;
using Logic.Exceptions;
using Logic.Implementation;
using Logic.Models;

namespace MusicDownloader.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongUrlProvider _songUrlProvider;
        private readonly ISongDownloader _songDownloader;
        public SongController()//ToDo
        {
            _songUrlProvider = new ZfFmSongUrlProvider();
            _songDownloader = new SongDownloader(_songUrlProvider);
        }

        // GET: Song
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public void DownloadAll()
        {
            List<string> songNames = new List<string>();
            songNames.Add("IAMX - Kingdom of Welcome Addiction");
            songNames.Add("IAMX - Imaginary begins");
            songNames.Add("bevurgfeuryfgeugy");

            List<Song> songs = new List<Song>();
            StringBuilder logStringBuilder = new StringBuilder();
            foreach (var songName in songNames)
            {
                try
                {
                    if (!string.IsNullOrEmpty(songName))
                    {
                        songs.Add(_songDownloader.GetSong(songName));
                    }
                }
                catch (SongNotFoundException)
                {
                    if (logStringBuilder.Length == 0)
                    {
                        logStringBuilder.AppendLine("This is the list of songs we couldn't download:");
                    }
                    logStringBuilder.AppendLine(songName);
                }
            }

            string log = logStringBuilder.ToString();
            if (string.IsNullOrEmpty(log))
            {
                log = "All songs have been downloaded successfylly.";
            }

            DownloadZip(songs, log);
        }

        private void DownloadZip(List<Song> songs, string readmeContent)
        {
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "Music" + ".zip");
            Response.ContentType = "application/zip";

            using (var zipStream = new ZipOutputStream(Response.OutputStream))
            {
                foreach (Song song in songs)
                {
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    byte[] fileBytes = song.EntryBytes;
                    var fileEntry = new ZipEntry(Path.GetFileName(song.FullName + song.FileExtension))
                    {
                        Size = fileBytes.Length
                    };

                    zipStream.PutNextEntry(fileEntry);
                    zipStream.Write(fileBytes, 0, fileBytes.Length);
                }

                //adding readme
                byte[] readmeFileBytes = System.Text.Encoding.Unicode.GetBytes(readmeContent);
                zipStream.PutNextEntry(new ZipEntry(Path.GetFileName("readme.txt"))
                {
                    Size = readmeFileBytes.Length
                });
                zipStream.Write(readmeFileBytes, 0, readmeFileBytes.Length);

                zipStream.Flush();
                zipStream.Close();
            }
        }
    }
}