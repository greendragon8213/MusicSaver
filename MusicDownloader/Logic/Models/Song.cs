using System;

namespace Logic.Models
{
    public class Song : IDisposable
    {
        //public string SongTitle { get; set; }
        //public string ArtistName { get; set; }
        private bool _isDisposed = false;

        public string FullName { get; set; }
        public byte[] EntryBytes { get; set; }

        public string FileExtension { get; set; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                //ToDo
                EntryBytes = null;
                _isDisposed = true;
            }
        }
    }
}
