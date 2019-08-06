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
        public LoginPage(User usuario)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //Init();
        }

        protected override void OnAppearing()
        {
            App.Logedin = false;
            this.BindingContext = new LoginPageViewModel(Navigation);
            base.OnAppearing();
        }
    }
}