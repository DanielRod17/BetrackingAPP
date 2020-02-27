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
        public Command OpenDatePicker { get; set; }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();
        private DateTime _dateSearch = DateTime.Today;
        ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments { get { return assignments; } }
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
        private string _mon_hours { get; set; }
        private string _tue_hours { get; set; }
        private string _wed_hours { get; set; }
        private string _thu_hours { get; set; }
        private string _fri_hours { get; set; }
        private string _sat_hours { get; set; }
        private string _sun_hours { get; set; }
        public string Mon_Hours 
        {
            get
            {
                return _mon_hours;
            }
            set
            {
                _mon_hours = value;
                OnPropertyChanged();
            }
        }
        public string Tue_Hours
        {
            get
            {
                return _tue_hours;
            }
            set
            {
                _tue_hours = value;
                OnPropertyChanged();
            }
        }
        public string Wed_Hours
        {
            get
            {
                return _wed_hours;
            }
            set
            {
                _wed_hours = value;
                OnPropertyChanged();
            }
        }
        public string Thu_Hours
        {
            get
            {
                return _thu_hours;
            }
            set
            {
                _thu_hours = value;
                OnPropertyChanged();
            }
        }
        public string Fri_Hours
        {
            get
            {
                return _fri_hours;
            }
            set
            {
                _fri_hours = value;
                OnPropertyChanged();
            }
        }
        public string Sat_Hours
        {
            get
            {
                return _sat_hours;
            }
            set
            {
                _sat_hours = value;
                OnPropertyChanged();
            }
        }
        public string Sun_Hours
        {
            get
            {
                return _sun_hours;
            }
            set
            {
                _sun_hours = value;
                OnPropertyChanged();
            }
        }
        private string _mon_Note { get; set; }
        private string _tue_Note { get; set; }
        private string _wed_Note { get; set; }
        private string _thu_Note { get; set; }
        private string _fri_Note { get; set; }
        private string _sat_Note { get; set; }
        private string _sun_Note { get; set; }
        public string Mon_Note
        {
            get
            {
                return _mon_Note;
            }
            set
            {
                _mon_Note = value;
                OnPropertyChanged();
            }
        }
        public string Tue_Note
        {
            get
            {
                return _tue_Note;
            }
            set
            {
                _tue_Note = value;
                OnPropertyChanged();
            }
        }
        public string Wed_Note
        {
            get
            {
                return _wed_Note;
            }
            set
            {
                _wed_Note = value;
                OnPropertyChanged();
            }
        }
        public string Thu_Note
        {
            get
            {
                return _thu_Note;
            }
            set
            {
                _thu_Note = value;
                OnPropertyChanged();
            }
        }
        public string Fri_Note
        {
            get
            {
                return _fri_Note;
            }
            set
            {
                _fri_Note = value;
                OnPropertyChanged();
            }
        }
        public string Sat_Note
        {
            get
            {
                return _sat_Note;
            }
            set
            {
                _sat_Note = value;
                OnPropertyChanged();
            }
        }
        public string Sun_Note
        {
            get
            {
                return _sun_Note;
            }
            set
            {
                _sun_Note = value;
                OnPropertyChanged();
            }
        }
        public string Assignment_Name { get; set; }
        public string Total_Hours { get; set; }
        public string Timecard_ID { get; set; }
        public string monday_info { get; set; }

        private User usuario;
        private Timecard _timecard { get; set; }
        public Timecard timecard{
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
            Timecard_ID = timecard.TimecardID;
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
            OpenDatePicker = new Command(async () => await OpenCalendar());
            MessagingCenter.Subscribe<IndividualCardViewModel, DateTime>(this, "UpdateFechaInd", (sender, value) =>
            {
                FechaSeleccionada = value;
            });
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
        public async Task OpenCalendar()
        {
            HasPropertyValueChanged = true;
            await Application.Current.MainPage.Navigation.PushPopupAsync(new Calendario(usuario, FechaSeleccionada, 1, timecard));
            HasPropertyValueChanged = false;
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
                    LoadTimecard(User.Id, ID);
                    SubmittedCard = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
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
        public async void UpdateTimecard()
        {
            HasPropertyValueChanged = true;
            var iD = timecard.ID;
            var AssignmentID = timecard.AssignmentID;
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
            });

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

                Mon_Hours = Timecard[0].Mon.ToString();
                Tue_Hours = Timecard[0].Tue.ToString();
                Wed_Hours = Timecard[0].Wed.ToString();
                Thu_Hours = Timecard[0].Thu.ToString();
                Fri_Hours = Timecard[0].Fri.ToString();
                Sat_Hours = Timecard[0].Sat.ToString();
                Sun_Hours = Timecard[0].Sun.ToString();
                Total_Hours = Timecard[0].suma.ToString();

                Mon_Note = Timecard[0].MonNote;
                Tue_Note = Timecard[0].TueNote;
                Wed_Note = Timecard[0].WedNote;
                Thu_Note = Timecard[0].ThuNote;
                Fri_Note = Timecard[0].FriNote;
                Sat_Note = Timecard[0].SatNote;
                Sun_Note = Timecard[0].SunNote;

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
                    day.bgColor = "#EBEBEB";
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
