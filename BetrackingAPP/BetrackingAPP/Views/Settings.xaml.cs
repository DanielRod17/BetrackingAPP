using BetrackingAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            App.Logedin = false;
            Application.Current.Properties["ID"] = null;

            var totales = Navigation.NavigationStack.Count;

            await Navigation.PushAsync(new LoginPage(Usuario));
            for (int i = 0; i < totales; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);
            }
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
            //Device.OpenUri(new Uri("https://bepc.backnetwork.net/BEPCINC/BeTracking/index"));
            Launcher.OpenAsync("https://bepc.backnetwork.net/BEPCINC/BeTracking/");
        }
        private void BETracking_Help(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("https://www.bepcinc.com/betrackinghelp"));
            Launcher.OpenAsync("https://www.bepcinc.com/betrackinghelp");
        }
        private void BETracking_Policy(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("https://www.bepcinc.com/privacy-and-terms"));
            Launcher.OpenAsync("https://www.bepcinc.com/privacy-and-terms");
        }
        private void BETracking_Terms(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("https://www.bepcinc.com/privacy-and-terms"));
            Launcher.OpenAsync("https://www.bepcinc.com/privacy-and-terms");
        }
    }
}