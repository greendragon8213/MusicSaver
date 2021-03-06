﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;
using Logic.Abstract;
using Logic.Exceptions;
using Logic.Models;
using Logic.Resources;

namespace Logic.Implementation
{
    public class MusicArchiveService : IMusicArchiveService
    {
        private readonly ISongDownloader _songDownloader;

        public MusicArchiveService(ISongDownloader songDownloader)
        {
            _songDownloader = songDownloader;
        }

        public async Task<string> CreateMusicArchive(List<string> songsList, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileName = Guid.NewGuid() + ".zip";
            Stream outputFileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write);
            var zipStream = new ZipOutputStream(outputFileStream);
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
                            Size = song.EntryBytes.Length,
                            IsUnicodeText = true
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
                        logStringBuilder.AppendLine(LogText.ThisIsListOfSongsWeCouldntDownload);
                    }
                    failedSongsCount++;
                    logStringBuilder.AppendLine(songName);
                }
            }

            AddLogFileToArchive(zipStream, logStringBuilder, songsList.Count, songsList.Count - failedSongsCount);

            zipStream.Flush();
            zipStream.Close();

            outputFileStream.Close();
            outputFileStream.Dispose();
            outputFileStream = null;
            zipStream = null;

            return fileName;
        }

        public Stream GetStream(string path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        #region Private methods

        private void AddLogFileToArchive(ZipOutputStream zipStream, StringBuilder logStringBuilder,
            int allSongsCount, int successDownloadedSongsCount)
        {
            if (logStringBuilder.Length == 0)
            {
                logStringBuilder.AppendLine(LogText.AllSongsDownloadedSuccessfully);
            }

            logStringBuilder.Insert(0, $"{LogText.SongsDownloaded} {successDownloadedSongsCount}/{allSongsCount}{Environment.NewLine}");
            
            //Adding download log
            byte[] readmeFileBytes = Encoding.Unicode.GetBytes(logStringBuilder.ToString());
            zipStream.PutNextEntry(new ZipEntry(Path.GetFileName("log.txt"))
            {
                Size = readmeFileBytes.Length
            });
            zipStream.Write(readmeFileBytes, 0, readmeFileBytes.Length);
        }
        
        #endregion
    }
}
