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
using System.Linq;

namespace BetrackingAPP.ViewModel
{
    public class TimecardsViewModel : BaseViewModel
    {
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

                    await LoadTimecards(usuario, FechaSeleccionada);

                    IsRefreshing = false;
                });
            }
        }
        public Command NewTimecardCommand { get; set; }
        private bool _happened;
        public bool HasPropertyValueChanged
        {
            get => _happened;
            set
            {
                _happened = value;
                OnPropertyChanged();
            }
        }
        public string Firstname { get; set; }
        public User usuario;
        private bool inWork;
        private List<Models.Timecard> _timecards;
        public ObservableCollection<Search> Searches { get; set; }
        public List<Models.Timecard> Timecards
        {
            get => _timecards;
            set
            {
                _timecards = value;
                OnPropertyChanged();
            }
        }
        private DateTime _dateSearch = DateTime.Today;
        private bool IsLoading
        {
            get => inWork;
            set
            {
                inWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime FechaSeleccionada
        {
            set
            {
                if (value.ToString() != "1/1/0001 12:00:00 AM")
                {
                    if ((int)value.DayOfWeek != 0)
                    {
                        if ((int)value.DayOfWeek == 1 || (int)value.DayOfWeek == 2)
                        {
                            var fecha = value.AddDays(-((int)value.DayOfWeek));
                            _dateSearch = fecha;
                        }
                        else
                        {
                            var fecha = value.AddDays(7 - (int)value.DayOfWeek);
                            _dateSearch = fecha;
                        }
                    }
                    else
                    {
                        _dateSearch = value;
                    }
                }
                else
                {
                    _dateSearch = value;
                }
                OnPropertyChanged();
            }
            get {
                if (_dateSearch.ToString() != "1/1/0001 12:00:00 AM")
                {
                    if ((int)_dateSearch.DayOfWeek != 0)
                    {
                        if ((int)_dateSearch.DayOfWeek == 1 || (int)_dateSearch.DayOfWeek == 2)
                        {
                            var fecha = _dateSearch.AddDays(-((int)_dateSearch.DayOfWeek));
                            _dateSearch = fecha;
                        }
                        else
                        {
                            var fecha = _dateSearch.AddDays(7 - (int)_dateSearch.DayOfWeek);
                            _dateSearch = fecha;
                        }
                    }
                    else
                    {
                        var fecha = _dateSearch;
                        _dateSearch = fecha;
                    }
                }
                _ = LoadTimecards(usuario, _dateSearch);
                IsLoading = false;
                return _dateSearch;
            }
        }
        private Timecard _selectedItem;
        public Timecard ShowTimecardDetails
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    GoToTimecard(_selectedItem, usuario);
                }
            }
        }
        public async void GoToTimecard(Timecard eu_timecard, User usuario)
        {
            HasPropertyValueChanged = true;
            var bandera = 0;
            for (var i = 0; i < usuario.Assignments.Length; i ++)
            {
                if (usuario.Assignments[i].Division == 5 || usuario.Assignments[i].Division == 2)
                {
                    bandera = 1;
                }
            }
            if (usuario.Payroll == "142")
            {
                bandera = 1;
            }
            if (eu_timecard != null && bandera == 1)
            {
                await App.Current.MainPage.Navigation.PushAsync(new IndividualCard(eu_timecard, usuario, _dateSearch));
            }else if (eu_timecard != null && bandera == 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new IndividualTimeInOut(eu_timecard, usuario, _dateSearch));
            }
            HasPropertyValueChanged = false;
        }
        public TimecardsViewModel(User usuarioFrom)
        {
            usuario = usuarioFrom;
            NewTimecardCommand = new Command(async () => await NavigateToNewTimecard());
        }
        public async Task NavigateToNewTimecard()
        {
            HasPropertyValueChanged = true;
            var bandera = 0;
            for (var i = 0; i < usuario.Assignments.Length; i++)
            {
                if (usuario.Assignments[i].Division == 5 || usuario.Assignments[i].Division == 2 || usuario.Assignments[i].Division == 3)
                {
                    bandera = 1;
                }
            }
            int[] arreglo_ids = { 702, 656, 738, 439, 805, 657, 713, 633, 537, 501, 689 };
            if (bandera == 1 || arreglo_ids.Contains(usuario.Id))
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NewTimecard(Timecards, usuario, FechaSeleccionada));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NewTimeOut(Timecards, usuario, FechaSeleccionada));
            }
            HasPropertyValueChanged = false;
        }
        public Command SearchCommand { get; set; }
        public async Task LoadTimecards(User usuario, DateTime dateSearch)
        {
            HasPropertyValueChanged = true;
            var client = new HttpClient();
            var date = dateSearch.Year + "-" + dateSearch.Month + "-" + dateSearch.Day;
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getTimecards.php?usuario="+usuario.Id+ "&date="+ date;
            var result = await client.GetAsync(URL);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Timecards = JsonConvert.DeserializeObject<List<Models.Timecard>>(responseData);
            }
            HasPropertyValueChanged = false;
        }
    }
}
