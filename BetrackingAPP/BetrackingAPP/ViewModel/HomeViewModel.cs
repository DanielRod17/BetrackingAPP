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
        private string _greeting { get; set; }
        public string Greeting {
            get
            {
                return _greeting;
            }
            set
            {
                _greeting = value;
                OnPropertyChanged();
            }
        }
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
        private ObservableCollection<Notification> _notifications;
        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return _notifications;
            }
            set
            {
                _notifications = value;
                OnPropertyChanged();
            }
        }
        public bool HasPropertyValueChanged { get; private set; }
        public HomeViewModel(User usuarioFrom)
        {
            Notifications = null;
            var tiempo = "";
            if (DateTime.Now.Hour <= 12 && DateTime.Now.Hour >= 5)
            {
                tiempo = "Good Morning";
}
            else if (DateTime.Now.Hour <= 18)
            {
                tiempo = "Good Afternoon";
            }
            else
            {
                tiempo = "Good Evening";
            }
            usuario = usuarioFrom;
            Greeting = tiempo + "\n" + usuarioFrom.Firstname + " " + usuarioFrom.Lastname + "!";
            _ = LoadNotifications(usuarioFrom);
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
                Notifications = JsonConvert.DeserializeObject<ObservableCollection<Notification>>(responseData);
                if (Notifications == null)
                {
                    Notifications = new ObservableCollection<Notification> {
                        new Notification() {
                            Title = "Welcome Back, " + usuario.Firstname + "!",
                            Message = "There are no current notifications" ,
                            From_Date = "",
                            Color = "#f4c53f",
                            cName = "System"
                        }
                    };
                }
                else
                {
                    Notifications = new ObservableCollection<Notification>();
                }
            }
            IsLoading = false;
        }
    }
}
