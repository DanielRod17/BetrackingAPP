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
using System.Net;

namespace BetrackingAPP.ViewModel
{
    public class IndividualReportViewModel : BaseViewModel
    {
        public Reports Reporte;

        ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();
        public ObservableCollection<Expense> ExpensesList { get { return expenses; } }

        ObservableCollection<File> files = new ObservableCollection<File>();
        public ObservableCollection<File> FilesList { get { return files; } }
        //public string Name { get; set; }
        public string ProjectName { get; set; }
        public DateTime? FromDate { get; set; }
        //public DateTime? ToDate { get; set; }
        public decimal BaggageExp { get; set; }
        public decimal AirfareExp { get; set; }
        public decimal CarRentalExp { get; set; }
        public decimal GasolineExp { get; set; }
        public decimal LodgingExp { get; set; }
        public decimal ParkingExp { get; set; }
        public decimal MiscExp { get; set; }
        public decimal MealsExp { get; set; }
        public decimal ToolsExp { get; set; }
        public decimal ChangeExp { get; set; }
        public decimal TransportationExp { get; set; }
        public decimal TollExp { get; set; }
        public decimal PerDiemExp { get; set; }
        public decimal Billable { get; set; }
        public decimal Refundable { get; set; }
        public decimal Total { get; set; }


        ///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////

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
        //private int _displayHeight = 0;
        public int DisplayHeight { get; set; }
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
        private string _Country_From { get; set; }
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
        private DateTime? _Arrival_Date = DateTime.Now;
        public DateTime? Arrival_Date
        {
            get
            {
                return _Arrival_Date;
            }
            set
            {
                if (value != null && value.ToString() != "")
                {
                    _Arrival_Date = value;
                    OnPropertyChanged();
                }
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
        private DateTime? _Hotel_Arrival_Date = DateTime.Now;
        public DateTime? Hotel_Arrival_Date
        {
            get
            {
                return _Hotel_Arrival_Date;
            }
            set
            {
                if (value != null && value.ToString() != "")
                {
                    _Hotel_Arrival_Date = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime? _Hotel_Departure_Date = DateTime.Now;
        public DateTime? Hotel_Departure_Date
        {
            get
            {
                return _Hotel_Departure_Date;
            }
            set
            {
                if (value != null && value.ToString() != "")
                {
                    _Hotel_Departure_Date = value;
                    OnPropertyChanged();
                }
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
        private DateTime? _Car_Rental_Pickup_Date = DateTime.Now;
        public DateTime? Car_Rental_Pickup_Date
        {
            get
            {
                return _Car_Rental_Pickup_Date;
            }
            set
            {
                if (value != null && value.ToString() != "")
                {
                    _Car_Rental_Pickup_Date = value;
                    OnPropertyChanged();
                }
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
        private DateTime? _Car_Rental_Return = DateTime.Now;
        public DateTime? Car_Rental_Return
        {
            get
            {
                return _Car_Rental_Return;
            }
            set
            {
                if (value != null && value.ToString() != "")
                {
                    _Car_Rental_Return = value;
                    OnPropertyChanged();
                }
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

        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        public Command DownloadCommand { get; private set; }
        void DownloadFile(string URL)
        {
            URL = "https://bepc.backnetwork.net/BEPCINC" + URL;
            WebClient myWebClient = new WebClient();
            Uri Ur = new Uri(URL);
            Device.OpenUri(Ur);
        }

        public IndividualReportViewModel(User usuarioFrom, Reports reportFrom)
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            Reporte = reportFrom;
            BaggageExp = 0;
            AirfareExp = 0;
            CarRentalExp = 0;
            GasolineExp = 0;
            LodgingExp = 0;
            ParkingExp = 0;
            MiscExp = 0;
            MealsExp = 0;
            ToolsExp = 0;
            ChangeExp = 0;
            TransportationExp = 0;
            TollExp = 0;
            PerDiemExp = 0;
            Billable = 0;
            Refundable = 0;
            Total = 0;

            CargarValores(Reporte);


            DownloadCommand = new Command<string>(DownloadFile);

            if (Reporte.Reason_Request != null)
            {
                DisplayHeight = 45;
                ReportType = 1;
            }
            else
            {
                DisplayHeight = 0;
                ReportType = 0;
            }
            var Expenses_List = reportFrom.Expenses;
            var Files_List = reportFrom.Files;
            decimal Valor_expense = 0;

            ProjectName = reportFrom.ProjectName;
            FromDate = reportFrom.FromDate;
            ToDate = reportFrom.ToDate;
            foreach (File file_item in Files_List)
            {
                files.Add(file_item);
            }
            foreach (Expense expense_item in Expenses_List)
            {
                expenses.Add(expense_item);
                Valor_expense = decimal.Parse(expense_item.Quantity);
                if (expense_item.Currency == 0)
                {
                    Valor_expense = Valor_expense / expense_item.DOF;
                }

                if (expense_item.Refundable == 1)
                {
                    Refundable += Valor_expense;
                }
                Billable += Valor_expense;
                Total += Valor_expense;
                switch (expense_item.Category)
                {
                    case 1:
                        BaggageExp += Valor_expense;
                        break;
                    case 2:
                        AirfareExp += Valor_expense;
                        break;
                    case 3:
                        CarRentalExp += Valor_expense;
                        break;
                    case 4:
                        LodgingExp += Valor_expense;
                        break;
                    case 5:
                        ParkingExp += Valor_expense;
                        break;
                    case 6:
                        GasolineExp += Valor_expense;
                        break;
                    case 7:
                        MiscExp += Valor_expense;
                        break;
                    case 8:
                        MealsExp += Valor_expense;
                        break;
                    case 9:
                        ToolsExp += Valor_expense;
                        break;
                    case 10:
                        ChangeExp += Valor_expense;
                        break;
                    case 11:
                        TransportationExp += Valor_expense;
                        break;
                    case 12:
                        TollExp += Valor_expense;
                        break;
                    case 13:
                        PerDiemExp += Valor_expense;
                        break;
                }
            }
        }

        public async Task CargarValores(Reports Reporte)
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            Name = Reporte.Name;
            AssignmentName = Reporte.AssignmentName;
            FromDate = Reporte.FromDate;
            ToDate = Reporte.ToDate;
            Status = (Reporte.Status + 1);

            ReportType = 0;
            Type_of_travel = Reporte.Type_of_travel;
            Flight = Reporte.Flight;
            From_Departure = Reporte.From_Departure;

            Country_From = Reporte.country_from_name;
            Country_To = Reporte.country_to_name;
            Return_Country_From = Reporte.rcountry_from_name;
            Return_Country_To = Reporte.rcountry_to_name;
            //var taskDelay = Task.Delay(3000);
            await Task.Delay(3000);
            State_From = Reporte.state_from_name;
            State_To = Reporte.state_to_name;
            Return_State_From = Reporte.rstate_from_name;
            Return_State_To = Reporte.rstate_to_name;
            await Task.Delay(2500);
            City_From = Reporte.city_from_name;
            City_To = Reporte.city_to_name;
            Return_City_From = Reporte.rcity_from_name;
            Return_City_To = Reporte.rcity_to_name;
            //await taskDelay;

            Arrival_Date = Reporte.Arrival_Date;
            To_Departure = Reporte.To_Departure;

            Seating_preference = Reporte.Seating_preference;


            Hotel_Arrival_Date = Reporte.Hotel_Arrival_Date;
            Hotel_Departure_Date = Reporte.Hotel_Departure_Date;
            Smoking = Reporte.Smoking;
            Special_Needs = Reporte.Special_Needs;
            Ground_Method = Reporte.Ground_Method;
            Emerald_Club_Number = Reporte.Emeral_Club_Number;
            Car_Rental_Pickup_Date = Reporte.Car_Rental_Pickup_Date;
            Car_Rental_Time = Reporte.Car_Rental_Time;
            Car_Size_Preference = 0;
            Car_Rental_Return = Reporte.Car_Rental_Return;
            Car_Rental_Return_Time = Reporte.Car_Rental_Return_Time;
            Reason_Request = Reporte.Reason_Request;
            Emergency_Contact = Reporte.Emergency_Contact;
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
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

        public async void AddExpenses(Reports reporte, User usuario)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddExpenses(reporte, usuario));
        }
    }
}
