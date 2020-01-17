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
using Plugin.DownloadManager;
using System.Net;
using Plugin.DownloadManager.Abstractions;

namespace BetrackingAPP.ViewModel
{
    public class IndividualReportViewModel : BaseViewModel
    {
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
        private bool _hasPropertyValueChanged { get; set; }
        public bool HasPropertyValueChanged
        {
            get
            {
                return _hasPropertyValueChanged;
            }
            set
            {
                _hasPropertyValueChanged = value;
                OnPropertyChanged();
            }
        }
        private bool _isTentative { get; set; }
        public bool IsTentative
        {
            get
            {
                return _isTentative;
            }
            set
            {
                _isTentative = value;
                OnPropertyChanged();
            }
        }
        private Reports _reporte { get; set; }
        public Reports Reporte
        {
            get
            {
                return _reporte;
            }
            set
            {
                _reporte = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();
        public ObservableCollection<Expense> ExpensesList
        {
            get
            {
                return expenses;
            }
            set
            {
                expenses = value;
                OnPropertyChanged();
            }
        }
        public int DisplayReportMode { get; set; }
        //public string Name { get; set; }
        public string ProjectName { get; set; }
        public DateTime? FromDate { get; set; }
        //public DateTime? ToDate { get; set; }
        private decimal _baggage { get; set; }
        public decimal BaggageExp
        {
            get
            {
                return _baggage;
            }
            set
            {
                _baggage = value;
                OnPropertyChanged();
            }
        }
        private decimal _airfare { get; set; }
        public decimal AirfareExp
        {
            get
            {
                return _airfare;
            }
            set
            {
                _airfare = value;
                OnPropertyChanged();
            }
            
        }
        private decimal _carRental { get; set; }
        public decimal CarRentalExp
        {
            get
            {
                return _carRental;
            }
            set
            {
                _carRental = value;
                OnPropertyChanged();
            }
        }
        private decimal _gasoline { get; set; }
        public decimal GasolineExp
        {
            get
            {
                return _gasoline;
            }
            set
            {
                _gasoline = value;
                OnPropertyChanged();
            }
        }
        private decimal _lodging { get; set; }
        public decimal LodgingExp
        {
            get
            {
                return _lodging;
            }
            set
            {
                _lodging = value;
                OnPropertyChanged();
            }
        }
        private decimal _parking { get; set; }
        public decimal ParkingExp
        {
            get
            {
                return _parking;
            }
            set
            {
                _parking = value;
                OnPropertyChanged();
            }
        }
        private decimal _misc { get; set; }
        public decimal MiscExp
        {
            get
            {
                return _misc;
            }
            set
            {
                _misc = value;
                OnPropertyChanged();
            }
        }
        private decimal _meals { get; set; }
        public decimal MealsExp
        {
            get
            {
                return _meals;
            }
            set
            {
                _meals = value;
                OnPropertyChanged();
            }
        }
        private decimal _tools { get; set; }
        public decimal ToolsExp
        {
            get
            {
                return _tools;
            }
            set
            {
                _tools = value;
                OnPropertyChanged();
            }
        }
        private decimal _change { get; set; }
        public decimal ChangeExp
        {
            get
            {
                return _change;
            }
            set
            {
                _change = value;
                OnPropertyChanged();
            }
        }
        private decimal _transportation { get; set; }
        public decimal TransportationExp
        {
            get
            {
                return _transportation;
            }
            set
            {
                _transportation = value;
                OnPropertyChanged();
            }
        }
        private decimal _toll { get; set; }
        public decimal TollExp
        {
            get
            {
                return _toll;
            }
            set
            {
                _toll = value;
                OnPropertyChanged();
            }
        }
        private decimal _perDiem { get; set; }
        public decimal PerDiemExp
        {
            get
            {
                return _perDiem;
            }
            set
            {
                _perDiem = value;
                OnPropertyChanged();
            }
        }
        public decimal Billable { get; set; }
        public decimal Refundable { get; set; }
        public decimal Total { get; set; }


        ///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////

        private ObservableCollection<File> files = new ObservableCollection<File>();
        public ObservableCollection<File> FilesList
        {
            get
            {
                return files;
            }
            set
            {
                files = value;
                OnPropertyChanged();
            }
        }
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
        internal async void SubmitReport(Reports eu_report, User usuario)
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Usuario",usuario.Id.ToString()),
                new KeyValuePair<string, string>("Reporte", eu_report.ID.ToString()),
                new KeyValuePair<string, string>("Firstname", usuario.Firstname),
                new KeyValuePair<string, string>("Lastname", usuario.Lastname),
                new KeyValuePair<string, string>("Email", usuario.Email),
                new KeyValuePair<string, string>("Payroll", usuario.Payroll.ToString())
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SubmitReport.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Request Sent for approval")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, responseData));
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
        private string _projectName { get; set; }
        public string ProjectNeim
        {
            get
            {
                return _projectName;
            }
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }
        private string _statusName { get; set; }
        public string StatusName
        {
            get
            {
                return _statusName;
            }
            set
            {
                _statusName = value;
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
        private int _displayHeight { get; set; }
        public int DisplayHeight {
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
        public User Usuario { get; set; }
        public Command UpdateReport { get; set; }

        //////////////////////////////////////////////////////
        //////////////////////////////////////////////////////
        public Command DownloadCommand { get; private set; }
        public Command DeleteFileCommand { get; private set; }
        public IDownloadFile File;
        async void DownloadFile(string URL)
        {
            
            IsLoading = true;
            URL = "https://bepc.backnetwork.net/BEPCINC" + URL;
            File = CrossDownloadManager.Current.CreateDownloadFile(
                URL
            );
            CrossDownloadManager.Current.Start(File, true);
            bool isDownloading = true;
            while (isDownloading)
            {
                await Task.Delay(500);
                isDownloading = IsDownloading(File);
            }
            //await Application.Current.MainPage.DisplayAlert("Success", "File Downloaded!", "OK");
            await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, "File Downloaded!"));
            IsLoading = false;
            
            //IsLoading = true;
            /*HttpClient client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("reporte", Reporte.ID.ToString()),
                new KeyValuePair<string, string>("archivo", URL)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/DescargarFile.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
            }
            else
            {
                IsLoading = false;
            }*/
        }
        public async void DeleteFileAsync(File Arquivo)
        {
            IsLoading = true;
            HttpClient client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("reporte", Reporte.ID.ToString()),
                new KeyValuePair<string, string>("file", Arquivo.URL),
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/DeleteFile.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "File Deleted!")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, "File Deleted!"));
                    FilesList.Remove(Arquivo);
                }
                else
                {
                    //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage(responseData));
                }
                
            }
            IsLoading = false;
        }
        bool IsDownloading(IDownloadFile file)
        {
            if (file == null) return false;

            switch (file.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                    return true;

                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public IndividualReportViewModel(User usuarioFrom, Reports reportFrom)
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            Reporte = reportFrom;
            Usuario = usuarioFrom;
            var bandera = 0;
            for (var i = 0; i < usuarioFrom.Assignments.Length; i++)
            {
                if (usuarioFrom.Assignments[i].Division == 5 || usuarioFrom.Assignments[i].Division == 2)
                {
                    bandera = 1;
                }
            }
            if (usuarioFrom.Payroll == "142")
            {
                bandera = 1;
            }
            if (bandera == 1)
            {
                DisplayReportMode = 45;
            }
            else
            {
                DisplayReportMode = 0;
            }
            MessagingCenter.Subscribe<IndividualReportViewModel>(this, "Change", (sender) =>
            {
                ReCargarValores();
            });
            IsLoading = true;
            BaggageExp = 0;
            ProjectNeim = Reporte.ProjectName;
            StatusName = Reporte.StatusName;
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
            FilesList = new ObservableCollection<File>();
            if (Reporte.Reason_Request != null)
            {
                ReportType = 0;
                DisplayHeight = 45;
            }
            else if (Reporte.Reason_Request == null)
            {
                ReportType = 1;
                DisplayHeight = 0;
            }

            CargarValores();

            DownloadCommand = new Command<string>(DownloadFile);
            DeleteFileCommand = new Command<Models.File>(DeleteFileAsync);
            GetAssignments(usuarioFrom);
            if (Reporte.Status == 0 || Reporte.Status == 2)
            {
                HasPropertyValueChanged = true;
            }
            else
            {
                HasPropertyValueChanged = false;
            }

            if (Reporte.Status == -1)
            {
                IsTentative = true;
            }
            else
            {
                IsTentative = false;
            }
            UpdateReport = new Command(async () => await ActualizarReporte());
        }
        public void GetAssignments(User usuario)
        {
            var Assignments_List = usuario.Assignments;
            foreach (Assignment assignment_item in Assignments_List)
            {
                Assignments.Add(assignment_item.Name);
            }
        }
        public async void ReCargarValores()
        {
            IsLoading = true;
            var client = new HttpClient();
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getReport.php?reporte=" + Reporte.ID;
            var result = await client.GetAsync(URL);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Reporte = JsonConvert.DeserializeObject<Reports>(responseData, settings);
                ExpensesList = new ObservableCollection<Expense>();
                CargarValores();
            }
        }
        public async void CargarValores()
        {
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            IsLoading = true;
            Name = Reporte.Name;
            AssignmentName = Reporte.AssignmentName;
            FromDate = Reporte.FromDate;
            ToDate = Reporte.ToDate;
            Status = (Reporte.Status + 1);
            Type_of_travel = Reporte.Type_of_travel;
            Flight = Reporte.Flight;
            From_Departure = Reporte.From_Departure;
            Country_From = Reporte.country_from_name;
            Country_To = Reporte.country_to_name;
            Return_Country_From = Reporte.rcountry_from_name;
            Return_Country_To = Reporte.rcountry_to_name;    
            await Task.Delay(2000);
            State_From = Reporte.state_from_name;
            State_To = Reporte.state_to_name;
            Return_State_From = Reporte.rstate_from_name;
            Return_State_To = Reporte.rstate_to_name;
            await Task.Delay(1500);
            City_From = Reporte.city_from_name;
            City_To = Reporte.city_to_name;
            Return_City_From = Reporte.rcity_from_name;
            Return_City_To = Reporte.rcity_to_name;            
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
            var Expenses_Lista = Reporte.Expenses;
            var Files_List = Reporte.Files;
            decimal Valor_expense = 0;

            ProjectName = Reporte.ProjectName;
            FromDate = Reporte.FromDate;
            ToDate = Reporte.ToDate;
            //files = new ObservableCollection<File>();
            ExpensesList = new ObservableCollection<Expense>();
            foreach (File file_item in Files_List)
            {
                FilesList.Add(file_item);
            }
            foreach (Expense expense_item in Expenses_Lista)
            {
                ExpensesList.Add(expense_item);
                Valor_expense = decimal.Parse(expense_item.Quantity);
                if (expense_item.Currency == 0)
                {
                    Valor_expense = decimal.Parse(String.Format("{0:.##}", (Valor_expense / expense_item.DOF)));
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
            IsLoading = false;
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
            IsLoading = true;
            await App.Current.MainPage.Navigation.PushAsync(new AddExpenses(reporte, usuario));
            IsLoading = false;
        }
        public async void AddFiles(Reports reporte, User usuario)
        {
            IsLoading = true;
            await App.Current.MainPage.Navigation.PushAsync(new AddFiles(reporte, usuario));
            IsLoading = false;
        }
        public async Task ActualizarReporte()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("ReporteID", Reporte.ID.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("Name", Name),
                new KeyValuePair<string, string>("Payroll", Usuario.Payroll),
                new KeyValuePair<string, string>("ReportType", ReportType.ToString()),
                new KeyValuePair<string, string>("Type_of_travel", Type_of_travel.ToString()),
                new KeyValuePair<string, string>("Flight", Flight.ToString()),
                new KeyValuePair<string, string>("FromDate", FromDate.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("From_Departure", From_Departure.ToString()),
                new KeyValuePair<string, string>("State_From", State_From),
                new KeyValuePair<string, string>("City_From", City_From),
                new KeyValuePair<string, string>("ToDate", ToDate.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Arrival_Date", Arrival_Date.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("To_Departure", To_Departure.ToString()),
                new KeyValuePair<string, string>("State_To", State_To),
                new KeyValuePair<string, string>("City_To", City_To),
                new KeyValuePair<string, string>("Seating_preference", Seating_preference.ToString()),
                new KeyValuePair<string, string>("Return_State_From", Return_State_From),
                new KeyValuePair<string, string>("Return_City_From", Return_City_From),
                new KeyValuePair<string, string>("Return_State_To", Return_State_To),
                new KeyValuePair<string, string>("Return_City_To", Return_City_To),
                new KeyValuePair<string, string>("Hotel_Arrival_Date", Hotel_Arrival_Date.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Hotel_Departure_Date", Hotel_Departure_Date.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Smoking", Smoking.ToString()),
                new KeyValuePair<string, string>("Special_Needs", Special_Needs),
                new KeyValuePair<string, string>("Ground_Method", Ground_Method.ToString()),
                new KeyValuePair<string, string>("Emerald_Club_Number", Emerald_Club_Number),
                new KeyValuePair<string, string>("Car_Rental_Pickup_Date", Car_Rental_Pickup_Date.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Car_Rental_Time", Car_Rental_Time.ToString()),
                new KeyValuePair<string, string>("Car_Size_Preference", Car_Size_Preference.ToString()),
                new KeyValuePair<string, string>("Car_Rental_Return", Car_Rental_Return.Value.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Car_Rental_Return_Time", Car_Rental_Return_Time.ToString()),
                new KeyValuePair<string, string>("Reason_Request", Reason_Request),
                new KeyValuePair<string, string>("Emergency_Contact", Emergency_Contact),
                new KeyValuePair<string, string>("Status", Status.ToString()),
                new KeyValuePair<string, string>("Firstname", Usuario.Firstname),
                new KeyValuePair<string, string>("Lastname", Usuario.Lastname),
                new KeyValuePair<string, string>("Email", Usuario.Email)
            });

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/UpdateReport.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Report Updated!")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario));
                    if (Status == 0)
                    {
                        IsTentative = false;
                        HasPropertyValueChanged = true;
                    }
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
    }
}
