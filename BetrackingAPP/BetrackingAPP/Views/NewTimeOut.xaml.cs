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
    public partial class NewTimeOut : ContentPage
    {
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
        public NewTimeOut(List<Models.Timecard> eu_timecard = null, User usuario = null, System.DateTime dateSearch = default)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new NewTimeOutViewModel(usuario, eu_timecard, dateSearch);
        }

        private void TimecardsList_ItemTapped(object sender, EventArgs e)
        {
            /*var vm = BindingContext as NewTimeOutViewModel;
            var day = e.Item as NewTimeOutCard;
            vm.HideOrShowInputs(day);*/
            var vm = BindingContext as NewTimeOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.HideOrShowInputs(day);
        }

        void Agregar_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTimeOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.AgregarBreak(day);
        }

        void Remove_Break(object sender, EventArgs e)
        {
            var vm = BindingContext as NewTimeOutViewModel;
            var day = (sender as View).BindingContext as NewTimeOutCard;
            vm.QuitarBreak(day);
        }
    }
}