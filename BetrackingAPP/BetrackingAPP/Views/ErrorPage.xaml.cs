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
    public partial class ErrorPage : PopupPage
    {
        public ErrorPage( string textContent = null )
        {
            InitializeComponent();
            var coso = this.FindByName<Label>("Motivo");
            coso.Text = textContent;
            BindingContext = new ErrorPageViewModel(textContent);
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