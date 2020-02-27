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
        public Command LoadPrevious { get; set; }
        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
        .Where(x => x.ToLower().Contains(text.ToLower()))
        .OrderBy(x => x)
        .ToList();

        public User Usuario;
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

        public Timecard Timecard;
        
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
        private ObservableCollection<NewTimeOutCard> _days { get; set; }
        public ObservableCollection<NewTimeOutCard> Days
        {
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
            IsLoading = true;
            Usuario = usuarioFrom;
            Fecha_Send = fecha;
            int monNum = fecha.AddDays(-6).Day;
            int tueNum = fecha.AddDays(-5).Day;
            int wedNum = fecha.AddDays(-4).Day;
            int thuNum = fecha.AddDays(-3).Day;
            int friNum = fecha.AddDays(-2).Day;
            int satNum = fecha.AddDays(-1).Day;
            Fecha_timecard = fecha.Date.ToString("MM/dd/yyyy");
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
            LoadPrevious = new Command(async () => await CargarPrevious());
            IsLoading = false;
        }
        public async Task CargarPrevious()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
            });
            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/CopyPrevious.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                Timecard timecard = JsonConvert.DeserializeObject<Timecard>(responseData);
                AssignmentName = timecard.Name;
                int monNum = Fecha_Send.AddDays(-6).Day;
                int tueNum = Fecha_Send.AddDays(-5).Day;
                int wedNum = Fecha_Send.AddDays(-4).Day;
                int thuNum = Fecha_Send.AddDays(-3).Day;
                int friNum = Fecha_Send.AddDays(-2).Day;
                int satNum = Fecha_Send.AddDays(-1).Day;
                int sunNum = Fecha_Send.Day;
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
                    new NewTimeOutCard() { Is24 = sun24, Break1Enabled = sunBreak1Enabled, Break2Enabled = sunBreak2Enabled, Break3Enabled = sunBreak3Enabled, Break1 = sunBreak1, Break2 = sunBreak2, Break3 = sunBreak3, Day = "Sun", Numero = sunNum, Valor = timecard.Sun, TimeIn = TimeSpan.Parse(timecard.Sun_In), TimeOut= TimeSpan.Parse(timecard.Sun_Out), Break1Out=TimeSpan.Parse(timecard.Sun_Meal_In), Break1In=TimeSpan.Parse(timecard.Sun_Meal_Out), Break2Out=TimeSpan.Parse(timecard.Sun_Break1_In), Break2In=TimeSpan.Parse(timecard.Sun_Break1_Out), Break3Out=TimeSpan.Parse(timecard.Sun_Break2_In), Break3In=TimeSpan.Parse(timecard.Sun_Break2_Out), Nota = timecard.SunNote, DisplayInputs = sunDisplay }
                };
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
            }
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

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveTimeOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Timecard Saved!")
                {
                    AssignmentName = "";

                    foreach (NewTimeOutCard dia in Days)
                    {
                        dia.Valor = 0.00m;
                        dia.Nota = "";
                    }

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
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
                //await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
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
                new KeyValuePair<string, string>("Submit", "1"),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("MM/dd/yyyy")),
                new KeyValuePair<string, string>("info", yeison)
            }); ;

            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveTimeOut.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                var responseData = await result.Content.ReadAsStringAsync();
                if (responseData == "Timecard Submitted!")
                {
                    AssignmentName = "";

                    foreach (NewTimeOutCard dia in Days)
                    {
                        dia.Valor = 0.00m;
                        dia.Nota = "";
                    }

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
                await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPage("Something went wrong :("));
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
