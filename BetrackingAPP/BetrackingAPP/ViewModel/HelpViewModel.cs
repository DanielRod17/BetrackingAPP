using BetrackingAPP.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BetrackingAPP.ViewModel
{
    public class HelpViewModel : BaseViewModel
    {
        public User Usuario { get; set; }
        private MediaFile _media { get; set; }
        public MediaFile MediaFile
        {
            get
            {
                return _media;
            }
            set
            {
                _media = value;
                OnPropertyChanged();
            }
        }
        private string _imgSrc { get; set; }
        public string imgSrc
        {
            get
            {
                return _imgSrc;
            }
            set
            {
                _imgSrc = value;
                OnPropertyChanged();
            }
        }
        private string _HelpText { get; set; }
        public string HelpText
        {
            get
            {
                return _HelpText;
            }
            set
            {
                _HelpText = value;
                OnPropertyChanged();
            }
        }
        public Command PedirAyuda { get; set; }
        public HelpViewModel(User usuario)
        {
            Usuario = usuario;
            PedirAyuda = new Command(AskHelp);
        }

        private async void AskHelp()
        {
            var content = new MultipartFormDataContent();
            if (MediaFile != null) {
                var splitted = MediaFile.Path.Split('/');
                string fiel_name = splitted[splitted.Length - 1];

                content.Add(new StreamContent(MediaFile.GetStream()),
                       $"\"{fiel_name}\"",
                       $"\"{MediaFile.Path}\"");
            }
            content.Add(new StringContent(Usuario.Id.ToString()), "User");
            content.Add(new StringContent(Usuario.Firstname), "Firstname");
            content.Add(new StringContent(Usuario.Lastname), "Lastname");
            content.Add(new StringContent(Usuario.Email), "Email");
            content.Add(new StringContent(HelpText), "Motivo");

            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "https://bepc.backnetwork.net/BEPCINC/api/RequestHelp.php";
            var result = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }

        }

        internal void AgregarFile(MediaFile mediaFile, ImageSource elemento)
        {
            MediaFile = mediaFile;
            imgSrc = mediaFile.Path;
        }
    }
}
