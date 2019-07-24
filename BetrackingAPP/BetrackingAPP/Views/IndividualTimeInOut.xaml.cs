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
        public Timecard timecard_Reload { get; set; }
        public DateTime date_reload { get; set; }
        public IndividualTimeInOut(Timecard eu_timecard = null, User usuario = null, DateTime _dateSearch = default)
        {
            Usuario_Reload = usuario;
            timecard_Reload = eu_timecard;
            date_reload = _dateSearch;
            InitializeComponent();
            BarBackgroundColor = Color.White;
            BarTextColor = Color.Black;
            BindingContext = new IndividualTimeInOutViewModel(usuario, eu_timecard, _dateSearch);
            CrearUpdateButtons(eu_timecard, usuario, _dateSearch);
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
                var Button = new Button { Text = "Update", BackgroundColor = Color.FromHex("#15212f"), TextColor = Color.FromHex("#FFFFFF"), FontSize = 20 };
                Button.Clicked += delegate { UpdateTimecard(eu_timecard.ID, Button, eu_timecard.AssignmentID); };
                Stack.Children.Add(Button);
            }
        }
        private void TimecardsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            var day = e.Item as NewTimeOutCard;
            vm.HideOrShowInputs(day);
        }

        void Agregar_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            vm.AgregarBreak();
        }

        void Remove_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            vm.QuitarBreak();
        }

        private void UpdateTimecard(int ID, Button button, int AssignmentID)
        {
            var vm = BindingContext as IndividualTimeInOutViewModel;
            vm.UpdateTimecard(ID, AssignmentID);
        }
    }
}