﻿using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using Plugin.Permissions;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System.IO;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFiles : ContentPage
    {
        private MediaFile file { get; set; }
        private MediaFile _mediaFile
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
                OnPropertyChanged();
            }
        }
        public AddFiles(Reports reporte, User usuario)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                case Device.UWP:
                default:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }
            InitializeComponent();
            BindingContext = new AddFilesViewModel(reporte, usuario);
        }
        private async void PickFile_Clicked(object sender, EventArgs e)
        {
            var filetypes = new string[] { "application/pdf" };
            var file = await CrossFilePicker.Current.PickFile(filetypes);
            if (file == null)
            {
                return;
            }
            if (file != null)
            {
                try
                {
                    var elemento = file.GetStream();
                    AgregarAListaArchivo(file, elemento);
                }
                catch
                {
                    await DisplayAlert("Oops :(", "Couldn't open the file. Try moving it to other folder", "Ok");
                }
            }
        }
        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "Ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
                return;

            /*FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });*/
            var elemento = ImageSource.FromStream(() => {
                return _mediaFile.GetStream();
            });
            AgregarALista(_mediaFile, elemento);

        }
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || ! CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "Ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                SaveToAlbum = true,
                PhotoSize = PhotoSize.Medium,
                AllowCropping = true,
                Name = "BTExpensesImage.jpg"
            });

            if (_mediaFile == null)
                return;
            var elemento = ImageSource.FromStream(() => {
                return _mediaFile.GetStream();
            });
            AgregarALista(_mediaFile, elemento);
            /*FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });*/
        }
        private async void UploadFile_Clicked(object sender, EventArgs e)
        {
            /*var content = new MultipartFormDataContent();
            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = "https://";
            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);*/
            var vm = BindingContext as AddFilesViewModel;
            await vm.SubirArchivosAsync();
        }

        public void AgregarALista(MediaFile _mediaFile, ImageSource elemento)
        {
            var vm = BindingContext as AddFilesViewModel;
            vm.AgregarALista(_mediaFile, elemento);
        }

        public void AgregarAListaArchivo(FileData file, System.IO.Stream elemento)
        {
            var vm = BindingContext as AddFilesViewModel;
            vm.AgregarAListaArchivo(file, elemento);
        }
        protected override void OnDisappearing()
        {
            var vm = BindingContext as AddFilesViewModel;
            vm.EnviarUpdate();
        }
    }
}