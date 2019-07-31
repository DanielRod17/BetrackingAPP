using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Help : ContentPage
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
        public Help(User usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new HelpViewModel(usuario);
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

            var elemento = ImageSource.FromStream(() => {
                return _mediaFile.GetStream();
            });
            AgregarFile(_mediaFile, elemento);
        }

        public void AgregarFile(MediaFile _mediaFile, ImageSource elemento)
        {
            var vm = BindingContext as HelpViewModel;
            vm.AgregarFile(_mediaFile, elemento);
        }
    }
}