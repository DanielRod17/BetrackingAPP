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
    public partial class NewTimecard : ContentPage
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
        public NewTimecard(List<Models.Timecard> eu_timecard = null, User usuario = null, System.DateTime dateSearch = default)
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
            InitializeComponent();
            BindingContext = new NewTimecardViewModel(usuario, eu_timecard, dateSearch);
        }

        private void TimecardsList_ItemTapped(object sender, EventArgs e)
        {
            /*var vm = BindingContext as NewTimecardViewModel;
            var day = e.Item as NewTimecardNormal;
            vm.HideOrShowInputs(day);*/
            var vm = BindingContext as NewTimecardViewModel;
            var day = (sender as View).BindingContext as NewTimecardNormal;
            vm.HideOrShowInputs(day);
        }
    }
}