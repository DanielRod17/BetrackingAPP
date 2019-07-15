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

namespace BetrackingAPP.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public User usuario;
        public string Greeting { get; set; }
        public HomeViewModel ViewModel { get; set; }
        private List<Models.Notification> _notifications;
        public List<Models.Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                OnPropertyChanged();
            }
        }

        public bool HasPropertyValueChanged { get; private set; }

        public HomeViewModel(User usuarioFrom)
        {
            usuario = usuarioFrom;
            Greeting = "Welcome back, " + usuarioFrom.Firstname + " " + usuarioFrom.Lastname;
        }

        public async void LoadNotifications(User usuario)
        {
            HasPropertyValueChanged = true;
            var client = new HttpClient();
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getNotifications.php?usuario=" + usuario.Id;
            var result = await client.GetAsync(URL);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Notifications = JsonConvert.DeserializeObject<List<Models.Notification>>(responseData);
            }
            HasPropertyValueChanged = false;
        }
    }
}
