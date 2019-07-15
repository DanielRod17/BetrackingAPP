using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Xfx;

namespace BetrackingAPP.Droid
{
    [Activity(Label = "BetrackingAPP", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public partial/*GORILLA*/class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Bundle bundle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            XfxControls.Init();
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // lock to portrait for phones
            if (Device.Idiom == TargetIdiom.Phone)
                this.RequestedOrientation = ScreenOrientation.SensorPortrait;

            LoadApplication(new App());

           
        }
        /*public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }*/
    }
}