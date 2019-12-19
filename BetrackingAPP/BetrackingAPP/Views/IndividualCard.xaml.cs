using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualCard : TabbedPage
    {
        public User Usuario_Reload { get; set; }
        public Timecard timecard_Reload { get; set; }
        public DateTime date_reload { get; set; }
        public Button boton { get; set; }
        public IndividualCard(Timecard eu_timecard = null, User usuario = null, DateTime _dateSearch = default)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                case Device.UWP:
                default:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }
            Usuario_Reload = usuario;
            timecard_Reload = eu_timecard;
            date_reload = _dateSearch;
            InitializeComponent();
            BarBackgroundColor = Color.White;
            BarTextColor = Color.Black;
            BindingContext = new IndividualCardViewModel(usuario, eu_timecard, _dateSearch);

            //if (eu_timecard.Mon_In != 0)
            //{
            var stackPanel = this.FindByName<StackLayout>("stackPanel");
            //await Application.Current.MainPage.DisplayAlert("Oops", timecard.Name, "OK");
            Label label;
            //////////////////////////////////////////Monday
            if (eu_timecard.Mon_In != "00:00:00" && eu_timecard.Mon_In != null)
            {
                var monday_info = "Monday In: " + eu_timecard.Mon_In + " Out: " + eu_timecard.Mon_Out;
                label = new Label { BindingContext = BindingContext, Text = monday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);

                if (eu_timecard.Mon_Meal_In != "00:00:00" || eu_timecard.Mon_Meal_Out != "00:00:00")
                {
                    var monday_break = "Meal Start: " + eu_timecard.Mon_Meal_In + " Meal Out: " + eu_timecard.Mon_Meal_Out;
                    if (eu_timecard.Mon_Break1_In != "00:00:00" || eu_timecard.Mon_Break1_Out != "00:00:00")
                    {
                        monday_break = monday_break + "1st Break In: " + eu_timecard.Mon_Break1_In + " 1st Break Out: " + eu_timecard.Mon_Break1_Out;
                    }
                    if (eu_timecard.Mon_Break2_In != "00:00:00" || eu_timecard.Mon_Break2_Out != "00:00:00")
                    {
                        monday_break = monday_break + "2nd Break In: " + eu_timecard.Mon_Break2_In + " 2nd Break Out: " + eu_timecard.Mon_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = monday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Tuesday
            if (eu_timecard.Tue_In != "00:00:00" && eu_timecard.Tue_In != null)
            {
                var tuesday_info = "Tuesday In: " + eu_timecard.Tue_In + " Out: " + eu_timecard.Tue_Out;
                label = new Label { BindingContext = BindingContext, Text = tuesday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Tue_Meal_In != "00:00:00" || eu_timecard.Tue_Meal_Out != "00:00:00")
                {
                    var Tuesday_break = "Meal Start: " + eu_timecard.Tue_Meal_In + " Meal Out: " + eu_timecard.Tue_Meal_Out;
                    if (eu_timecard.Tue_Break1_In != "00:00:00" || eu_timecard.Tue_Break1_Out != "00:00:00")
                    {
                        Tuesday_break = Tuesday_break + "1st Break In: " + eu_timecard.Tue_Break1_In + " 1st Break Out: " + eu_timecard.Tue_Break1_Out;
                    }
                    if (eu_timecard.Tue_Break2_In != "00:00:00" || eu_timecard.Tue_Break2_Out != "00:00:00")
                    {
                        Tuesday_break = Tuesday_break + "2nd Break In: " + eu_timecard.Tue_Break2_In + " 2nd Break Out: " + eu_timecard.Tue_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Tuesday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Wednesday
            if (eu_timecard.Wed_In != "00:00:00" && eu_timecard.Wed_In != null)
            {
                var wednesday_info = "Wednesday In: " + eu_timecard.Wed_In + " Out: " + eu_timecard.Wed_Out;
                label = new Label { BindingContext = BindingContext, Text = wednesday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Wed_Meal_In != "00:00:00" || eu_timecard.Wed_Meal_Out != "00:00:00")
                {
                    var Wednesday_break = "Meal Start: " + eu_timecard.Wed_Meal_In + " Meal Out: " + eu_timecard.Wed_Meal_Out;
                    if (eu_timecard.Wed_Break1_In != "00:00:00" || eu_timecard.Wed_Break1_Out != "00:00:00")
                    {
                        Wednesday_break = Wednesday_break + "1st Break In: " + eu_timecard.Wed_Break1_In + " 1st Break Out: " + eu_timecard.Wed_Break1_Out;
                    }
                    if (eu_timecard.Wed_Break2_In != "00:00:00" || eu_timecard.Wed_Break2_Out != "00:00:00")
                    {
                        Wednesday_break = Wednesday_break + "2nd Break In: " + eu_timecard.Wed_Break2_In + " 2nd Break Out: " + eu_timecard.Wed_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Wednesday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Thursday
            if (eu_timecard.Thu_In != "00:00:00" && eu_timecard.Thu_In != null)
            {
                var thursday_info = "Thursday In: " + eu_timecard.Thu_In + " Out: " + eu_timecard.Thu_Out;
                label = new Label { BindingContext = BindingContext, Text = thursday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Thu_Meal_In != "00:00:00" || eu_timecard.Thu_Meal_Out != "00:00:00")
                {
                    var Thursday_break = "Meal Start: " + eu_timecard.Thu_Meal_In + " Meal Out: " + eu_timecard.Thu_Meal_Out;
                    if (eu_timecard.Thu_Break1_In != "00:00:00" || eu_timecard.Thu_Break1_Out != "00:00:00")
                    {
                        Thursday_break = Thursday_break + "1st Break In: " + eu_timecard.Thu_Break1_In + " 1st Break Out: " + eu_timecard.Thu_Break1_Out;
                    }
                    if (eu_timecard.Thu_Break2_In != "00:00:00" || eu_timecard.Thu_Break2_Out != "00:00:00")
                    {
                        Thursday_break = Thursday_break + "2nd Break In: " + eu_timecard.Thu_Break2_In + " 2nd Break Out: " + eu_timecard.Thu_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Thursday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Friday
            if (eu_timecard.Fri_In != "00:00:00" && eu_timecard.Fri_In != null)
            {
                var friday_info = "Friday In: " + eu_timecard.Fri_In + " Out: " + eu_timecard.Fri_Out;
                label = new Label { BindingContext = BindingContext, Text = friday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Fri_Meal_In != "00:00:00" || eu_timecard.Fri_Meal_Out != "00:00:00")
                {
                    var Friday_break = "Meal Start: " + eu_timecard.Fri_Meal_In + " Meal Out: " + eu_timecard.Fri_Meal_Out;
                    if (eu_timecard.Fri_Break1_In != "00:00:00" || eu_timecard.Fri_Break1_Out != "00:00:00")
                    {
                        Friday_break = Friday_break + "1st Break In: " + eu_timecard.Fri_Break1_In + " 1st Break Out: " + eu_timecard.Fri_Break1_Out;
                    }
                    if (eu_timecard.Fri_Break2_In != "00:00:00" || eu_timecard.Fri_Break2_Out != "00:00:00")
                    {
                        Friday_break = Friday_break + "2nd Break In: " + eu_timecard.Fri_Break2_In + " 2nd Break Out: " + eu_timecard.Fri_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Friday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Saturday
            if (eu_timecard.Sat_In != "00:00:00" && eu_timecard.Sat_In != null)
            {
                var saturday_info = "Saturday In: " + eu_timecard.Sat_In + " Out: " + eu_timecard.Sat_Out;
                label = new Label { BindingContext = BindingContext, Text = saturday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Sat_Meal_In != "00:00:00" || eu_timecard.Sat_Meal_Out != "00:00:00")
                {
                    var Saturday_break = "Meal Start: " + eu_timecard.Sat_Meal_In + " Meal Out: " + eu_timecard.Sat_Meal_Out;
                    if (eu_timecard.Sat_Break1_In != "00:00:00" || eu_timecard.Sat_Break1_Out != "00:00:00")
                    {
                        Saturday_break = Saturday_break + "1st Break In: " + eu_timecard.Sat_Break1_In + " 1st Break Out: " + eu_timecard.Sat_Break1_Out;
                    }
                    if (eu_timecard.Sat_Break2_In != "00:00:00" || eu_timecard.Sat_Break2_Out != "00:00:00")
                    {
                        Saturday_break = Saturday_break + "2nd Break In: " + eu_timecard.Sat_Break2_In + " 2nd Break Out: " + eu_timecard.Sat_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Saturday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }
            //////////////////////////////////////////Sunday
            if (eu_timecard.Sun_In != "00:00:00" && eu_timecard.Sun_In != null)
            {
                var sunday_info = "Sunday In: " + eu_timecard.Sun_In + " Out: " + eu_timecard.Sun_Out;
                label = new Label { BindingContext = BindingContext, Text = sunday_info, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                stackPanel.Children.Add(label);
                if (eu_timecard.Sun_Meal_In != "00:00:00" || eu_timecard.Sun_Meal_Out != "00:00:00")
                {
                    var Sunday_break = "Meal Start: " + eu_timecard.Sun_Meal_In + " Meal Out: " + eu_timecard.Sun_Meal_Out;
                    if (eu_timecard.Sun_Break1_In != "00:00:00" || eu_timecard.Sun_Break1_Out != "00:00:00")
                    {
                        Sunday_break = Sunday_break + "1st Break In: " + eu_timecard.Sun_Break1_In + " 1st Break Out: " + eu_timecard.Sun_Break1_Out;
                    }
                    if (eu_timecard.Sun_Break2_In != "00:00:00" || eu_timecard.Sun_Break2_Out != "00:00:00")
                    {
                        Sunday_break = Sunday_break + "2nd Break In: " + eu_timecard.Sun_Break2_In + " 2nd Break Out: " + eu_timecard.Sun_Break2_Out;
                    }
                    label = new Label { BindingContext = BindingContext, Text = Sunday_break, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                    stackPanel.Children.Add(label);
                }
            }

            if (eu_timecard.Submitted == 0)
            {
                var Button = new Button
                {
                    Text = "Submit",
                    BackgroundColor = Color.FromHex("#15212f"),
                    TextColor = Color.FromHex("#FFFFFF"),
                    FontSize = 24,
                    Margin = 20,
                    FontFamily = Device.RuntimePlatform == Device.Android ? "BebasNeue Bold.ttf#BebasNeue Bold" : null
                };
                Button.Clicked += delegate { SubmitTimecard(eu_timecard.ID, Button); };
                stackPanel.Children.Add(Button);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Actions List
            CrearUpdateButtons(eu_timecard, usuario, _dateSearch);
        }


        private void SubmitTimecard(int ID, Button button)
        {
            var vm = BindingContext as IndividualCardViewModel;
            vm.SubmitTimecard(ID);
            button.IsVisible = false;
            boton.IsVisible = false;
        }

        private void CrearUpdateButtons(Timecard eu_timecard, User usuario, DateTime fecha)
        {
            var Stack = this.FindByName<StackLayout>("UpdateList");
            
            if (eu_timecard.Submitted == 0)
            {
                var Button = new Button
                {
                    Text = "Update",
                    BackgroundColor = Color.FromHex("#15212f"),
                    TextColor = Color.FromHex("#FFFFFF"),
                    FontSize = 24,
                    Margin = 20,
                    FontFamily = Device.RuntimePlatform == Device.Android ? "BebasNeue Bold.ttf#BebasNeue Bold" : null
                };
                Button.Clicked += delegate { UpdateTimecard(eu_timecard.ID, Button, eu_timecard.AssignmentID); };
                Stack.Children.Add(Button);
                boton = Button;
            }
        }
        private void UpdateTimecard(int ID, Button button, int AssignmentID)
        {
            var vm = BindingContext as IndividualCardViewModel;
            vm.UpdateTimecard(ID, AssignmentID);
        }

        private void TimecardsList_ItemTapped(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualCardViewModel;
            var day = (sender as View).BindingContext as NewTimecardNormal;
            vm.HideOrShowInputs(day);
        }
    }
}