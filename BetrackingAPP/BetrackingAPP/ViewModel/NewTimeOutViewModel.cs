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
    public class NewTimeOutViewModel : BaseViewModel
    {
        public Command SaveTimecard { get; set; }
        public Command SubmitTimecard { get; set; }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        .OrderBy(x => x)
        .ToList();

        public User Usuario;

        public Timecard Timecard;
        private NewTimeOutCard _oldDay;
        public DateTime Fecha_Send;
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

        public ObservableCollection<NewTimeOutCard> Days { get; set; }

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
        public NewTimeOutViewModel(User usuarioFrom, List<Timecard> timecardFrom, System.DateTime fecha)
        {
            Usuario = usuarioFrom;
            Fecha_Send = fecha;
            int monNum = fecha.AddDays(-6).Day;
            int tueNum = fecha.AddDays(-5).Day;
            int wedNum = fecha.AddDays(-4).Day;
            int thuNum = fecha.AddDays(-3).Day;
            int friNum = fecha.AddDays(-2).Day;
            int satNum = fecha.AddDays(-1).Day;
            Fecha_timecard = "TIME IN/OUT \n" + fecha.Date.ToString("MM/dd/yyyy");
            Days = new ObservableCollection<NewTimeOutCard> {
                new NewTimeOutCard() { Day = "Mon", Numero = monNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Tue", Numero = tueNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Wed", Numero = wedNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Thu", Numero = thuNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Fri", Numero = friNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Sat", Numero = satNum, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" },
                new NewTimeOutCard() { Day = "Sun", Numero = fecha.Day, Nota = "", InputHeight = 0, DisplayInputs = 0, Break1 = 0, Break2 = 0, Break3 = 0, bgColor="White" }
            };
            GetAssignments(usuarioFrom);

            SaveTimecard = new Command(async () => await GuardarTimecard());
            SubmitTimecard = new Command(async () => await SometerTimecard());
        }

        public async void Test()
        {
            await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
        }

        public async Task GuardarTimecard()
        {
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveTimeOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Saved!")
                {
                    AssignmentName = "";

                    foreach (NewTimeOutCard dia in Days)
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
        }

        public async Task SometerTimecard()
        {
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Submit", "1"),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveTimeOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Timecard Submitted!")
                {
                    AssignmentName = "";

                    foreach (NewTimeOutCard dia in Days)
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
        }
        public void GetAssignments(User usuario)
        {
            var Assignments_List = usuario.Assignments;
            foreach (Assignment assignment_item in Assignments_List)
            {
                Assignments.Add(assignment_item.Name);
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
                else if(day.Break3 == 0)
                {
                    day.Break3 = 45;
                    day.DisplayInputs += 90;
                    day.Break3Enabled = true;
                }
            }
            //UpdateDays(_oldDay);
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
                else if(day.Break1 == 45)
                {
                    day.Break1 = 0;
                    day.DisplayInputs -= 90;
                    day.Break1Enabled = false;
                }
            }
            //UpdateDays(_oldDay);
        }

        public void HideOrShowInputs(NewTimeOutCard day)
        {
            if (day != null)
            {
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 235;
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
                    day.bgColor = "#E1EAF7";
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
            /*if (_oldDay == day)
            {
                // click twice on same item to hide it
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 225;
                    day.bgColor = "#b9c7da";
                }
                else
                {
                    day.DisplayInputs = 0;
                    day.bgColor = "white";
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
            _oldDay = day;*/
        }

        private void UpdateDays(NewTimeOutCard day)
        {
            var tempSeconds = day.TimeOut.TotalSeconds - day.TimeIn.TotalSeconds;
            var tempSpan = TimeSpan.FromSeconds(tempSeconds);
            var hh = tempSpan.Hours;
            var mm = (tempSpan.Minutes / 60);
            day.Valor = hh + mm;
            var index = Days.IndexOf(day);
            Days.Remove(day);
            Days.Insert(index, day);
        }
    }
}
