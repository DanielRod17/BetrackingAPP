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
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();

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
        public ObservableCollection<Timecard> Info { get; set; }
        ObservableCollection<Actions> acciones = new ObservableCollection<Actions>();
        public ObservableCollection<Actions> Actions { get { return acciones; } }

        ObservableCollection<NewTimecardNormal> _dias;
        public ObservableCollection<NewTimecardNormal> Days { get { return _dias;  } set { _dias = value; OnPropertyChanged("Days"); } }

        public IndividualCardViewModel(User usuarioFrom, Timecard timecardFrom, DateTime _dateSearch)
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
            Days = new ObservableCollection<NewTimecardNormal> {
                new NewTimecardNormal() { Day = "Mon", Numero = monNum, Valor = timecard.Mon, Nota = timecard.MonNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Tue", Numero = tueNum, Valor = timecard.Tue, Nota = timecard.TueNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Wed", Numero = wedNum, Valor = timecard.Wed, Nota = timecard.WedNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Thu", Numero = thuNum, Valor = timecard.Thu, Nota = timecard.ThuNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Fri", Numero = friNum, Valor = timecard.Fri, Nota = timecard.FriNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Sat", Numero = satNum, Valor = timecard.Sat, Nota = timecard.SatNote, DisplayInputs = 35 },
                new NewTimecardNormal() { Day = "Sun", Numero = fecha.Day, Valor = timecard.Sun, Nota = timecard.SunNote, DisplayInputs = 35 }
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
                //new Timecard() { Mon = timecard.Mon, Tue = timecard.Tue, Val = 0.00m, Nota = "", DisplayInputs = 0 },
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

            acciones.Add(new Actions { ActionName = "Saved Date:", ActionDate = eu_timecard.CreatedDate });
            acciones.Add(new Actions { ActionName = "Submit Date:", ActionDate = eu_timecard.SubmitDate });
            acciones.Add(new Actions { ActionName = "Las Edit Date:", ActionDate = eu_timecard.LEditDate });
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
                new KeyValuePair<string, string>("date", FechaSeleccionada.Date.ToString("g")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/UpdateMX.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                /*if (responseData == "Timecard Submitted!")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(User, responseData));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                }*/
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
        }
    }
}
