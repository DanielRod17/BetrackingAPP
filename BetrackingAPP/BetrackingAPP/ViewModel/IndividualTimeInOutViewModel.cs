using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BetrackingAPP.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using BetrackingAPP.Views;

namespace BetrackingAPP.ViewModel
{
    public class IndividualTimeInOutViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private bool _submitted { get; set; }
        public bool SubmittedCard
        {
            get
            {
                return _submitted;
            }
            set
            {
                _submitted = value;
                OnPropertyChanged();
            }
        }
        public Command OpenDatePicker { get; set; }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();
        public DateTime _dateSearch = DateTime.Today;
        private string _dateString { get; set; }
        public string DateString
        {
            get
            {
                return _dateString;
            }
            set
            {
                _dateString = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments { get { return assignments; } }
        public DateTime FechaSeleccionada
        {
            get
            {
                return _dateSearch;
            }
            set
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

                DateString = _dateSearch.ToShortDateString();
                OnPropertyChanged();

            }
        }
        public User User { get; set; }
        public string Timecard_ID { get; set; }
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
        private string _assignmentName;
        public string AssignmentName
        {
            set
            {
                _assignmentName = value;
                OnPropertyChanged();
            }
            get
            {
                return _assignmentName;
            }
        }
        public Timecard Timecard { get; set; }
        private decimal _monH { get; set; }
        public decimal Mon_Hours {
            get
            {
                return _monH;
            }
            set
            {
                _monH = value;
                OnPropertyChanged();
            }
        }
        private decimal _tueH { get; set; }
        public decimal Tue_Hours {
            get
            {
                return _tueH;
            }
            set
            {
                _tueH = value;
                OnPropertyChanged();
            }
        }
        private decimal _wedH { get; set; }
        public decimal Wed_Hours {
            get
            {
                return _wedH;
            }
            set
            {
                _wedH = value;
                OnPropertyChanged();
            }
        }
        private decimal _thuH { get; set; }
        public decimal Thu_Hours {
            get
            {
                return _thuH;
            }
            set
            {
                _thuH = value;
                OnPropertyChanged();
            }
        }
        private decimal _friH { get; set; }
        public decimal Fri_Hours {
            get
            {
                return _friH;
            }
            set
            {
                _friH = value;
                OnPropertyChanged();
            }
        }
        private decimal _satH { get; set; }
        public decimal Sat_Hours {
            get
            {
                return _satH;
            }
            set
            {
                _satH = value;
                OnPropertyChanged();
            }
        }
        private decimal _sunH { get; set; }
        public decimal Sun_Hours {
            get
            {
                return _sunH;
            }
            set
            {
                _sunH = value;
                OnPropertyChanged();
            }
        }
        public string Mon_Note { get; set; }
        public string Tue_Note { get; set; }
        public string Wed_Note { get; set; }
        public string Thu_Note { get; set; }
        public string Fri_Note { get; set; }
        public string Sat_Note { get; set; }
        public string Sun_Note { get; set; }
        public string Assignment_Name { get; set; }
        public decimal Total_Hours { get; set; }
        public string monday_info { get; set; }
        private User usuario;
        private Timecard _timecard{ get; set; }
        public Timecard timecard
        {
            get
            {
                return _timecard;
            }
            set
            {
                _timecard = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Timecard> _info { get; set; }
        public ObservableCollection<Timecard> Info
        {
            get
            {
                return _info;
            }

            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<Actions> acciones = new ObservableCollection<Actions>();
        public ObservableCollection<Actions> Actions
        {
            get
            {
                return acciones;
            }
            set
            {
                acciones = value;
                OnPropertyChanged("Actions");
            }
        }
        //ObservableCollection<NewTimeOutCard> _dias;
        private ObservableCollection<NewTimeOutCard> _days;
        public ObservableCollection<NewTimeOutCard> Days {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }
        public IndividualTimeInOutViewModel(User usuarioFrom, Timecard timecardFrom, DateTime _dateSearch)
        {
            if (timecardFrom.Submitted == 0)
            {
                SubmittedCard = true;
            }
            else
            {
                SubmittedCard = false;
            }
            FechaSeleccionada = _dateSearch;
            usuario = usuarioFrom;
            User = usuario;
            timecard = timecardFrom;
            Timecard_ID = timecard.TimecardID;
            var fecha = _dateSearch;
            int monNum = fecha.AddDays(-6).Day;
            int tueNum = fecha.AddDays(-5).Day;
            int wedNum = fecha.AddDays(-4).Day;
            int thuNum = fecha.AddDays(-3).Day;
            int friNum = fecha.AddDays(-2).Day;
            int satNum = fecha.AddDays(-1).Day;
            int sunNum = fecha.Day;
            int monDisplay = 0;
            int monBreak1 = 0;
            int monBreak2 = 0;
            int monBreak3 = 0;
            bool monBreak1Enabled = false;
            bool monBreak2Enabled = false;
            bool monBreak3Enabled = false;
            int tueDisplay = 0;
            int tueBreak1 = 0;
            int tueBreak2 = 0;
            int tueBreak3 = 0;
            bool tueBreak1Enabled = false;
            bool tueBreak2Enabled = false;
            bool tueBreak3Enabled = false;
            int wedDisplay = 0;
            int wedBreak1 = 0;
            int wedBreak2 = 0;
            int wedBreak3 = 0;
            bool wedBreak1Enabled = false;
            bool wedBreak2Enabled = false;
            bool wedBreak3Enabled = false;
            int thuDisplay = 0;
            int thuBreak1 = 0;
            int thuBreak2 = 0;
            int thuBreak3 = 0;
            bool thuBreak1Enabled = false;
            bool thuBreak2Enabled = false;
            bool thuBreak3Enabled = false;
            int friDisplay = 0;
            int friBreak1 = 0;
            int friBreak2 = 0;
            int friBreak3 = 0;
            bool friBreak1Enabled = false;
            bool friBreak2Enabled = false;
            bool friBreak3Enabled = false;
            int satDisplay = 0;
            int satBreak1 = 0;
            int satBreak2 = 0;
            int satBreak3 = 0;
            bool satBreak1Enabled = false;
            bool satBreak2Enabled = false;
            bool satBreak3Enabled = false;
            int sunDisplay = 0;
            int sunBreak1 = 0;
            int sunBreak2 = 0;
            int sunBreak3 = 0;
            bool sunBreak1Enabled = false;
            bool sunBreak2Enabled = false;
            bool sunBreak3Enabled = false;
            bool mon24 = false;
            bool tue24 = false;
            bool wed24 = false;
            bool thu24 = false;
            bool fri24 = false;
            bool sat24 = false;
            bool sun24 = false;

            if (timecard.Mon != 0 && timecard.Mon_In == "00:00:00" && timecard.Mon_Out == "00:00:00")
            {
                mon24 = true;
            }
            if (timecard.Tue != 0 && timecard.Tue_In == "00:00:00" && timecard.Tue_Out == "00:00:00")
            {
                tue24 = true;
            }
            if (timecard.Wed != 0 && timecard.Wed_In == "00:00:00" && timecard.Wed_Out == "00:00:00")
            {
                wed24 = true;
            }
            if (timecard.Thu != 0 && timecard.Thu_In == "00:00:00" && timecard.Thu_Out == "00:00:00")
            {
                thu24 = true;
            }
            if (timecard.Fri != 0 && timecard.Fri_In == "00:00:00" && timecard.Fri_Out == "00:00:00")
            {
                fri24 = true;
            }
            if (timecard.Sat != 0 && timecard.Sat_In == "00:00:00" && timecard.Sat_Out == "00:00:00")
            {
                sat24 = true;
            }
            if (timecard.Sun != 0 && timecard.Sun_In == "00:00:00" && timecard.Sun_Out == "00:00:00")
            {
                sun24 = true;
            }



            if (timecard.Mon_Meal_In != "00:00:00")
            {
                //monDisplay += 70;
                monBreak1 = 45;
                monBreak1Enabled = true;
                if (timecard.Mon_Break1_In != "00:00:00")
                {
                    //monDisplay += 70;
                    monBreak2 = 45;
                    monBreak2Enabled = true;
                    if (timecard.Mon_Break2_In != "00:00:00")
                    {
                        monBreak3 = 45;
                        monBreak3Enabled = true;
                        //monDisplay += 70;
                    }
                }
            }

            if (timecard.Tue_Meal_In != "00:00:00")
            {
                //tueDisplay += 70;
                tueBreak1 = 45;
                tueBreak1Enabled = true;
                if (timecard.Tue_Break1_In != "00:00:00")
                {
                    //tueDisplay += 70;
                    tueBreak2 = 45;
                    tueBreak2Enabled = true;
                    if (timecard.Tue_Break2_In != "00:00:00")
                    {
                        tueBreak3 = 45;
                        tueBreak3Enabled = true;
                        //tueDisplay += 70;
                    }
                }
            }

            if (timecard.Wed_Meal_In != "00:00:00")
            {
                //wedDisplay += 70;
                wedBreak1 = 45;
                wedBreak1Enabled = true;
                if (timecard.Wed_Break1_In != "00:00:00")
                {
                    //wedDisplay += 70;
                    wedBreak2 = 45;
                    wedBreak2Enabled = true;
                    if (timecard.Wed_Break2_In != "00:00:00")
                    {
                        wedBreak3 = 45;
                        wedBreak3Enabled = true;
                        //wedDisplay += 70;
                    }
                }
            }

            if (timecard.Thu_Meal_In != "00:00:00")
            {
                //thuDisplay += 70;
                thuBreak1 = 45;
                thuBreak1Enabled = true;
                if (timecard.Thu_Break1_In != "00:00:00")
                {
                    //thuDisplay += 70;
                    thuBreak2 = 45;
                    thuBreak2Enabled = true;
                    if (timecard.Thu_Break2_In != "00:00:00")
                    {
                        thuBreak3 = 45;
                        thuBreak3Enabled = true;
                        //thuDisplay += 70;
                    }
                }
            }

            if (timecard.Fri_Meal_In != "00:00:00")
            {
                //friDisplay += 70;
                friBreak1 = 45;
                friBreak1Enabled = true;
                if (timecard.Fri_Break1_In != "00:00:00")
                {
                    //friDisplay += 70;
                    friBreak2 = 45;
                    friBreak2Enabled = true;
                    if (timecard.Fri_Break2_In != "00:00:00")
                    {
                        friBreak3 = 45;
                        friBreak3Enabled = true;
                        //friDisplay += 70;
                    }
                }
            }

            if (timecard.Sat_Meal_In != "00:00:00")
            {
                //satDisplay += 70;
                satBreak1 = 45;
                satBreak1Enabled = true;
                if (timecard.Sat_Break1_In != "00:00:00")
                {
                    //satDisplay += 70;
                    satBreak2 = 45;
                    satBreak2Enabled = true;
                    if (timecard.Sat_Break2_In != "00:00:00")
                    {
                        satBreak3 = 45;
                        satBreak3Enabled = true;
                        //satDisplay += 70;
                    }
                }
            }

            if (timecard.Sun_Meal_In != "00:00:00")
            {
                //sunDisplay += 70;
                sunBreak1 = 45;
                sunBreak1Enabled = true;
                if (timecard.Sun_Break1_In != "00:00:00")
                {
                    //sunDisplay += 70;
                    sunBreak2 = 45;
                    sunBreak2Enabled = true;
                    if (timecard.Sun_Break2_In != "00:00:00")
                    {
                        sunBreak3 = 45;
                        sunBreak3Enabled = true;
                        //sunDisplay += 70;
                    }
                }
            }

            Days = new ObservableCollection<NewTimeOutCard> {
                new NewTimeOutCard() { Is24 = mon24, Break1Enabled = monBreak1Enabled, Break2Enabled = monBreak2Enabled, Break3Enabled = monBreak3Enabled, Break1 = monBreak1, Break2 = monBreak2, Break3 = monBreak3, Day = "Mon", Numero = monNum, Valor = timecard.Mon, TimeIn = TimeSpan.Parse(timecard.Mon_In), TimeOut= TimeSpan.Parse(timecard.Mon_Out), Break1Out=TimeSpan.Parse(timecard.Mon_Meal_In), Break1In=TimeSpan.Parse(timecard.Mon_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Mon_Break1_In), Break2In=TimeSpan.Parse(timecard.Mon_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Mon_Break2_In), Break3In=TimeSpan.Parse(timecard.Mon_Break2_Out), Nota = timecard.MonNote, DisplayInputs = monDisplay },
                new NewTimeOutCard() { Is24 = tue24, Break1Enabled = tueBreak1Enabled, Break2Enabled = tueBreak2Enabled, Break3Enabled = tueBreak3Enabled, Break1 = tueBreak1, Break2 = tueBreak2, Break3 = tueBreak3, Day = "Tue", Numero = tueNum, Valor = timecard.Tue, TimeIn = TimeSpan.Parse(timecard.Tue_In), TimeOut= TimeSpan.Parse(timecard.Tue_Out), Break1Out=TimeSpan.Parse(timecard.Tue_Meal_In), Break1In=TimeSpan.Parse(timecard.Tue_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Tue_Break1_In), Break2In=TimeSpan.Parse(timecard.Tue_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Tue_Break2_In), Break3In=TimeSpan.Parse(timecard.Tue_Break2_Out), Nota = timecard.TueNote, DisplayInputs = tueDisplay },
                new NewTimeOutCard() { Is24 = wed24, Break1Enabled = wedBreak1Enabled, Break2Enabled = wedBreak2Enabled, Break3Enabled = wedBreak3Enabled, Break1 = wedBreak1, Break2 = wedBreak2, Break3 = wedBreak3, Day = "Wed", Numero = wedNum, Valor = timecard.Wed, TimeIn = TimeSpan.Parse(timecard.Wed_In), TimeOut= TimeSpan.Parse(timecard.Wed_Out), Break1Out=TimeSpan.Parse(timecard.Wed_Meal_In), Break1In=TimeSpan.Parse(timecard.Wed_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Wed_Break1_In), Break2In=TimeSpan.Parse(timecard.Wed_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Wed_Break2_In), Break3In=TimeSpan.Parse(timecard.Wed_Break2_Out), Nota = timecard.WedNote, DisplayInputs = wedDisplay },
                new NewTimeOutCard() { Is24 = thu24, Break1Enabled = thuBreak1Enabled, Break2Enabled = thuBreak2Enabled, Break3Enabled = thuBreak3Enabled, Break1 = thuBreak1, Break2 = thuBreak2, Break3 = thuBreak3, Day = "Thu", Numero = thuNum, Valor = timecard.Thu, TimeIn = TimeSpan.Parse(timecard.Thu_In), TimeOut= TimeSpan.Parse(timecard.Thu_Out), Break1Out=TimeSpan.Parse(timecard.Thu_Meal_In), Break1In=TimeSpan.Parse(timecard.Thu_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Thu_Break1_In), Break2In=TimeSpan.Parse(timecard.Thu_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Thu_Break2_In), Break3In=TimeSpan.Parse(timecard.Thu_Break2_Out), Nota = timecard.ThuNote, DisplayInputs = thuDisplay },
                new NewTimeOutCard() { Is24 = fri24, Break1Enabled = friBreak1Enabled, Break2Enabled = friBreak2Enabled, Break3Enabled = friBreak3Enabled, Break1 = friBreak1, Break2 = friBreak2, Break3 = friBreak3, Day = "Fri", Numero = friNum, Valor = timecard.Fri, TimeIn = TimeSpan.Parse(timecard.Fri_In), TimeOut= TimeSpan.Parse(timecard.Fri_Out), Break1Out=TimeSpan.Parse(timecard.Fri_Meal_In), Break1In=TimeSpan.Parse(timecard.Fri_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Fri_Break1_In), Break2In=TimeSpan.Parse(timecard.Fri_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Fri_Break2_In), Break3In=TimeSpan.Parse(timecard.Fri_Break2_Out), Nota = timecard.FriNote, DisplayInputs = friDisplay },
                new NewTimeOutCard() { Is24 = sat24, Break1Enabled = satBreak1Enabled, Break2Enabled = satBreak2Enabled, Break3Enabled = satBreak3Enabled, Break1 = satBreak1, Break2 = satBreak2, Break3 = satBreak3, Day = "Sat", Numero = satNum, Valor = timecard.Sat, TimeIn = TimeSpan.Parse(timecard.Sat_In), TimeOut= TimeSpan.Parse(timecard.Sat_Out), Break1Out=TimeSpan.Parse(timecard.Sat_Meal_In), Break1In=TimeSpan.Parse(timecard.Sat_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Sat_Break1_In), Break2In=TimeSpan.Parse(timecard.Sat_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Sat_Break2_In), Break3In=TimeSpan.Parse(timecard.Sat_Break2_Out), Nota = timecard.SatNote, DisplayInputs = satDisplay },
                new NewTimeOutCard() { Is24 = sun24, Break1Enabled = sunBreak1Enabled, Break2Enabled = sunBreak2Enabled, Break3Enabled = sunBreak3Enabled, Break1 = sunBreak1, Break2 = sunBreak2, Break3 = sunBreak3, Day = "Sun", Numero = fecha.Day, Valor = timecard.Sun, TimeIn = TimeSpan.Parse(timecard.Sun_In), TimeOut= TimeSpan.Parse(timecard.Sun_Out), Break1Out=TimeSpan.Parse(timecard.Sun_Meal_In), Break1In=TimeSpan.Parse(timecard.Sun_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Sun_Break1_In), Break2In=TimeSpan.Parse(timecard.Sun_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Sun_Break2_In), Break3In=TimeSpan.Parse(timecard.Sun_Break2_Out), Nota = timecard.SunNote, DisplayInputs = sunDisplay }
            };

            GetAssignments(usuarioFrom);

            Assignment_Name = timecard.Name;
            AssignmentName = timecard.Name;
            Mon_Hours = timecard.Mon;
            Tue_Hours = timecard.Tue;
            Wed_Hours = timecard.Wed;
            Thu_Hours = timecard.Thu;
            Fri_Hours = timecard.Fri;
            Sat_Hours = timecard.Sat;
            Sun_Hours = timecard.Sun;
            Total_Hours = timecard.suma;

            Mon_Note = timecard.MonNote;
            Tue_Note = timecard.TueNote;
            Wed_Note = timecard.WedNote;
            Thu_Note = timecard.ThuNote;
            Fri_Note = timecard.FriNote;
            Sat_Note = timecard.SatNote;
            Sun_Note = timecard.SunNote;

            Info = new ObservableCollection<Timecard> {
               timecard
            };
            GetActions(timecard);
            OpenDatePicker = new Command(async () => await OpenCalendar());
            MessagingCenter.Subscribe<IndividualTimeInOutViewModel, DateTime>(this, "UpdateFechaIndInOut", (sender, value) =>
            {
                FechaSeleccionada = value;
            });
        }
        public async Task OpenCalendar()
        {
            HasPropertyValueChanged = true;
            await Application.Current.MainPage.Navigation.PushPopupAsync(new Calendario(usuario, FechaSeleccionada, 2, timecard));
            HasPropertyValueChanged = false;
        }
        public void GetAssignments(User usuario)
        {
            var Assignments_List = usuario.Assignments;
            foreach (Assignment assignment_item in Assignments_List)
            {
                Assignments.Add(assignment_item.Name);
            }
        }
        public void GetActions(Timecard eu_timecard)
        {
            Actions = new ObservableCollection<Actions>();
            Actions.Add(new Actions { ActionName = "Saved Date:", ActionDate = eu_timecard.CreatedDate });
            Actions.Add(new Actions { ActionName = "Submit Date:", ActionDate = eu_timecard.SubmitDate });
            Actions.Add(new Actions { ActionName = "Last Edit Date:", ActionDate = eu_timecard.LEditDate });
        }
        public async void SubmitTimecard()
        {
            var ID = timecard.ID;
            HasPropertyValueChanged = true;
            HttpClient client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("timecard_id", ID.ToString()),
                new KeyValuePair<string, string>("usuario", User.Id.ToString())
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SubmitTimecard.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Timecard Submitted!")
                {
                    HasPropertyValueChanged = false;
                    SubmittedCard = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage(responseData));
                }
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
            }
        }
        public async void UpdateTimecard()
        {
            var iD = timecard.ID;
            var AssignmentID = timecard.AssignmentID;
            HasPropertyValueChanged = true;
            var temp = Days;
            Days = null;
            Days = temp;
            HttpClient client = new HttpClient();
            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", User.Id.ToString()),
                new KeyValuePair<string, string>("Update", "1"),
                new KeyValuePair<string, string>("Timecard", iD.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentID.ToString()),
                new KeyValuePair<string, string>("AssignmentName", AssignmentName),
                new KeyValuePair<string, string>("date", FechaSeleccionada.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/UpdateInOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Timecard Updated!")
                {
                    LoadTimecard(User.Id, iD);
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage(responseData));
                }
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
            }
            HasPropertyValueChanged = false;
        }
        public async void LoadTimecard(int usuario, int timecard)
        {
            HasPropertyValueChanged = true;
            var client = new HttpClient();
            var URL = "https://bepc.backnetwork.net/BEPCINC/api/getTimecard.php?usuario=" + usuario + "&timecard=" + timecard;
            var result = await client.GetAsync(URL);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                var Timecard = JsonConvert.DeserializeObject<List<Models.Timecard>>(responseData);
                Info = null;
                Info = new ObservableCollection<Timecard>(Timecard);
                int monNum = FechaSeleccionada.AddDays(-6).Day;
                int tueNum = FechaSeleccionada.AddDays(-5).Day;
                int wedNum = FechaSeleccionada.AddDays(-4).Day;
                int thuNum = FechaSeleccionada.AddDays(-3).Day;
                int friNum = FechaSeleccionada.AddDays(-2).Day;
                int satNum = FechaSeleccionada.AddDays(-1).Day;
                int sunNum = FechaSeleccionada.Day;

                int monDisplay = 0;
                int monBreak1 = 0;
                int monBreak2 = 0;
                int monBreak3 = 0;
                int tueDisplay = 0;
                int tueBreak1 = 0;
                int tueBreak2 = 0;
                int tueBreak3 = 0;
                int wedDisplay = 0;
                int wedBreak1 = 0;
                int wedBreak2 = 0;
                int wedBreak3 = 0;
                int thuDisplay = 0;
                int thuBreak1 = 0;
                int thuBreak2 = 0;
                int thuBreak3 = 0;
                int friDisplay = 0;
                int friBreak1 = 0;
                int friBreak2 = 0;
                int friBreak3 = 0;
                int satDisplay = 0;
                int satBreak1 = 0;
                int satBreak2 = 0;
                int satBreak3 = 0;
                int sunDisplay = 0;
                int sunBreak1 = 0;
                int sunBreak2 = 0;
                int sunBreak3 = 0;

                if (Timecard[0].Mon_Meal_In != "00:00:00")
                {
                    //monDisplay += 70;
                    monBreak1 = 35;
                    if (Timecard[0].Mon_Break1_In != "00:00:00")
                    {
                        //monDisplay += 70;
                        monBreak2 = 35;
                        if (Timecard[0].Mon_Break2_In != "00:00:00")
                        {
                            monBreak3 = 35;
                            //monDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Tue_Meal_In != "00:00:00")
                {
                    //tueDisplay += 70;
                    tueBreak1 = 35;
                    if (Timecard[0].Tue_Break1_In != "00:00:00")
                    {
                        //tueDisplay += 70;
                        tueBreak2 = 35;
                        if (Timecard[0].Tue_Break2_In != "00:00:00")
                        {
                            tueBreak3 = 35;
                            //tueDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Wed_Meal_In != "00:00:00")
                {
                    //wedDisplay += 70;
                    wedBreak1 = 35;
                    if (Timecard[0].Wed_Break1_In != "00:00:00")
                    {
                        //wedDisplay += 70;
                        wedBreak2 = 35;
                        if (Timecard[0].Wed_Break2_In != "00:00:00")
                        {
                            wedBreak3 = 35;
                            //wedDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Thu_Meal_In != "00:00:00")
                {
                    //thuDisplay += 70;
                    thuBreak1 = 35;
                    if (Timecard[0].Thu_Break1_In != "00:00:00")
                    {
                        //thuDisplay += 70;
                        thuBreak2 = 35;
                        if (Timecard[0].Thu_Break2_In != "00:00:00")
                        {
                            thuBreak3 = 35;
                            //thuDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Fri_Meal_In != "00:00:00")
                {
                    //friDisplay += 70;
                    friBreak1 = 35;
                    if (Timecard[0].Fri_Break1_In != "00:00:00")
                    {
                        //friDisplay += 70;
                        friBreak2 = 35;
                        if (Timecard[0].Fri_Break2_In != "00:00:00")
                        {
                            friBreak3 = 35;
                            //friDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Sat_Meal_In != "00:00:00")
                {
                    //satDisplay += 70;
                    satBreak1 = 35;
                    if (Timecard[0].Sat_Break1_In != "00:00:00")
                    {
                        //satDisplay += 70;
                        satBreak2 = 35;
                        if (Timecard[0].Sat_Break2_In != "00:00:00")
                        {
                            satBreak3 = 35;
                            //satDisplay += 70;
                        }
                    }
                }

                if (Timecard[0].Sun_Meal_In != "00:00:00")
                {
                    //sunDisplay += 70;
                    sunBreak1 = 35;
                    if (Timecard[0].Sun_Break1_In != "00:00:00")
                    {
                        //sunDisplay += 70;
                        sunBreak2 = 35;
                        if (Timecard[0].Sun_Break2_In != "00:00:00")
                        {
                            sunBreak3 = 35;
                            //sunDisplay += 70;
                        }
                    }
                }

                var temp = new ObservableCollection<NewTimeOutCard> {
                    new NewTimeOutCard() { Break1 = monBreak1, Break2 = monBreak2, Break3 = monBreak3, Day = "Mon", Numero = monNum, Valor = Timecard[0].Mon, TimeIn = TimeSpan.Parse(Timecard[0].Mon_In), TimeOut= TimeSpan.Parse(Timecard[0].Mon_Out), Break1Out=TimeSpan.Parse(Timecard[0].Mon_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Mon_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Mon_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Mon_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Mon_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Mon_Break2_Out), Nota = Timecard[0].MonNote, DisplayInputs = monDisplay },
                    new NewTimeOutCard() { Break1 = tueBreak1, Break2 = tueBreak2, Break3 = tueBreak3, Day = "Tue", Numero = tueNum, Valor = Timecard[0].Tue, TimeIn = TimeSpan.Parse(Timecard[0].Tue_In), TimeOut= TimeSpan.Parse(Timecard[0].Tue_Out), Break1Out=TimeSpan.Parse(Timecard[0].Tue_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Tue_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Tue_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Tue_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Tue_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Tue_Break2_Out), Nota = Timecard[0].TueNote, DisplayInputs = tueDisplay },
                    new NewTimeOutCard() { Break1 = wedBreak1, Break2 = wedBreak2, Break3 = wedBreak3, Day = "Wed", Numero = wedNum, Valor = Timecard[0].Wed, TimeIn = TimeSpan.Parse(Timecard[0].Wed_In), TimeOut= TimeSpan.Parse(Timecard[0].Wed_Out), Break1Out=TimeSpan.Parse(Timecard[0].Wed_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Wed_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Wed_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Wed_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Wed_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Wed_Break2_Out), Nota = Timecard[0].WedNote, DisplayInputs = wedDisplay },
                    new NewTimeOutCard() { Break1 = thuBreak1, Break2 = thuBreak2, Break3 = thuBreak3, Day = "Thu", Numero = thuNum, Valor = Timecard[0].Thu, TimeIn = TimeSpan.Parse(Timecard[0].Thu_In), TimeOut= TimeSpan.Parse(Timecard[0].Thu_Out), Break1Out=TimeSpan.Parse(Timecard[0].Thu_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Thu_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Thu_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Thu_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Thu_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Thu_Break2_Out), Nota = Timecard[0].ThuNote, DisplayInputs = thuDisplay },
                    new NewTimeOutCard() { Break1 = friBreak1, Break2 = friBreak2, Break3 = friBreak3, Day = "Fri", Numero = friNum, Valor = Timecard[0].Fri, TimeIn = TimeSpan.Parse(Timecard[0].Fri_In), TimeOut= TimeSpan.Parse(Timecard[0].Fri_Out), Break1Out=TimeSpan.Parse(Timecard[0].Fri_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Fri_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Fri_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Fri_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Fri_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Fri_Break2_Out), Nota = Timecard[0].FriNote, DisplayInputs = friDisplay },
                    new NewTimeOutCard() { Break1 = satBreak1, Break2 = satBreak2, Break3 = satBreak3, Day = "Sat", Numero = satNum, Valor = Timecard[0].Sat, TimeIn = TimeSpan.Parse(Timecard[0].Sat_In), TimeOut= TimeSpan.Parse(Timecard[0].Sat_Out), Break1Out=TimeSpan.Parse(Timecard[0].Sat_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Sat_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Sat_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Sat_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Sat_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Sat_Break2_Out), Nota = Timecard[0].SatNote, DisplayInputs = satDisplay },
                    new NewTimeOutCard() { Break1 = sunBreak1, Break2 = sunBreak2, Break3 = sunBreak3, Day = "Sun", Numero = sunNum, Valor = Timecard[0].Sun, TimeIn = TimeSpan.Parse(Timecard[0].Sun_In), TimeOut= TimeSpan.Parse(Timecard[0].Sun_Out), Break1Out=TimeSpan.Parse(Timecard[0].Sun_Meal_In), Break1In=TimeSpan.Parse(Timecard[0].Sun_Meal_Out), Break2Out=TimeSpan.Parse(Timecard[0].Sun_Break1_In), Break2In=TimeSpan.Parse(Timecard[0].Sun_Break1_Out), Break3Out=TimeSpan.Parse(Timecard[0].Sun_Break2_In), Break3In=TimeSpan.Parse(Timecard[0].Sun_Break2_Out), Nota = Timecard[0].SunNote, DisplayInputs = sunDisplay }
                };
                GetActions(Timecard[0]);
                Assignment_Name = Timecard[0].Name;
                AssignmentName = Timecard[0].Name;
                Mon_Hours = Timecard[0].Mon;
                Tue_Hours = Timecard[0].Tue;
                Wed_Hours = Timecard[0].Wed;
                Thu_Hours = Timecard[0].Thu;
                Fri_Hours = Timecard[0].Fri;
                Sat_Hours = Timecard[0].Sat;
                Sun_Hours = Timecard[0].Sun;
                Total_Hours = Timecard[0].suma;

                Mon_Note = Timecard[0].MonNote;
                Tue_Note = Timecard[0].TueNote;
                Wed_Note = Timecard[0].WedNote;
                Thu_Note = Timecard[0].ThuNote;
                Fri_Note = Timecard[0].FriNote;
                Sat_Note = Timecard[0].SatNote;
                Sun_Note = Timecard[0].SunNote;

            }
        }
        public void HideOrShowInputs(NewTimeOutCard day)
        {
            if (day != null)
            {
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 255;
                    if (day.Break1Enabled == true)
                    {
                        day.DisplayInputs += 90;
                        day.Break1 = 45;
                    }
                    if (day.Break2Enabled == true)
                    {
                        day.DisplayInputs += 90;
                        day.Break2 = 45;
                    }
                    if (day.Break3Enabled == true)
                    {
                        day.DisplayInputs += 90;
                        day.Break3 = 45;
                    }
                    day.InputHeight = 45;
                    day.bgColor = "#ebebeb";
                }
                else
                {
                    day.Break1 = 0;
                    day.Break2 = 0;
                    day.Break3 = 0;
                    day.DisplayInputs = 0;
                    day.InputHeight = 0;
                    day.bgColor = "White";
                }
            }
        }
        private void UpdateDays(NewTimeOutCard day)
        {
            try
            {
                var index = Days.IndexOf(day);
                if (index != -1) { 
                    Days.Remove(day);
                    Days.Insert(index, day);
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("BIG ERRRRROOOOOOOOR: {0}", e.Source);
            }
        }
        public void AgregarBreak(NewTimeOutCard day)
        {
            if (day.Break1 == 0)
            {
                day.Break1 = 45;
                day.DisplayInputs += 90;
                day.Break1Enabled = true;
            }
            else
            {
                if (day.Break2 == 0)
                {
                    day.Break2 = 45;
                    day.DisplayInputs += 90;
                    day.Break2Enabled = true;
                }
                else if (day.Break3 == 0)
                {
                    day.Break3 = 45;
                    day.DisplayInputs += 90;
                    day.Break3Enabled = true;
                }
            }
        }
        public void QuitarBreak(NewTimeOutCard day)
        {
            if (day.Break3 == 45)
            {
                day.Break3 = 0;
                day.DisplayInputs -= 90;
                day.Break3Enabled = false;
            }
            else
            {
                if (day.Break2 == 45)
                {
                    day.Break2 = 0;
                    day.DisplayInputs -= 90;
                    day.Break2Enabled = false;
                }
                else if (day.Break1 == 45)
                {
                    day.Break1 = 0;
                    day.DisplayInputs -= 90;
                    day.Break1Enabled = false;
                }
            }
            //UpdateDays(_oldDay);
        }
        public async void DeleteTimecard()
        {
            var iD = timecard.ID;
            HasPropertyValueChanged = true;
            HttpClient client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", User.Id.ToString()),
                new KeyValuePair<string, string>("Delete", "1"),
                new KeyValuePair<string, string>("Timecard", iD.ToString()),
                new KeyValuePair<string, string>("AssignmentName", AssignmentName),
                new KeyValuePair<string, string>("date", FechaSeleccionada.Date.ToString("MM/dd/yyyy"))
            });

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/DeleteTimecard.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Deleted!")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
                    await Application.Current.MainPage.Navigation.PopAsync();
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
            HasPropertyValueChanged = false;
        }
    }
}
