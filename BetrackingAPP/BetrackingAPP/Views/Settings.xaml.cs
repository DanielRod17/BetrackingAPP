using BetrackingAPP.Models;
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
    public partial class Settings : ContentPage
    {
        public User Usuario { get; set; }
        public Settings()
        {
            InitializeComponent();
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            App.Logedin = false;
            await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage(Usuario));
        }

        protected override void OnAppearing()
        {
            if (App.Logedin == false)
            {
                Navigation.PushAsync(new LoginPage(Usuario));
            }
            base.OnAppearing();
        }

        private void BETracking_Web(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://bepc.backnetwork.net/BEPCINC/BeTracking/index"));
        }
        private void BETracking_Help(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.bepcinc.com/betrackinghelp"));
        }
        private void BETracking_Policy(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.bepcinc.com/privacy-and-terms"));
        }
        private void BETracking_Terms(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.bepcinc.com/privacy-and-terms"));
        }
    }
}