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
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new AddExpensesViewModel(reporte, usuario);
        }

        private void Expense_Item_Tapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as AddExpensesViewModel;
            var day = e.Item as ExpensesList;
            vm.HideOrShowInputs(day);
        }
    }
}