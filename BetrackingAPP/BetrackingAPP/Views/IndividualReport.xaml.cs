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
        public IndividualReport(Models.Reports eu_report, Models.User usuario)
        {
            InitializeComponent();
            BarBackgroundColor = Color.White;
            BarTextColor = Color.Black;
            BindingContext = new IndividualReportViewModel(usuario, eu_report);
            if (eu_report.Status == 0 || eu_report.Status == 2)
            {
                var stackPanel = this.FindByName<StackLayout>("ExpensesList");
                var Button = new Button { Text = "Add Expenses", BackgroundColor = Color.FromHex("#15212f"), TextColor = Color.FromHex("#FFFFFF"), FontSize = 20 };
                Button.Clicked += delegate { AddExpenses(eu_report, usuario, Button); };
                stackPanel.Children.Add(Button);
            }
        }

        private void AddExpenses(Reports eu_report, User usuario, Button button)
        {
            var vm = BindingContext as IndividualReportViewModel;
            vm.AddExpenses(eu_report, usuario);
        }
    }
}