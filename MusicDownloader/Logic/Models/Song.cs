using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Song
    {
        //public string SongTitle { get; set; }
        //public string ArtistName { get; set; }

        public string FullName { get; set; }
        public byte[] EntryBytes { get; set; }

        public string FileExtension { get; set; }
    }
}
