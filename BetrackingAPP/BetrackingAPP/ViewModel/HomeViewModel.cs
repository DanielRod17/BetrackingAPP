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
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadNotifications(usuario);

                    IsRefreshing = false;
                });
            }
        }
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
            Greeting = "Welcome,\n" + usuarioFrom.Firstname + " " + usuarioFrom.Lastname + "!";
        }
        public async Task LoadNotifications(User usuario)
        {
            IsLoading = true;
            var client = new HttpClient();
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getNotifications.php?usuario=" + usuario.Id;
            var result = await client.GetAsync(URL);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Notifications = JsonConvert.DeserializeObject<List<Models.Notification>>(responseData);
            }
            IsLoading = false;
        }
    }
}
