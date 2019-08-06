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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Help(Usuario));
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
    }
}