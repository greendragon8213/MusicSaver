using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using Logic.Abstract;
using Logic.Implementation;

namespace MusicDownloader.Controllers
{
    public class SongController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISongUrlProvider _songUrlProvider;
        private readonly ISongDownloader _songDownloader;
        private readonly IMusicArchiveService _musicArchiveService;
        public SongController()//ToDo DI
        {
            _songUrlProvider = new ZfFmSongUrlProvider();
            _songDownloader = new SongDownloader(_songUrlProvider);
            _musicArchiveService = new MusicArchiveService(_songDownloader);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> FormMusicArchive([FromBody]List<string> songsList)
        {
            try
            {
                string temporaryFilesPath = Path.Combine(Server.MapPath("~"), 
                    (ConfigurationManager.AppSettings["TemporaryFilesFolderName"]));
                string savedFileName = await _musicArchiveService.CreateMusicArchive(songsList, temporaryFilesPath);
                return Json(new { fileName = savedFileName });
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, exception);
                Response.StatusCode = 500;
                return Json(new { errorMessage = exception.Message});
            }
        }

        public void DownloadMusicArchive(string fileName)
        {
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
            catch (Exception exception)
            {
                _logger.Error(exception.Message, exception);
                //ToDo return error response
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
