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
    public partial class AddExpenses : ContentPage
    {
        public AddExpenses(Reports reporte, User usuario)
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
            BindingContext = new AddExpensesViewModel(reporte, usuario);
        }

        private void Expense_Item_Tapped(object sender, EventArgs e)
        {
            var vm = BindingContext as AddExpensesViewModel;
            var day = (sender as View).BindingContext as ExpensesList;
            vm.HideOrShowInputs(day);
        }

        protected override void OnDisappearing()
        {
            var vm = BindingContext as AddExpensesViewModel;
            vm.EnviarUpdate();
        }
    }
}