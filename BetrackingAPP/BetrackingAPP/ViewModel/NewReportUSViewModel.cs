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
using System.Linq;
using Rg.Plugins.Popup.Extensions;

namespace BetrackingAPP.ViewModel
{
    public class NewReportUSViewModel : BaseViewModel
    {
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();
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
        public User Usuario;
        public Command CreateExpense { get; set; }
        private ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments
        {
            get
            {
                return assignments;
            }
        }
        private string _assignmentName { get; set; }
        public string AssignmentName
        {
            get
            {
                return _assignmentName;
            }
            set
            {
                _assignmentName = value;
                OnPropertyChanged();
            }
        }
        private string _name { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        private int _status { get; set; }
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public NewReportUSViewModel(User usuario)
        {
            IsLoading = true;
            Usuario = usuario;
            GetAssignments(usuario);
            CreateExpense = new Command(async () => await CrearReporte());
            IsLoading = false;
        }
        public async Task CrearReporte()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("Payroll", Usuario.Payroll),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("StartDate", StartDate.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("EndDate", EndDate.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Firstname", Usuario.Firstname),
                new KeyValuePair<string, string>("Lastname", Usuario.Lastname),
                new KeyValuePair<string, string>("Email", Usuario.Email),
                new KeyValuePair<string, string>("Status", Status.ToString())
            });

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/CreateReport.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Yes")
                {
                    AssignmentName = "";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, "Expenses' Report Created!"));
                }
                else
                {
                    //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage(responseData));
                }
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
            }
            IsLoading = false;
        }
        public void GetAssignments(User usuario)
        {
            var Assignments_List = usuario.Assignments;
            foreach (Assignment assignment_item in Assignments_List)
            {
                Assignments.Add(assignment_item.Name);
            }
        }
    }
}
