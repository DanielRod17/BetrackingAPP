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
    public class NewReportMXViewModel : BaseViewModel
    {
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();
        public User Usuario;

        /////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////

        private ObservableCollection<string> _StatesListFrom = new ObservableCollection<string>();
        private ObservableCollection<string> _CitiesListFrom = new ObservableCollection<string>();
        public ObservableCollection<string> StatesListFrom
        {
            get
            {
                return _StatesListFrom;
            }
            set
            {
                _StatesListFrom = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> CitiesListFrom
        {
            get
            {
                return _CitiesListFrom;
            }
            set
            {
                _CitiesListFrom = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _StatesListDest = new ObservableCollection<string>();
        private ObservableCollection<string> _CitiesListDest = new ObservableCollection<string>();
        public ObservableCollection<string> StatesListDest
        {
            get
            {
                return _StatesListDest;
            }
            set
            {
                _StatesListDest = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> CitiesListDest
        {
            get
            {
                return _CitiesListDest;
            }
            set
            {
                _CitiesListDest = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _StatesListReturnFrom = new ObservableCollection<string>();
        private ObservableCollection<string> _CitiesListReturnFrom = new ObservableCollection<string>();
        public ObservableCollection<string> StatesListReturnFrom
        {
            get
            {
                return _StatesListReturnFrom;
            }
            set
            {
                _StatesListReturnFrom = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> CitiesListReturnFrom
        {
            get
            {
                return _CitiesListReturnFrom;
            }
            set
            {
                _CitiesListReturnFrom = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _StatesListReturnTo = new ObservableCollection<string>();
        private ObservableCollection<string> _CitiesListReturnTo = new ObservableCollection<string>();
        public ObservableCollection<string> StatesListReturnTo
        {
            get
            {
                return _StatesListReturnTo;
            }
            set
            {
                _StatesListReturnTo = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> CitiesListReturnTo
        {
            get
            {
                return _CitiesListReturnTo;
            }
            set
            {
                _CitiesListReturnTo = value;
                OnPropertyChanged();
            }
        }

        //////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////
        public Command CreateExpense { get; set; }
        private ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments
        {
            get
            {
                return assignments;
            }
            set
            {
                assignments = value;
                OnPropertyChanged();
            }
        }
        private bool _HasPropertyValueChanged = false;
        public bool HasPropertyValueChanged
        {
            get
            {
                return _HasPropertyValueChanged;
            }
            set
            {
                _HasPropertyValueChanged = value;
                OnPropertyChanged();
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
        private int _displayHeight = 45;
        public int DisplayHeight
        {
            get
            {
                return _displayHeight;
            }
            set
            {
                _displayHeight = value;
                OnPropertyChanged();
            }
        }
        private int _reportType = 0;
        public int ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                _reportType = value;
                if (_reportType == 1)
                {
                    DisplayHeight = 0;
                }
                else
                {
                    DisplayHeight = 45;
                }
                OnPropertyChanged();
            }
        }
        private int _type_of_travel = 0;
        public int Type_of_travel
        {
            get
            {
                return _type_of_travel;
            }
            set
            {
                _type_of_travel = value;
                OnPropertyChanged();
            }
        }
        private int _Flight = 0;
        public int Flight
        {
            get
            {
                return _Flight;
            }
            set
            {
                _Flight = value;
                OnPropertyChanged();
            }
        }
        private int _From_Departure = 0;
        public int From_Departure
        {
            get
            {
                return _From_Departure;
            }
            set
            {
                _From_Departure = value;
                OnPropertyChanged();
            }
        }
        private string _Country_From = "MX";
        public string Country_From
        {
            get
            {
                return _Country_From;
            }
            set
            {
                _Country_From = value;
                ChangeStates(1);
                ChangeCities(1);
                OnPropertyChanged();
            }
        }
        private string _State_From { get; set; }
        public string State_From
        {
            get
            {
                return _State_From;
            }
            set
            {
                _State_From = value;
                ChangeCities(1);
                OnPropertyChanged();
            }
        }
        private string _City_From { get; set; }
        public string City_From
        {
            get
            {
                return _City_From;
            }
            set
            {
                _City_From = value;
                OnPropertyChanged();
            }
        }
        private DateTime _ToDate = DateTime.Now;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Arrival_Date = DateTime.Now;
        public DateTime Arrival_Date
        {
            get
            {
                return _Arrival_Date;
            }
            set
            {
                _Arrival_Date = value;
                OnPropertyChanged();
            }
        }
        private int _To_Departure = 0;
        public int To_Departure
        {
            get
            {
                return _To_Departure;
            }
            set
            {
                _To_Departure = value;
                OnPropertyChanged();
            }
        }
        private string _Country_To = "MX";
        public string Country_To
        {
            get
            {
                return _Country_To;
            }
            set
            {
                _Country_To = value;
                ChangeStates(2);
                ChangeCities(2);
                OnPropertyChanged();
            }
        }
        private string _State_To { get; set; }
        public string State_To
        {
            get
            {
                return _State_To;
            }
            set
            {
                _State_To = value;
                ChangeCities(2);
                OnPropertyChanged();
            }
        }
        private string _City_To { get; set; }
        public string City_To
        {
            get
            {
                return _City_To;
            }
            set
            {
                _City_To = value;
                OnPropertyChanged();
            }
        }
        private int _Seating_preference = 0;
        public int Seating_preference
        {
            get
            {
                return _Seating_preference;
            }
            set
            {
                _Seating_preference = value;
                OnPropertyChanged();
            }
        }
        private string _Return_Country_From = "MX";
        public string Return_Country_From
        {
            get
            {
                return _Return_Country_From;
            }
            set
            {
                _Return_Country_From = value;
                ChangeStates(3);
                ChangeCities(3);
                OnPropertyChanged();
            }
        }
        private string _Return_State_From { get; set; }
        public string Return_State_From
        {
            get
            {
                return _Return_State_From;
            }
            set
            {
                _Return_State_From = value;
                ChangeCities(3);
                OnPropertyChanged();
            }
        }
        private string _Return_City_From { get; set; }
        public string Return_City_From
        {
            get
            {
                return _Return_City_From;
            }
            set
            {
                _Return_City_From = value;
                OnPropertyChanged();
            }
        }
        private string _Return_Country_To = "MX";
        public string Return_Country_To
        {
            get
            {
                return _Return_Country_To;
            }
            set
            {
                _Return_Country_To = value;
                ChangeStates(4);
                ChangeCities(4);
                OnPropertyChanged();
            }
        }
        private string _Return_State_To { get; set; }
        public string Return_State_To
        {
            get
            {
                return _Return_State_To;
            }
            set
            {
                _Return_State_To = value;
                ChangeCities(4);
                OnPropertyChanged();
            }
        }
        private string _Return_City_To { get; set; }
        public string Return_City_To
        {
            get
            {
                return _Return_City_To;
            }
            set
            {
                _Return_City_To = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Hotel_Arrival_Date = DateTime.Now;
        public DateTime Hotel_Arrival_Date
        {
            get
            {
                return _Hotel_Arrival_Date;
            }
            set
            {
                _Hotel_Arrival_Date = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Hotel_Departure_Date = DateTime.Now;
        public DateTime Hotel_Departure_Date
        {
            get
            {
                return _Hotel_Departure_Date;
            }
            set
            {
                _Hotel_Departure_Date = value;
                OnPropertyChanged();
            }
        }
        private int _Smoking = 0;
        public int Smoking
        {
            get
            {
                return _Smoking;
            }
            set
            {
                _Smoking = value;
                OnPropertyChanged();
            }
        }
        private string _Special_Needs { get; set; }
        public string Special_Needs
        {
            get
            {
                return _Special_Needs;
            }
            set
            {
                _Special_Needs = value;
                OnPropertyChanged();
            }
        }
        private int _Ground_Method = 0;
        public int Ground_Method
        {
            get
            {
                return _Ground_Method;
            }
            set
            {
                _Ground_Method = value;
                OnPropertyChanged();
            }
        }
        private string _Emerald_Club_Number { get; set; }
        public string Emerald_Club_Number
        {
            get
            {
                return _Emerald_Club_Number;
            }
            set
            {
                _Emerald_Club_Number = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Car_Rental_Pickup_Date = DateTime.Now;
        public DateTime Car_Rental_Pickup_Date
        {
            get
            {
                return _Car_Rental_Pickup_Date;
            }
            set
            {
                _Car_Rental_Pickup_Date = value;
                OnPropertyChanged();
            }
        }
        private int _Car_Rental_Time = 0;
        public int Car_Rental_Time
        {
            get
            {
                return _Car_Rental_Time;
            }
            set
            {
                _Car_Rental_Time = value;
                OnPropertyChanged();
            }
        }
        private int _Car_Size_Preference = 0;
        public int Car_Size_Preference
        {
            get
            {
                return _Car_Size_Preference;
            }
            set
            {
                _Car_Size_Preference = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Car_Rental_Return = DateTime.Now;
        public DateTime Car_Rental_Return
        {
            get
            {
                return _Car_Rental_Return;
            }
            set
            {
                _Car_Rental_Return = value;
                OnPropertyChanged();
            }
        }
        private int _Car_Rental_Return_Time = 0;
        public int Car_Rental_Return_Time
        {
            get
            {
                return _Car_Rental_Return_Time;
            }
            set
            {
                _Car_Rental_Return_Time = value;
                OnPropertyChanged();
            }
        }
        private string _Reason_Request { get; set; }
        public string Reason_Request
        {
            get
            {
                return _Reason_Request;
            }
            set
            {
                _Reason_Request = value;
                OnPropertyChanged();
            }
        }
        private string _Emergency_Contact { get; set; }
        public string Emergency_Contact
        {
            get
            {
                return _Emergency_Contact;
            }
            set
            {
                _Emergency_Contact = value;
                OnPropertyChanged();
            }
        }
        private int _Status = 0;
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }
        public NewReportMXViewModel(User usuario)
        {
            Usuario = usuario;
            GetAssignments(usuario);
            CreateExpense = new Command(async () => await CrearReporte());
        }
        public async Task CrearReporte()
        {
            HttpClient client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("Payroll", Usuario.Payroll),
                new KeyValuePair<string, string>("ReportType", ReportType.ToString()),
                new KeyValuePair<string, string>("Type_of_travel", Type_of_travel.ToString()),
                new KeyValuePair<string, string>("Flight", Flight.ToString()),
                new KeyValuePair<string, string>("FromDate", StartDate.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("From_Departure", From_Departure.ToString()),
                new KeyValuePair<string, string>("State_From", State_From),
                new KeyValuePair<string, string>("City_From", City_From),
                new KeyValuePair<string, string>("ToDate", ToDate.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Arrival_Date", Arrival_Date.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("To_Departure", To_Departure.ToString()),
                new KeyValuePair<string, string>("State_To", State_To),
                new KeyValuePair<string, string>("City_To", City_To),
                new KeyValuePair<string, string>("Seating_preference", Seating_preference.ToString()),
                new KeyValuePair<string, string>("Return_State_From", Return_State_From),
                new KeyValuePair<string, string>("Return_City_From", Return_City_From),
                new KeyValuePair<string, string>("Return_State_To", Return_State_To),
                new KeyValuePair<string, string>("Return_City_To", Return_City_To),
                new KeyValuePair<string, string>("Hotel_Arrival_Date", Hotel_Arrival_Date.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Hotel_Departure_Date", Hotel_Departure_Date.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Smoking", Smoking.ToString()),
                new KeyValuePair<string, string>("Special_Needs", Special_Needs),
                new KeyValuePair<string, string>("Ground_Method", Ground_Method.ToString()),
                new KeyValuePair<string, string>("Emerald_Club_Number", Emerald_Club_Number),
                new KeyValuePair<string, string>("Car_Rental_Pickup_Date", Car_Rental_Pickup_Date.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Car_Rental_Time", Car_Rental_Time.ToString()),
                new KeyValuePair<string, string>("Car_Size_Preference", Car_Size_Preference.ToString()),
                new KeyValuePair<string, string>("Car_Rental_Return", Car_Rental_Return.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Car_Rental_Return_Time", Car_Rental_Return_Time.ToString()),
                new KeyValuePair<string, string>("Reason_Request", Reason_Request),
                new KeyValuePair<string, string>("Emergency_Contact", Emergency_Contact),
                new KeyValuePair<string, string>("Status", Status.ToString()),
                new KeyValuePair<string, string>("Firstname", Usuario.Firstname),
                new KeyValuePair<string, string>("Lastname", Usuario.Lastname),
                new KeyValuePair<string, string>("Email", Usuario.Email)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/CreateReportMX.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Saved!")
                {
                    AssignmentName = "";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
        }
        public void GetAssignments(User usuario)
        {
            var Assignments_List = usuario.Assignments;
            foreach (Assignment assignment_item in Assignments_List)
            {
                Assignments.Add(assignment_item.Name);
            }
        }
        public async void ChangeStates(int v)
        {
            string country_val = "";
            if (v == 1)
            {
                country_val = Country_From;
            }
            else if (v == 2)
            {
                country_val = Country_To;
            }
            else if (v == 3)
            {
                country_val = Return_Country_From;
            }
            else if (v == 4)
            {
                country_val = Return_Country_To;
            }
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://bepc.backnetwork.net/BEPCINC/api/getStates.php?Country=" + country_val);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<ObservableCollection<string>>(responseData);
                if (v == 1)
                {
                    StatesListFrom = estados;
                }
                else if (v == 2)
                {
                    StatesListDest = estados;
                }
                else if (v == 3)
                {
                    StatesListReturnFrom = estados;
                }
                else if (v == 4)
                {
                    StatesListReturnTo = estados;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
        }
        public async void ChangeCities(int v)
        {
            string state_val = "";
            if (v == 1)
            {
                state_val = State_From;
            }
            else if (v == 2)
            {
                state_val = State_To;
            }
            else if (v == 3)
            {
                state_val = Return_State_From;
            }
            else if (v == 4)
            {
                state_val = Return_State_To;
            }
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://bepc.backnetwork.net/BEPCINC/api/getStates.php?State=" + state_val);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<ObservableCollection<string>>(responseData);
                if (v == 1)
                {
                    CitiesListFrom = cities;
                }
                else if (v == 2)
                {
                    CitiesListDest = cities;
                }
                else if (v == 3)
                {
                    CitiesListReturnFrom = cities;
                }
                else if (v == 4)
                {
                    CitiesListReturnTo = cities;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
        }
    }
}
