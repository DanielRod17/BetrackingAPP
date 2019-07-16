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
            BindingContext = new IndividualCardViewModel(usuario, eu_timecard, _dateSearch);
        }
    }
}