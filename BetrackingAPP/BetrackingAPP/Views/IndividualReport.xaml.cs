using BetrackingAPP.Models;
using BetrackingAPP.ViewModel;
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
    public partial class IndividualReport : TabbedPage
    {
        public Reports Reporte { get; set; }
        public User Usuario { get; set; }
        private bool _cargado = false;
        public bool Cargado
        {
            get
            {
                return _cargado;
            }
            set
            {
                _cargado = value;
                OnPropertyChanged();
            }
        }
        public IndividualReport(Models.Reports eu_report, Models.User usuario)
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
            Usuario = usuario;
            Reporte = eu_report;
            //BarBackgroundColor = Color.White;
            //BarTextColor = Color.Black;
            BindingContext = new IndividualReportViewModel(usuario, eu_report);
            Cargado = true;
        }
        protected override void OnAppearing()
        {
            var vm = BindingContext as IndividualReportViewModel;
            //vm.CargarValores();
        }
        private void AddExpenses(object sender, EventArgs e)
        {
            var eu_report = Reporte;
            var usuario = Usuario;
            var vm = BindingContext as IndividualReportViewModel;
            vm.AddExpenses(eu_report, usuario);
        }
        private void AddFiles(object sender, EventArgs e)
        {
            var eu_report = Reporte;
            var usuario = Usuario;
            var vm = BindingContext as IndividualReportViewModel;
            vm.AddFiles(eu_report, usuario);
        }
        private void SubmitReport(object sender, EventArgs e)
        {
            var eu_report = Reporte;
            var usuario = Usuario;
            var vm = BindingContext as IndividualReportViewModel;
            vm.SubmitReport(eu_report, usuario);
        }
    }
}