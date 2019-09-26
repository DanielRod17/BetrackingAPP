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
    public class NewTimecardViewModel : BaseViewModel
    {
        public Command SaveTimecard { get; set; }
        public Command SubmitTimecard { get; set; }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().Contains(text.ToLower()))
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
        public Timecard Timecard;
        //private NewTimecardNormal _oldDay;
        private DateTime _fecha_send { get; set; }
        public DateTime Fecha_Send
        {
            get
            {
                return _fecha_send;
            }
            set
            {
                _fecha_send = value;
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
        public string Fecha_timecard { get; set; }
        ObservableCollection<string> assignments = new ObservableCollection<string>();
        public ObservableCollection<string> Assignments { get { return assignments; } }
        public ObservableCollection<NewTimecardNormal> Days { get; set; }
        private NewTimecardNormal _selectedItem;
        public NewTimecardNormal ShowDay
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    //GoToDay(_selectedItem);
                }
            }
        }
        public void GoToDay(NewTimecardNormal eu_day)
        {
            if (eu_day != null)
            {
                //await App.Current.MainPage.Navigation.PushAsync(new CardDay(eu_day));
            }
        }
        public NewTimecardViewModel(User usuarioFrom, List<Timecard> timecardFrom, System.DateTime fecha)
        {
            IsLoading = true;
            Usuario = usuarioFrom;
            Fecha_Send = fecha;
            int monNum = fecha.AddDays(-6).Day;
            int tueNum = fecha.AddDays(-5).Day;
            int wedNum = fecha.AddDays(-4).Day;
            int thuNum = fecha.AddDays(-3).Day;
            int friNum = fecha.AddDays(-2).Day;
            int satNum = fecha.AddDays(-1).Day;
            Fecha_timecard = "NEW TIMECARD\n" + fecha.Date.ToString("MM/dd/yyyy");
            Days = new ObservableCollection<NewTimecardNormal> {
                new NewTimecardNormal() { Day = "Mon", Numero = monNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Tue", Numero = tueNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Wed", Numero = wedNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Thu", Numero = thuNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Fri", Numero = friNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Sat", Numero = satNum, Nota = "", DisplayInputs = 0, bgColor="White" },
                new NewTimecardNormal() { Day = "Sun", Numero = fecha.Day, Nota = "", DisplayInputs = 0, bgColor="White" }
            };
            GetAssignments(usuarioFrom);

            SaveTimecard = new Command(async () => await GuardarTimecard());
            SubmitTimecard = new Command(async () => await SometerTimecard());
            IsLoading = false;
        }

        public async Task GuardarTimecard()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveMX.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Saved!")
                {
                    AssignmentName = "";

                    foreach (NewTimecardNormal dia in Days)
                    {
                        dia.Valor = 0.00m;
                        dia.Nota = "";
                    }

                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, responseData));
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
            IsLoading = false;
        }

        public async Task SometerTimecard()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("Submit", "1"),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveMX.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Submitted!")
                {
                    AssignmentName = "";

                    foreach (NewTimecardNormal dia in Days)
                    {
                        dia.Valor = 0.00m;
                        dia.Nota = "";
                    }

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

        private void UpdateDays(NewTimecardNormal day)
        {
            var index = Days.IndexOf(day);
            Days.Remove(day);
            Days.Insert(index, day);
        }
    }
}
