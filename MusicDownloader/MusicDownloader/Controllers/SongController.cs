using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using Logic.Abstract;
using MusicDownloader.Resources;

namespace MusicDownloader.Controllers
{
    public class SongController : BaseController
    {
        //private readonly ISongUrlProvider _songUrlProvider;
        //private readonly ISongDownloader _songDownloader;
        private readonly IMusicArchiveService _musicArchiveService;
        public SongController(ILog logger, 
            //ISongUrlProvider songUrlProvider, ISongDownloader songDownloader, 
            IMusicArchiveService musicArchiveService):base(logger)
        {
            //_songUrlProvider = songUrlProvider;
            //_songDownloader = songDownloader;
            _musicArchiveService = musicArchiveService;
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> FormMusicArchive([FromBody]List<string> songsList)
        {
            if(songsList == null || songsList.Count == 0)
                throw new ArgumentException(ErrorMessages.SongsListIsEmpty);

            string temporaryFilesPath = Path.Combine(Server.MapPath("~"), 
                (ConfigurationManager.AppSettings["TemporaryFilesFolderName"]));
            string savedFileName = await _musicArchiveService.CreateMusicArchive(songsList, temporaryFilesPath);

            return Json(new { fileName = savedFileName });
        }

        public void DownloadMusicArchive(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentException(ErrorMessages.MusicArchiveNameIsEmpty);

            string temporaryFilesPath = Path.Combine(Server.MapPath("~"),
                    (ConfigurationManager.AppSettings["TemporaryFilesFolderName"]));

            Stream musicStream = _musicArchiveService.GetStream(Path.Combine(temporaryFilesPath, fileName));
            DownloadStream(musicStream);

            musicStream = null;
            _musicArchiveService.DeleteFile(Path.Combine(temporaryFilesPath, fileName));
        }
        
        #region Private methods

        private void DownloadStream(Stream inputStream)
        {
            try
            {
                Response.Buffer = false;

                // Setting the unknown [ContentType]
                // will display the saving dialog for the user
                Response.ContentType = "application/octet-stream";

                // With setting the file name,
                // in the saving dialog, user will see
                // the [strFileName] name instead of [download]!
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "MusicArchive.zip");

                long fileLength = inputStream.Length;

                // Notify user (client) the total file length
                Response.AddHeader("Content-Length", fileLength.ToString());

                // Total bytes that should be read
                long fileLengthToRead = fileLength;

                // 32KB
                int bufferSize = 32 * 1024;

                // Create buffer for reading [intBufferSize] bytes from file
                byte[] buffer = new byte[bufferSize];

                // Read the bytes of file
                while (fileLengthToRead > 0)
                {
                    // Verify that the client is connected or not
                    if (Response.IsClientConnected)
                    {
                        // Read the data and put it in the buffer.
                        int bytesNumberReadFromStream =
                            inputStream.Read(buffer: buffer, offset: 0, count: bufferSize);

                        // Write the data from buffer to the current output stream.
                        Response.OutputStream.Write
                            (buffer: buffer, offset: 0,
                            count: bytesNumberReadFromStream);

                        // Send the data to output (Don't buffer in server's RAM!!!)
                        Response.Flush();

                        fileLengthToRead = fileLengthToRead - bytesNumberReadFromStream;
                    }
                    else
                    {
                        // Prevent infinite loop if user disconnected!
                        fileLengthToRead = -1;
                    }
                }
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Flush();
                    //inputStream.Close();
                    inputStream.Dispose();
                }
                Response.Close();
            }
        }

        //private List<string> GetSongList(string inlineSongsList)
        //{
        //    List<string> songList = inlineSongsList.Split('\n').ToList();

        //    songList = RemoveTimeSpan(songList);
        //    songList = RemoveEmpty(songList);
        //    songList = songList.Distinct().ToList();

        //    return songList;
        //}

        //private List<string> RemoveEmpty(List<string> strings)
        //{
        //    strings.RemoveAll(string.IsNullOrWhiteSpace);
        //    return strings;
        //}

        //private List<string> RemoveTimeSpan(List<string> strings)
        //{
        //    string timaSpanPattern = @"(\d)+:(\d)+";
        //    Regex timeSpanpRegex = new Regex(timaSpanPattern, RegexOptions.IgnoreCase);
        //    strings.RemoveAll(s => timeSpanpRegex.IsMatch(s));
        //    return strings;
        //}

        #endregion
    }
}
