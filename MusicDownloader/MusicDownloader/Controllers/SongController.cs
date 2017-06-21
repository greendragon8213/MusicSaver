using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using log4net;
using Logic.Abstract;
using Logic.Exceptions;
using Logic.Implementation;
using Logic.Models;

namespace MusicDownloader.Controllers
{
    public class SongController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISongUrlProvider _songUrlProvider;
        private readonly ISongDownloader _songDownloader;
        public SongController()//ToDo DI
        {
            _songUrlProvider = new ZfFmSongUrlProvider();
            _songDownloader = new SongDownloader(_songUrlProvider);
        }
        
        [System.Web.Mvc.HttpPost]
        public async Task DownloadSongs([FromBody]string[] songsList)
        {
            Stream songsStream = null;
            try
            {
                songsStream = await GetZipStreamAsync(songsList);
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message, exception);
                if (songsStream != null)
                {
                    songsStream.Flush();
                    songsStream.Close();
                    songsStream.Dispose();
                }
                return;
            }

            DownloadStream(songsStream);
        }
        
        private async Task<Stream> GetZipStreamAsync(string[] songsList)
        {
            Stream outputFileStream = new FileStream("D:/testzip.zip", FileMode.Create, FileAccess.Write);
            using (var zipStream = new ZipOutputStream(outputFileStream))
            {
                StringBuilder logStringBuilder = new StringBuilder();
                int failedSongsCount = 0;

                foreach (string songName in songsList)
                {
                    if (string.IsNullOrEmpty(songName))
                    {
                        continue;
                    }

                    try
                    {
                        using (Song song = await _songDownloader.GetSongAsync(songName))
                        {
                            var fileEntry = new ZipEntry(Path.GetFileName(song.FullName + song.FileExtension))
                            {
                                Size = song.EntryBytes.Length
                            };

                            zipStream.PutNextEntry(fileEntry);
                            zipStream.Write(song.EntryBytes, 0, song.EntryBytes.Length);

                            zipStream.CloseEntry();
                        }
                    }
                    catch (SongNotFoundException)
                    {
                        if (logStringBuilder.Length == 0)
                        {
                            logStringBuilder.AppendLine("This is the list of songs we couldn't download:");
                        }
                        failedSongsCount++;
                        logStringBuilder.AppendLine(songName);
                    }
                }

                AddLogFileToArchive(zipStream, logStringBuilder, songsList.Length, songsList.Length - failedSongsCount);
           
                zipStream.Flush();
                zipStream.Close();

                outputFileStream.Close();
                outputFileStream.Dispose();
            }

            return new FileStream("D:/testzip.zip", FileMode.Open, FileAccess.Read);
        }

        private void AddLogFileToArchive(ZipOutputStream zipStream, StringBuilder logStringBuilder, 
            int allSongsCount, int successDownloadedSongsCount)
        {
            if (logStringBuilder.Length == 0)
            {
                logStringBuilder.AppendLine("All songs have been downloaded successfylly.");
            }

            logStringBuilder.AppendLine($"Songs downloaded: {successDownloadedSongsCount}/{allSongsCount}");

            //Adding download log
            byte[] readmeFileBytes = Encoding.Unicode.GetBytes(logStringBuilder.ToString());
            zipStream.PutNextEntry(new ZipEntry(Path.GetFileName("log.txt"))
            {
                Size = readmeFileBytes.Length
            });
            zipStream.Write(readmeFileBytes, 0, readmeFileBytes.Length);
        }

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

                // Read the bytes of file
                while (fileLengthToRead > 0)
                {
                    // Verify that the client is connected or not
                    if (Response.IsClientConnected)
                    {
                        // 32KB
                        int bufferSize = 32 * 1024;

                        // Create buffer for reading [intBufferSize] bytes from file
                        byte[] buffer = new byte[bufferSize];

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
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Flush();
                    inputStream.Close();
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
    }
}
