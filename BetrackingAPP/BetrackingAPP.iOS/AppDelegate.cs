using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xfx;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.DownloadManager;

namespace BetrackingAPP.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public object ImagecircleRender { get; private set; }
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
        {
            CrossDownloadManager.BackgroundSessionCompletionHandler = completionHandler;
        }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("5591c0eb-8f92-4ce0-b444-ca5943cea3d9", typeof(Analytics), typeof(Crashes));
            XfxControls.Init();
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            ImagecircleRender.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
