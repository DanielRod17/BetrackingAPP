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
    public class IndividualCardViewModel : BaseViewModel, INotifyPropertyChanged
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
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();
        private DateTime _dateSearch = DateTime.Today;
        ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments { get { return assignments; } }
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
        public ObservableCollection<Timecard> Info {
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
        public ObservableCollection<Actions> Actions {
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
        private ObservableCollection<NewTimecardNormal> _dias;
        public ObservableCollection<NewTimecardNormal> Days
        {
            get
            {
                return _dias;
            }
            set
            {
                _dias = value;
                OnPropertyChanged();
            }
        }
        public IndividualCardViewModel(User usuarioFrom, Timecard timecardFrom, DateTime _dateSearch)
        {
            FechaSeleccionada = _dateSearch;
            HasPropertyValueChanged = true;
            usuario = usuarioFrom;
            User = usuario;
            if (timecardFrom.Submitted == 0)
            {
                SubmittedCard = true;
            }
            else
            {
                SubmittedCard = false;
            }
            timecard = timecardFrom;
            var fecha = _dateSearch;
            int monNum = fecha.AddDays(-6).Day;
            int tueNum = fecha.AddDays(-5).Day;
            int wedNum = fecha.AddDays(-4).Day;
            int thuNum = fecha.AddDays(-3).Day;
            int friNum = fecha.AddDays(-2).Day;
            int satNum = fecha.AddDays(-1).Day;
            int sunNum = fecha.Day;
            Days = new ObservableCollection<NewTimecardNormal> {
                new NewTimecardNormal() { Day = "Mon", Numero = monNum, Valor = timecard.Mon, Nota = timecard.MonNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Tue", Numero = tueNum, Valor = timecard.Tue, Nota = timecard.TueNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Wed", Numero = wedNum, Valor = timecard.Wed, Nota = timecard.WedNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Thu", Numero = thuNum, Valor = timecard.Thu, Nota = timecard.ThuNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Fri", Numero = friNum, Valor = timecard.Fri, Nota = timecard.FriNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Sat", Numero = satNum, Valor = timecard.Sat, Nota = timecard.SatNote, DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Sun", Numero = fecha.Day, Valor = timecard.Sun, Nota = timecard.SunNote, DisplayInputs = 0, bgColor="White" }
            };

            Assignment_Name = timecard.Name;
            AssignmentName = timecard.Name;

            GetAssignments(usuarioFrom);

            Mon_Hours = timecard.Mon.ToString();
            Tue_Hours = timecard.Tue.ToString();
            Wed_Hours = timecard.Wed.ToString();
            Thu_Hours = timecard.Thu.ToString();
            Fri_Hours = timecard.Fri.ToString();
            Sat_Hours = timecard.Sat.ToString();
            Sun_Hours = timecard.Sun.ToString();
            Total_Hours = timecard.suma.ToString();

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
                    LoadTimecard(User.Id, ID);
                    SubmittedCard = false;
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
            HasPropertyValueChanged = false;
        }

        public async void UpdateTimecard(int iD, int AssignmentID)
        {
            HasPropertyValueChanged = true;
            var temp = Days;
            Days = new ObservableCollection<NewTimecardNormal>();
            Days = temp;
            HttpClient client = new HttpClient();
            var yeison = JsonConvert.SerializeObject(_dias);
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

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/UpdateMX.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
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
                Days = new ObservableCollection<NewTimecardNormal>();
                Info = new ObservableCollection<Timecard>(Timecard);
                int monNum = FechaSeleccionada.AddDays(-6).Day;
                int tueNum = FechaSeleccionada.AddDays(-5).Day;
                int wedNum = FechaSeleccionada.AddDays(-4).Day;
                int thuNum = FechaSeleccionada.AddDays(-3).Day;
                int friNum = FechaSeleccionada.AddDays(-2).Day;
                int satNum = FechaSeleccionada.AddDays(-1).Day;
                int sunNum = FechaSeleccionada.Day;
                var temp = new ObservableCollection<NewTimecardNormal> {
                    new NewTimecardNormal() { Day = "Mon", Numero = monNum, Valor = Timecard[0].Mon, Nota = Timecard[0].MonNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Tue", Numero = tueNum, Valor = Timecard[0].Tue, Nota = Timecard[0].TueNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Wed", Numero = wedNum, Valor = Timecard[0].Wed, Nota = Timecard[0].WedNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Thu", Numero = thuNum, Valor = Timecard[0].Thu, Nota = Timecard[0].ThuNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Fri", Numero = friNum, Valor = Timecard[0].Fri, Nota = Timecard[0].FriNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Sat", Numero = satNum, Valor = Timecard[0].Sat, Nota = Timecard[0].SatNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Sun", Numero = sunNum, Valor = Timecard[0].Sun, Nota = Timecard[0].SunNote, DisplayInputs = 0 },
                    new NewTimecardNormal() { Day = "Sun", Numero = sunNum, Valor = Timecard[0].Sun, Nota = Timecard[0].SunNote, DisplayInputs = 0 },
                };
                
                GetActions(Timecard[0]);
                temp.RemoveAt(7);
                Days = temp;
            }
            HasPropertyValueChanged = false;
        }
        public void HideOrShowInputs(NewTimecardNormal day)
        {
            if (day != null)
            {
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 100;
                    day.DisplayInputsNotes = 35;
                    day.bgColor = "#F4F4F4";
                }
                else
                {
                    day.DisplayInputs = 0;
                    day.DisplayInputsNotes = 0;
                    day.bgColor = "White";
                }
            }
        }
    }
}
