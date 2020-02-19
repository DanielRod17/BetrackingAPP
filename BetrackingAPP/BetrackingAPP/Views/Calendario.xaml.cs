using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendario : PopupPage
    {
        public Calendario(User user, DateTime fecha, int Individual, Timecard Timecard = null)
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel(user, fecha, Individual, Timecard);
        }
        void HidePopup(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
        protected override void OnDisappearing()
        {
            var vm = BindingContext as CalendarViewModel;
            vm.EnviarUpdate();
        }
    }
}