using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.Threading;

namespace BetrackingAPP.Droid
{
    [Activity(Label = "BE Tracking", Theme = "@style/MainTheme.Base        ", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : Activity, ISurfaceHolderCallback
    {
        MediaPlayer player;
        VideoView videoView;

        static readonly string TAG = "X:" + typeof(SplashScreen).Name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashLayout);

            //access video view
            videoView = FindViewById<VideoView>(Resource.Id.splashVideo);
            //string videoPath = $"android.resource://{Application.PackageName}/TransData/AppIntro.mp4";
            //videoView.SetVideoPath(videoPath);
            play("TransData/AppIntro.mp4");
            //StartActivity(typeof(MainActivity));
            //access the video
            //string videoPath = $"android.resource://{Application.PackageName}/{Resource.Raw.video}";
            //string videoPath = $"android.resource://{Application.PackageName}/{Resource.Raw.AppIntro}";
            // Create your application here
        }
        void play(string fullPath)
        {
            ISurfaceHolder holder = videoView.Holder;
            holder.SetType(SurfaceType.PushBuffers);
            // Necesito saber cuando la superficie esta creada para poder asignar el Display al MediaPlayer
            holder.AddCallback(this);
            player = new MediaPlayer();
            Android.Content.Res.AssetFileDescriptor afd = this.Assets.OpenFd(fullPath);
            if (afd != null)
            {
                player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                player.Prepare();
                player.Start();
                player.Completion += Player_Completion;
                //StartActivity(typeof(MainActivity));
            }
        }

        private void Player_Completion(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceCreated");
            player.SetDisplay(holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceDestroyed");
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
        {
            Console.WriteLine("SurfaceChanged");
        }
    }
}