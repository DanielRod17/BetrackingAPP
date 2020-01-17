using BetrackingAPP.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using BetrackingAPP.Views;

namespace BetrackingAPP.ViewModel
{
    public class HelpViewModel : BaseViewModel
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
            IsLoading = true;
            if (HelpText != null && HelpText != "")
            {
                string fiel_name = "";
                var content = new MultipartFormDataContent();
                if (MediaFile != null)
                {
                    var splitted = MediaFile.Path.Split('/');
                    fiel_name = splitted[splitted.Length - 1];

                    content.Add(new StreamContent(MediaFile.GetStream()),
                           $"\"{fiel_name}\"",
                           $"\"{MediaFile.Path}\"");
                }
                content.Add(new StringContent(Usuario.Id.ToString()), "User");
                content.Add(new StringContent(Usuario.Firstname), "Firstname");
                content.Add(new StringContent(Usuario.Lastname), "Lastname");
                content.Add(new StringContent(Usuario.Email), "Email");
                content.Add(new StringContent(HelpText), "Motivo");
                content.Add(new StringContent(fiel_name), "Filename");

                var httpClient = new HttpClient();
                var uploadServiceBaseAddress = "https://bepc.backnetwork.net/BEPCINC/api/RequestHelp.php";
                var result = await httpClient.PostAsync(uploadServiceBaseAddress, content);
                if (result.IsSuccessStatusCode)
                {
                    var responseData = await result.Content.ReadAsStringAsync();
                    if (responseData == "Email sent to the support department")
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, responseData));
                        HelpText = "";
                        imgSrc = "";
                    }
                    else
                    {
                        //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage(responseData));
                    }
                }
                else
                {
                    //await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
                }
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Oops", "Enter a description of your issue", "OK");
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Enter a description of your issue"));
            }
            IsLoading = false;
        }
        internal void AgregarFile(MediaFile mediaFile, ImageSource elemento)
        {
            MediaFile = mediaFile;
            imgSrc = mediaFile.Path;
        }
    }
}
