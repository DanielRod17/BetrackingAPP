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
        private User usuario { get; set; }
        public ExpensesViewModel(User usuarioFrom)
        {
            usuario = usuarioFrom;
            GetTravels(usuario);
        }

        public async void GetTravels(User usuario)
        {

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
            if (eu_report != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new IndividualReport(eu_report, usuario));
            }
        }


    }
}
