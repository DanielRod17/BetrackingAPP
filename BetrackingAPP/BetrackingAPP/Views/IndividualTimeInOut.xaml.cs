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
   public partial class IndividualTimeInOut : TabbedPage
    {
        public User Usuario_Reload { get; set; }
        public Button boton { get; set; }
        public Timecard timecard_Reload { get; set; }
        public DateTime date_reload { get; set; }
        public IndividualTimeInOut(Timecard eu_timecard = null, User usuario = null, DateTime _dateSearch = default)
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
            BindingContext = new IndividualTimeInOutViewModel(usuario, eu_timecard, _dateSearch);
            if (eu_timecard.Submitted == 0)
            {
                var Button = new Button { 
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
            CrearUpdateButtons(eu_timecard, usuario, _dateSearch);
        }
        private void SubmitTimecard(int ID, Button button)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            vm.SubmitTimecard(ID);
            button.IsVisible = false;
            boton.IsVisible = false;
        }
        private async void ViewCell_Appearing(object sender, EventArgs e)
        {
            var cell = sender as ViewCell;
            var view = cell.View;
            view.TranslationX = -100;
            view.Opacity = 0;
            await Task.WhenAny<bool>
            (
                view.TranslateTo(0, 0, 250, Easing.SinIn),
                view.FadeTo(1, 500, Easing.BounceIn)
            );
        }
        private void CrearUpdateButtons(Timecard eu_timecard, User usuario, DateTime fecha)
        {
            var Stack = this.FindByName<StackLayout>("UpdateList");

            if (eu_timecard.Submitted == 0)
            {
                var Button = new Button { 
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
        private void TimecardsList_ItemTapped(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.HideOrShowInputs(day);
        }
        void Agregar_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.AgregarBreak(day);
        }
        void Remove_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.QuitarBreak(day);
        }
        private void UpdateTimecard(int ID, Button button, int AssignmentID)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            vm.UpdateTimecard(ID, AssignmentID);
        }
    }
}