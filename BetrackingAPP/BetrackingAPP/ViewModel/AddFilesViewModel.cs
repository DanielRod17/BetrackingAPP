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
using System.IO;
using System.Collections;
using System.Net.Http.Headers;

namespace BetrackingAPP.ViewModel
{
    public class AddFilesViewModel : BaseViewModel
    {
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public void EnviarUpdate()
        {
            IsLoading = true;
            MessagingCenter.Send<IndividualReportViewModel>(new IndividualReportViewModel(Usuario, Reporte), "Change");
            IsLoading = false;
        }
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
            IsLoading = true;
            HasUploaded = false;
            Usuario = usuarioFrom;
            Reporte = reportFrom;
            DeleteMedia = new Command<FilesAdd>(BorrarMedia);
            DeleteFile = new Command<ArchivosAdd>(BorrarFiles);
            Medias = new ObservableCollection<FilesAdd>();
            Archivos = new ObservableCollection<ArchivosAdd>();
            IsLoading = false;
        }
        public void AgregarALista(MediaFile mediaFile, ImageSource fuente)
        {
            IsLoading = true;
            var splitted = mediaFile.Path.Split('/');
            string fiel_name = splitted[splitted.Length - 1];
            Medias.Add(new FilesAdd() { Archivo = mediaFile, Fuente = fuente, Path = mediaFile.Path, FileName = fiel_name });
            IsLoading = false;
        }
        internal void AgregarAListaArchivo(FileData file, Stream elemento)
        {
            IsLoading = true;
            var splitted = file.FileName.Split('.');
            var ext = splitted[splitted.Length - 1];
            string contents = System.Text.Encoding.ASCII.GetString(file.DataArray);

            splitted = file.FileName.Split('/');
            string fiel_name = file.FileName;


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

            Archivos.Add(new ArchivosAdd() { Archivo = file, Path = path, FileName = fiel_name, Contents = contents, Contenido = elemento });
            IsLoading = false;
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
            IsLoading = true;
            var content = new MultipartFormDataContent();

            var arrayList = new ArrayList();
            foreach (FilesAdd media in Medias)
            {
                content.Add(new StreamContent(media.Archivo.GetStream()),
                    $"\"{media.FileName}\"",
                    $"\"{media.Archivo.Path}\"");
                arrayList.Add(media.FileName);
            }
            foreach (ArchivosAdd archivo in Archivos)
            {

                byte[] byteArray = Encoding.ASCII.GetBytes(archivo.Contents);
                //byteArray = System.IO.File.ReadAllBytes(archivo.Archivo.FilePath);
                MemoryStream stream = new MemoryStream(byteArray);
                content.Add(new StreamContent(archivo.Contenido),
                //content.Add(new StreamContent(File.Open(archivo.Archivo.FilePath, FileMode.Open)),
                    $"\"{archivo.Archivo.FileName}\"",
                    $"\"{archivo.Archivo.FilePath}\"");
                arrayList.Add(archivo.FileName);
            }
            // content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data; boundary=------WebKitFormBoundary7MA4YWxkTrZu0gW\r\n");
            //content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            //content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            var json = JsonConvert.SerializeObject(arrayList);
            content.Add(new StringContent(Usuario.Id.ToString()), "User");
            content.Add(new StringContent(Reporte.ID.ToString()), "TravelID");
            content.Add(new StringContent(json), "NombresArchivo");

            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders
            //.Accept
            //.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data", "boundary=------WebKitFormBoundary7MA4YWxkTrZu0gW\r\n"));//ACCEPT header
            var uploadServiceBaseAddress = "https://bepc.backnetwork.net/BEPCINC/api/SaveFiles.php";
            var result = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Files uploaded!")
                {
                    Archivos = new ObservableCollection<ArchivosAdd>();
                    Medias = new ObservableCollection<FilesAdd>();
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, responseData));
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
            IsLoading = false;
        }
    }
}
