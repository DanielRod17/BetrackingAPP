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
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();

        private NewTimeOutCard _oldDay;
        public DateTime _dateSearch = DateTime.Today;
        ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments { get { return assignments; } }
        public DateTime FechaSeleccionada
        {
            set
            {
                OnPropertyChanged();

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
            }
            get
            {
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
                return _dateSearch;
            }
        }
        public User User { get; set; }

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
        public string Mon_Hours { get; set; }
        public string Tue_Hours { get; set; }
        public string Wed_Hours { get; set; }
        public string Thu_Hours { get; set; }
        public string Fri_Hours { get; set; }
        public string Sat_Hours { get; set; }
        public string Sun_Hours { get; set; }
        public string Mon_Note { get; set; }
        public string Tue_Note { get; set; }
        public string Wed_Note { get; set; }
        public string Thu_Note { get; set; }
        public string Fri_Note { get; set; }
        public string Sat_Note { get; set; }
        public string Sun_Note { get; set; }
        public string Assignment_Name { get; set; }
        public string Total_Hours { get; set; }

        public string monday_info { get; set; }

        private User usuario;
        private Timecard timecard;
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

        ObservableCollection<NewTimeOutCard> _dias;
        public ObservableCollection<NewTimeOutCard> Days { get { return _dias; } set { _dias = value; OnPropertyChanged(); } }

        public IndividualTimeInOutViewModel(User usuarioFrom, Timecard timecardFrom, DateTime _dateSearch)
        {

            usuario = usuarioFrom;
            User = usuario;
            timecard = timecardFrom;
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
            if (timecard.Mon != 0)
            {
                monDisplay = 215;
            }
            if (timecard.Tue != 0)
            {
                tueDisplay = 215;
            }
            if (timecard.Wed != 0)
            {
                wedDisplay = 215;
            }
            if (timecard.Thu != 0)
            {
                thuDisplay = 215;
            }
            if (timecard.Fri != 0)
            {
                friDisplay = 215;
            }
            if (timecard.Sat != 0)
            {
                satDisplay = 215;
            }
            if (timecard.Sun != 0)
            {
                sunDisplay = 215;
            }



            if (timecard.Mon_Meal_In != "00:00:00")
            {
                monDisplay += 70;
                monBreak1 = 35;
                if (timecard.Mon_Break1_In != "00:00:00")
                {
                    monDisplay += 70;
                    monBreak2 = 35;
                    if (timecard.Mon_Break2_In != "00:00:00")
                    {
                        monBreak3 = 35;
                        monDisplay += 70;
                    }
                }
            }

            if (timecard.Tue_Meal_In != "00:00:00")
            {
                tueDisplay += 70;
                tueBreak1 = 35;
                if (timecard.Tue_Break1_In != "00:00:00")
                {
                    tueDisplay += 70;
                    tueBreak2 = 35;
                    if (timecard.Tue_Break2_In != "00:00:00")
                    {
                        tueBreak3 = 35;
                        tueDisplay += 70;
                    }
                }
            }

            if (timecard.Wed_Meal_In != "00:00:00")
            {
                wedDisplay += 70;
                wedBreak1 = 35;
                if (timecard.Wed_Break1_In != "00:00:00")
                {
                    wedDisplay += 70;
                    wedBreak2 = 35;
                    if (timecard.Wed_Break2_In != "00:00:00")
                    {
                        wedBreak3 = 35;
                        wedDisplay += 70;
                    }
                }
            }

            if (timecard.Thu_Meal_In != "00:00:00")
            {
                thuDisplay += 70;
                thuBreak1 = 35;
                if (timecard.Thu_Break1_In != "00:00:00")
                {
                    thuDisplay += 70;
                    thuBreak2 = 35;
                    if (timecard.Thu_Break2_In != "00:00:00")
                    {
                        thuBreak3 = 35;
                        thuDisplay += 70;
                    }
                }
            }

            if (timecard.Fri_Meal_In != "00:00:00")
            {
                friDisplay += 70;
                friBreak1 = 35;
                if (timecard.Fri_Break1_In != "00:00:00")
                {
                    friDisplay += 70;
                    friBreak2 = 35;
                    if (timecard.Fri_Break2_In != "00:00:00")
                    {
                        friBreak3 = 35;
                        friDisplay += 70;
                    }
                }
            }

            if (timecard.Sat_Meal_In != "00:00:00")
            {
                satDisplay += 70;
                satBreak1 = 35;
                if (timecard.Sat_Break1_In != "00:00:00")
                {
                    satDisplay += 70;
                    satBreak2 = 35;
                    if (timecard.Sat_Break2_In != "00:00:00")
                    {
                        satBreak3 = 35;
                        satDisplay += 70;
                    }
                }
            }

            if (timecard.Sun_Meal_In != "00:00:00")
            {
                sunDisplay += 70;
                sunBreak1 = 35;
                if (timecard.Sun_Break1_In != "00:00:00")
                {
                    sunDisplay += 70;
                    sunBreak2 = 35;
                    if (timecard.Sun_Break2_In != "00:00:00")
                    {
                        sunBreak3 = 35;
                        sunDisplay += 70;
                    }
                }
            }

            Days = new ObservableCollection<NewTimeOutCard> {
                new NewTimeOutCard() { Break1 = monBreak1, Break2 = monBreak2, Break3 = monBreak3, Day = "Mon", Numero = monNum, Valor = timecard.Mon, TimeIn = TimeSpan.Parse(timecard.Mon_In), TimeOut= TimeSpan.Parse(timecard.Mon_Out), Nota = timecard.MonNote, DisplayInputs = monDisplay },
                new NewTimeOutCard() { Break1 = tueBreak1, Break2 = tueBreak2, Break3 = tueBreak3, Day = "Tue", Numero = tueNum, Valor = timecard.Tue, TimeIn = TimeSpan.Parse(timecard.Tue_In), TimeOut= TimeSpan.Parse(timecard.Tue_Out), Nota = timecard.TueNote, DisplayInputs = tueDisplay },
                new NewTimeOutCard() { Break1 = wedBreak1, Break2 = wedBreak2, Break3 = wedBreak3, Day = "Wed", Numero = wedNum, Valor = timecard.Wed, TimeIn = TimeSpan.Parse(timecard.Wed_In), TimeOut= TimeSpan.Parse(timecard.Wed_Out), Nota = timecard.WedNote, DisplayInputs = wedDisplay },
                new NewTimeOutCard() { Break1 = thuBreak1, Break2 = thuBreak2, Break3 = thuBreak3, Day = "Thu", Numero = thuNum, Valor = timecard.Thu, TimeIn = TimeSpan.Parse(timecard.Thu_In), TimeOut= TimeSpan.Parse(timecard.Thu_Out), Nota = timecard.ThuNote, DisplayInputs = thuDisplay },
                new NewTimeOutCard() { Break1 = friBreak1, Break2 = friBreak2, Break3 = friBreak3, Day = "Fri", Numero = friNum, Valor = timecard.Fri, TimeIn = TimeSpan.Parse(timecard.Fri_In), TimeOut= TimeSpan.Parse(timecard.Fri_Out), Nota = timecard.FriNote, DisplayInputs = friDisplay },
                new NewTimeOutCard() { Break1 = satBreak1, Break2 = satBreak2, Break3 = satBreak3, Day = "Sat", Numero = satNum, Valor = timecard.Sat, TimeIn = TimeSpan.Parse(timecard.Sat_In), TimeOut= TimeSpan.Parse(timecard.Sat_Out), Nota = timecard.SatNote, DisplayInputs = satDisplay },
                new NewTimeOutCard() { Break1 = sunBreak1, Break2 = sunBreak2, Break3 = sunBreak3, Day = "Sun", Numero = fecha.Day, Valor = timecard.Sun, TimeIn = TimeSpan.Parse(timecard.Sun_In), TimeOut= TimeSpan.Parse(timecard.Sun_Out), Nota = timecard.SunNote, DisplayInputs = sunDisplay }
            };

            Assignment_Name = timecard.Name;
            AssignmentName = timecard.Name;

            GetAssignments(usuarioFrom);

            Mon_Hours = "Monday: " + timecard.Mon;
            Tue_Hours = "Tuesday: " + timecard.Tue;
            Wed_Hours = "Wednesday: " + timecard.Wed;
            Thu_Hours = "Thursday: " + timecard.Thu;
            Fri_Hours = "Friday: " + timecard.Fri;
            Sat_Hours = "Saturday: " + timecard.Sat;
            Sun_Hours = "Sun: " + timecard.Sun;
            Total_Hours = "Total Hours: " + timecard.suma.ToString();

            Mon_Note = "Monday: " + timecard.MonNote;
            Tue_Note = "Tuesday: " + timecard.TueNote;
            Wed_Note = "Wednesday: " + timecard.WedNote;
            Thu_Note = "Thursday: " + timecard.ThuNote;
            Fri_Note = "Friday: " + timecard.FriNote;
            Sat_Note = "Saturday: " + timecard.SatNote;
            Sun_Note = "Sun: " + timecard.SunNote;
            monday_info = "ROFL";

            Info = new ObservableCollection<Timecard> {
               timecard
            };
            GetActions(timecard);
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

        public async void SubmitTimecard(int ID)
        {
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
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
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

        public async void UpdateTimecard(int iD, int AssignmentID)
        {
            var temp = Days;
            Days = new ObservableCollection<NewTimeOutCard>();
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
                new KeyValuePair<string, string>("date", FechaSeleccionada.Date.ToString("g")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/UpdateInOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Updated!")
                {
                    LoadTimecard(User.Id, iD);
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
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
                Days = new ObservableCollection<NewTimeOutCard>();
                Info = new ObservableCollection<Timecard>(Timecard);
                int monNum = FechaSeleccionada.AddDays(-6).Day;
                int tueNum = FechaSeleccionada.AddDays(-5).Day;
                int wedNum = FechaSeleccionada.AddDays(-4).Day;
                int thuNum = FechaSeleccionada.AddDays(-3).Day;
                int friNum = FechaSeleccionada.AddDays(-2).Day;
                int satNum = FechaSeleccionada.AddDays(-1).Day;
                int sunNum = FechaSeleccionada.Day;
                var temp = new ObservableCollection<NewTimeOutCard> {
                    new NewTimeOutCard() { Day = "Mon", Numero = monNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Tue", Numero = tueNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Wed", Numero = wedNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Thu", Numero = thuNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Fri", Numero = friNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Sat", Numero = satNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 },
                    new NewTimeOutCard() { Day = "Sun", Numero = sunNum, Nota = "", DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0 }
                };
                Days = null;
                GetActions(Timecard[0]);
                Days = temp;
            }
        }

        public void HideOrShowInputs(NewTimeOutCard day)
        {

            if (_oldDay == day)
            {
                // click twice on same item to hide it
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 225;
                }
                else
                {
                    day.DisplayInputs = 0;
                }
                UpdateDays(day);
            }
            else
            {
                if (_oldDay != null)
                {
                    // hide previous selected item
                    _oldDay.DisplayInputs = 0;
                    UpdateDays(_oldDay);
                }
                // show selected item
                day.DisplayInputs = 225;
                UpdateDays(day);
            }
            _oldDay = day;
        }

        private void UpdateDays(NewTimeOutCard day)
        {
            var index = Days.IndexOf(day);
            Days.Remove(day);
            Days.Insert(index, day);
        }

        public void AgregarBreak()
        {
            if (_oldDay.Break1 == 0)
            {
                _oldDay.Break1 = 35;
                _oldDay.DisplayInputs += 70;
            }
            else
            {
                if (_oldDay.Break2 == 0)
                {
                    _oldDay.Break2 = 35;
                    _oldDay.DisplayInputs += 70;
                }
                else if (_oldDay.Break3 == 0)
                {
                    _oldDay.Break3 = 35;
                    _oldDay.DisplayInputs += 70;
                }
            }
            UpdateDays(_oldDay);
        }

        public void QuitarBreak()
        {
            if (_oldDay.Break3 == 35)
            {
                _oldDay.Break3 = 0;
                _oldDay.DisplayInputs -= 70;
            }
            else
            {
                if (_oldDay.Break2 == 35)
                {
                    _oldDay.Break2 = 0;
                    _oldDay.DisplayInputs -= 70;
                }
                else if (_oldDay.Break1 == 35)
                {
                    _oldDay.Break1 = 0;
                    _oldDay.DisplayInputs -= 70;
                }
            }
            UpdateDays(_oldDay);
        }
    }
}
