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
        private static bool _logedin { get; set; }
        public static bool Logedin { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage(null));
            //MainPage = new  NavigationPage(new IndividualCard());
        }

        protected override void OnStart()
        {
            if ( Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS )
            {
                //Application.Current.MainPage.DisplayAlert("Oops", "iOS", "OK");
                AppCenter.Start("5591c0eb-8f92-4ce0-b444-ca5943cea3d9", typeof(Push));
            }
            else if ( Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android )
            {
                //Application.Current.MainPage.DisplayAlert("Oops", "Android", "OK");
                AppCenter.Start("3dc6f4a4-ed59-486a-ac0a-3f6d880a8b16", typeof(Push));
            }
            //AppCenter.Start("3dc6f4a4-ed59-486a-ac0a-3f6d880a8b16", typeof(Push));
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
