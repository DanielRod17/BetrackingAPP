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

        //private INavigation Navigation;
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
            GetTravels(usuario);
            NewReportCommand = new Command(async () => await NavigateToNewReport());
        }
        public async Task NavigateToNewReport()
        {
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

        }
        public async void GetTravels(User usuario)
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
            }
            HasPropertyValueChanged = false;
        }
    }
}
