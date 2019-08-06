using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BetrackingAPP.Models
{
    public class FilesAdd
    {
        public MediaFile Archivo { get; set; }
        public ImageSource Fuente { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }

    public class ArchivosAdd
    {
        public FileData Archivo { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Contents { get; set; }
        public Stream Contenido { get; set; }
    }
}
