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
    public partial class ReturnSave : PopupPage
    {
        private List<Models.Timecard> _timecards;
        public List<Models.Timecard> Timecards
        {
            get => _timecards;
            set
            {
                _timecards = value;
                OnPropertyChanged();
            }
        }
        User usuario { get; set; }
        string textContent { get; set; }
        public ReturnSave(User usuarioFrom = null, string textContentFrom = null)
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
            usuario = usuarioFrom;
            BindingContext = new ReturnSaveViewModel(usuario, textContentFrom);
            var coso = this.FindByName<Label>("Motivo");
            coso.Text = textContentFrom;
            coso.TextColor = Color.FromHex("#15212f");
        }

        void HidePopup(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void GoBack(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
            Navigation.PopAsync();
        }
    }
}