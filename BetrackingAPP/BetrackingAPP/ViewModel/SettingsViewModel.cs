using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BetrackingAPP.Models;
using BetrackingAPP.Views;
using Xamarin.Forms;
using PCLCrypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using Plugin.DownloadManager;
using System.Net;
using Plugin.DownloadManager.Abstractions;

namespace BetrackingAPP.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public User usuario { get; set; }
        public Command GoToHelp { get; set; }
        public SettingsViewModel(User Usuario)
        {
            usuario = Usuario;
            GoToHelp = new Command(AyudaGo);
        }

        public async void AyudaGo()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Help(usuario));
        }
    }
}
