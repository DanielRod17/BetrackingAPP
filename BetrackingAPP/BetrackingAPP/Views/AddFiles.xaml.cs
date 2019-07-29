using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFiles : ContentPage
    {
        public AddFiles(Reports reporte, User usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new AddFilesViewModel(reporte, usuario);
        }

        private void PickFile_Clicked(object sender, EventArgs e)
        {

        }
        private void PickPhoto_Clicked(object sender, EventArgs e)
        {

        }
        private void TakePhoto_Clicked(object sender, EventArgs e)
        {

        }
        private void UploadFile_Clicked(object sender, EventArgs e)
        {

        }
    }
}