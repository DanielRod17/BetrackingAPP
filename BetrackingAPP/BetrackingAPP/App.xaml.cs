using BetrackingAPP.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace BetrackingAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new  NavigationPage(new IndividualCard());
        }

        protected override void OnStart()
        {
            AppCenter.Start("3dc6f4a4-ed59-486a-ac0a-3f6d880a8b16", typeof(Push));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
