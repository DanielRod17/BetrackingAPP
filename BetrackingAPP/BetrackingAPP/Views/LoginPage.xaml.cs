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
            this.BindingContext = new LoginPageViewModel(Navigation);
            base.OnAppearing();
        }

        private void Entry_Username_Completed(object sender, EventArgs e)
        {
            var password = this.FindByName<Entry>("Entry_Password");
            password.Focus();
        }

        private void Entry_Password_Completed(object sender, EventArgs e)
        {
            var boton = this.FindByName<Button>("Btn_Signin");
            boton.Command.Execute(null);
        }

        private new void Focused(object sender, FocusEventArgs e)
        {
            var user_input = this.FindByName<Grid>("Grid_Container");
            //user_input.Margin.Top = 10;
            user_input.TranslationY = -50;
        }
        private void FocusedPass(object sender, FocusEventArgs e)
        {
            var user_input = this.FindByName<Grid>("Grid_Container");
            //user_input.Margin.Top = 10;
            user_input.TranslationY = -120;
        }
        private new void Unfocused(object sender, FocusEventArgs e)
        {
            var user_input = this.FindByName<Grid>("Grid_Container");
            //user_input.Margin.Top = 10;
            user_input.TranslationY = 0;
        }
    }
}