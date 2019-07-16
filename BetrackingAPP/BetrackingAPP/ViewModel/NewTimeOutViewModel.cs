﻿using System;
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
        private NewTimecardNormal _oldDay;
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

        public async void GoToDay(NewTimecardNormal eu_day)
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
            Days = new ObservableCollection<NewTimecardNormal> {
                new NewTimecardNormal() { Day = "Mon", Numero = monNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Tue", Numero = tueNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Wed", Numero = wedNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Thu", Numero = thuNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Fri", Numero = friNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Sat", Numero = satNum, Valor = 0.00m, Nota = "", DisplayInputs = 0 },
                new NewTimecardNormal() { Day = "Sun", Numero = fecha.Day, Valor = 0.00m, Nota = "", DisplayInputs = 0 }
            };
            GetAssignments(usuarioFrom);

            SaveTimecard = new Command(async () => await GuardarTimecard());
            SubmitTimecard = new Command(async () => await SometerTimecard());
        }

        public async Task GuardarTimecard()
        {
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(Days);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Assignment", AssignmentName),
                new KeyValuePair<string, string>("date", Fecha_Send.Date.ToString("g")),
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
            foreach (NewTimecardNormal dia in Days)
            {
                dia.Valor = 0.00m;
                dia.Nota = "";
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

        public void HideOrShowInputs(NewTimecardNormal day)
        {

            if (_oldDay == day)
            {
                // click twice on same item to hide it
                if (day.DisplayInputs == 0)
                {
                    day.DisplayInputs = 100;
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
                day.DisplayInputs = 100;
                UpdateDays(day);
            }
            _oldDay = day;
        }

        private void UpdateDays(NewTimecardNormal day)
        {
            var index = Days.IndexOf(day);
            Days.Remove(day);
            Days.Insert(index, day);
        }
    }
}