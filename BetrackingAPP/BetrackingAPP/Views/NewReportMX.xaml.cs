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
    public partial class NewReportMX : ContentPage
    {
        public User Usuario { get; set; }
        public NewReportMX(User usuario = null)
        {
            InitializeComponent();
            BindingContext = new NewReportMXViewModel(usuario);
        }
    }
}