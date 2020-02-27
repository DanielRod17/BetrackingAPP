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

namespace BetrackingAPP.ViewModel
{
    public class ExpensesViewModel : BaseViewModel
    {
        private string _expensesName { get; set; }
        public string ExpensesName
        {
            get
            {
                return _expensesName;
            }
            set
            {
                _expensesName = value;
                OnPropertyChanged();
                Filtrar(value);
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

                    await GetTravels(usuario);

                    IsRefreshing = false;
                });
            }
        }
        public Command NewReportCommand { get; set; }
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
        private List<Reports> _reports;
        public List<Models.Reports> Reports
        {
            get => _reports;
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }
        public User usuario { get; set; }
        public ExpensesViewModel(User usuarioFrom)
        {
            usuario = usuarioFrom;
            _ = GetTravels(usuario);
            NewReportCommand = new Command(async () => await NavigateToNewReport());
        }
        public async Task NavigateToNewReport()
        {
            HasPropertyValueChanged = true;
            var bandera = 0;
            for (var i = 0; i < usuario.Assignments.Length; i++)
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
            if (bandera == 1)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NewReportMX(usuario));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NewReportUS(usuario));
            }
            HasPropertyValueChanged = false;
        }
        public async Task GetTravels(User usuario)
        {
            HasPropertyValueChanged = true;
            var client = new HttpClient();
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getReports.php?usuario=" + usuario.Id;
            var result = await client.GetAsync(URL);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Reports = JsonConvert.DeserializeObject<List<Reports>>(responseData, settings);
            }
            HasPropertyValueChanged = false;
        }
        private Reports _selectedItem;
        public Reports ShowReportDetails
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    GoToReport(_selectedItem, usuario);
                }
            }
        }
        public async void GoToReport(Reports eu_report, User usuario)
        {
            HasPropertyValueChanged = true;
            if (eu_report != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new IndividualReport(eu_report, usuario));
                HasPropertyValueChanged = false;
            }
        }
        public void Filtrar(string Busqueda)
        {
            HasPropertyValueChanged = true;
            string Name = "";
            string Assignment = "";
            string Status = "";
            foreach(Reports reporte in Reports)
            {
                Name = reporte.Name.ToUpper();
                Assignment = reporte.AssignmentName.ToUpper();
                Status = reporte.StatusName.ToUpper();
                Busqueda = Busqueda.ToUpper();
                if ( Name.Contains(Busqueda) || Assignment.Contains(Busqueda) || Status.Contains(Busqueda))
                {
                    reporte.Searched = 130;
                }
                else
                {
                    reporte.Searched = 0;
                }
            }
            HasPropertyValueChanged = false;
        }
    }
}
