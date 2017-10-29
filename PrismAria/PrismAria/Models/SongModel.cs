using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class SongModel
    {
        public string SongName { get; set; }
        public string SongDesc { get; set; }
        public FileData SongFile { get; set; }
        public string GenreId { get; set; }
    }
}
