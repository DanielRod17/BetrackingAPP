using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BetrackingAPP.Models;
using BetrackingAPP.Views;
using Xamarin.Forms;
using PCLCrypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using System.Net;
using Plugin.Media.Abstractions;
using Plugin.FilePicker.Abstractions;

namespace BetrackingAPP.ViewModel
{
    public class AddFilesViewModel : BaseViewModel
    {
        public User Usuario { get; set; }
        public Reports Reporte { get; set; }
        private bool _HasUploaded { get; set; }
        public bool HasUploaded
        {
            get
            {
                return _HasUploaded;
            }
            set
            {
                _HasUploaded = value;
                OnPropertyChanged();
            }
        }
        public Command<FilesAdd> DeleteMedia { get; }
        public Command<ArchivosAdd> DeleteFile { get; }
        private ObservableCollection<FilesAdd> _medias { get; set; }
        public ObservableCollection<FilesAdd> Medias
        {
            get
            {
                return _medias;
            }
            set
            {
                _medias = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ArchivosAdd> _archivos { get; set; }
        public ObservableCollection<ArchivosAdd> Archivos
        {
            get
            {
                return _archivos;
            }
            set
            {
                _archivos = value;
                OnPropertyChanged();
            }
        }
        public AddFilesViewModel(Reports reportFrom, User usuarioFrom)
        {
            HasUploaded = false;
            Usuario = usuarioFrom;
            Reporte = reportFrom;
            DeleteMedia = new Command<FilesAdd>(BorrarMedia);
            DeleteFile = new Command<ArchivosAdd>(BorrarFiles);
            Medias = new ObservableCollection<FilesAdd>();
            Archivos = new ObservableCollection<ArchivosAdd>();
            //Medias.Add(new FilesAdd() { Path = "fileicon.png" });
        }
        public void AgregarALista(MediaFile mediaFile, ImageSource fuente)
        {
            var splitted = mediaFile.Path.Split('/');
            string fiel_name = splitted[splitted.Length - 1];
            Medias.Add(new FilesAdd() { Archivo = mediaFile, Fuente = fuente, Path = mediaFile.Path, FileName = fiel_name });

            /*if (HasUploaded == false)
            {
                Medias.RemoveAt(0);
                HasUploaded = true;
            }*/
        }

        internal void AgregarAListaArchivo(FileData file)
        {
            var splitted = file.FileName.Split('.');
            var ext = splitted[splitted.Length - 1];


            splitted = file.FileName.Split('/');
            string fiel_name = splitted[splitted.Length - 1];


            string path = "";

            if (ext.ToUpper() == "JPG" || ext.ToUpper() == "PNG" || ext.ToUpper() == "BMP" || ext.ToUpper() == "GIF")
            {
                path = file.FilePath;
            }
            else if (ext.ToUpper() == "PDF")
            {
                path = "pdficon.png";
            }
            else
            {
                path = "fileicon.png";
            }

            Archivos.Add(new ArchivosAdd() { Archivo = file, Path = path, FileName = fiel_name });
        }
        public void BorrarMedia(FilesAdd item)
        {
            Medias.Remove(item);
        }
        public void BorrarFiles(ArchivosAdd item)
        {
            Archivos.Remove(item);
        }

        internal async Task SubirArchivosAsync()
        {
            var content = new MultipartFormDataContent();

            foreach (FilesAdd media in Medias)
            {
                content.Add(new StreamContent(media.Archivo.GetStream()),
                    $"\"{media.FileName}\"",
                    $"\"{media.Archivo.Path}\"");
            }
            foreach (ArchivosAdd archivo in Archivos)
            {
                content.Add(new StreamContent(archivo.Archivo.GetStream()),
                    $"\"{archivo.FileName}\"",
                    $"\"{archivo.Archivo.FilePath}\"");
            }
            content.Add(new StringContent(Usuario.Id.ToString()), "User");
            content.Add(new StringContent(Reporte.ID.ToString()), "TravelID");

            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "https://bepc.backnetwork.net/BEPCINC/api/SaveFiles.php";
            var result = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Saved!")
                {
                    
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
        }
    }
}
