using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BetrackingAPP.Models;
using BetrackingAPP.Views;
using Xamarin.Forms;
using PCLCrypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using System.Net;

namespace BetrackingAPP.ViewModel
{
    public class AddExpensesViewModel : BaseViewModel
    {
        public User Usuario { get; set; }
        public Reports Reporte { get; set; }
        public ObservableCollection<ExpensesList> _expensesList { get; set; }
        public ObservableCollection<ExpensesList> ExpensesList
        {
            get
            {
                return _expensesList;
            }
            set
            {
                _expensesList = value;
                OnPropertyChanged();
            }
        }
        public Command AddExpense { get; private set; }
        public AddExpensesViewModel(Reports reporte, User usuario)
        {
            Usuario = usuario;
            Reporte = reporte;
            AddExpense = new Command<ExpensesList>(AgregarExpense);
            ExpensesList = new ObservableCollection<ExpensesList> {
                new ExpensesList() { ID = 1, NameCategory = "Baggage Fees", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 2, NameCategory = "Airfare", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 3, NameCategory = "Car Rental", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 4, NameCategory = "Lodging", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 5, NameCategory = "Parking", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 6, NameCategory = "Gasoline", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 7, NameCategory = "Miscellaneous", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 8, NameCategory = "Meals", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 9, NameCategory = "Tools & Supplies", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 10, NameCategory = "Change Fees", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 11, NameCategory = "Transportation", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses =new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 12, NameCategory = "Toll", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 13, NameCategory = "Per Diem", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new List<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
            };
            CargarExpenses();
        }

        public void CargarExpenses()
        {
            foreach (Expense expense_item in Reporte.Expenses)
            {
                switch (expense_item.Category)
                {
                    case 1:
                        EmpujarExpense(1, expense_item);
                        break;
                    case 2:
                        EmpujarExpense(2, expense_item);
                        break;
                    case 3:
                        EmpujarExpense(3, expense_item);
                        break;
                    case 4:
                        EmpujarExpense(4, expense_item);
                        break;
                    case 5:
                        EmpujarExpense(5, expense_item);
                        break;
                    case 6:
                        EmpujarExpense(6, expense_item);
                        break;
                    case 7:
                        EmpujarExpense(7, expense_item);
                        break;
                    case 8:
                        EmpujarExpense(8, expense_item);
                        break;
                    case 9:
                        EmpujarExpense(9, expense_item);
                        break;
                    case 10:
                        EmpujarExpense(10, expense_item);
                        break;
                    case 11:
                        EmpujarExpense(11, expense_item);
                        break;
                    case 12:
                        EmpujarExpense(12, expense_item);
                        break;
                    case 13:
                        EmpujarExpense(13, expense_item);
                        break;
                    default:
                        break;
                }
            }
        }

        private void EmpujarExpense(int v, Expense item)
        {
            var Category_List = ExpensesList[v - 1];
            var tempList = Category_List.iExpenses;
            tempList.Add(item);
            ExpensesList[v - 1].iExpenses = tempList;
        }

        public void HideOrShowInputs(ExpensesList expense_cat)
        {
            if (expense_cat != null) {

                if (expense_cat.DisplayInputs == 0)
                {
                    if (expense_cat.DisplayAdd == 0)
                    {
                        expense_cat.DisplayAdd = 40;
                    }
                    else
                    {
                        expense_cat.DisplayAdd = 0;
                    }
                    expense_cat.DisplayInputs = ( (expense_cat.iExpenses.Count) * 390 );
                }
                else
                {
                    expense_cat.DisplayAdd = 0;
                    expense_cat.DisplayInputs = 0;
                }
            }
        }
        public void AgregarExpense(ExpensesList Lista)
        {
            var x = Lista;
        }
    }
}
