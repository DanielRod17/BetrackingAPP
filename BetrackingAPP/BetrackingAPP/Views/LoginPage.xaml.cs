using BetrackingAPP.Models;
using BottomBar.XamarinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BetrackingAPP.ViewModel;


namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Init();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new LoginPageViewModel(Navigation);
            //Scroll();
        }


        void Init()
        {
            //BackgroundColor = Constants.BackgroundColor;
            //ActivitySpinner.IsVisible = false;
            //LoginIcon.HeightRequest = Constants.LoginIconHeight;
        }

    }
}