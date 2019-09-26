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
using System.Linq;

namespace BetrackingAPP.ViewModel
{
    public class AddExpensesViewModel : BaseViewModel
    {
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public User Usuario { get; set; }
        public Reports Reporte { get; set; }
        public string ReportName { get; set; }
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
        public void EnviarUpdate()
        {
            IsLoading = true;
            MessagingCenter.Send<IndividualReportViewModel>(new IndividualReportViewModel(Usuario, Reporte), "Change");
            IsLoading = false;
        }
        public Command<int> AddExpense { get; }
        public Command<Expense> DeleteExpense { get; }
        public Command GuardarExpenses { get; set; }
        public AddExpensesViewModel(Reports reporte, User usuario)
        {
            IsLoading = true;
            Usuario = usuario;
            Reporte = reporte;
            ReportName = Reporte.Name;
            AddExpense = new Command<int>(AgregarExpense);
            DeleteExpense = new Command<Expense>(BorrarExpense);
            GuardarExpenses = new Command(async () => await SaveExpenses());
            ExpensesList = new ObservableCollection<ExpensesList> {
                new ExpensesList() { ID = 1, NameCategory = "Baggage Fees", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 2, NameCategory = "Airfare", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 3, NameCategory = "Car Rental", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 4, NameCategory = "Lodging", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 5, NameCategory = "Parking", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 6, NameCategory = "Gasoline", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 7, NameCategory = "Miscellaneous", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 8, NameCategory = "Meals", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 9, NameCategory = "Tools & Supplies", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 10, NameCategory = "Change Fees", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 11, NameCategory = "Transportation", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses =new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 12, NameCategory = "Toll", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
                new ExpensesList() { ID = 13, NameCategory = "Per Diem", TotalMoney = 0.00m, TotalExpenses = 0, iExpenses = new ObservableCollection<Expense>(), DisplayInputs = 0, DisplayAdd = 0 },
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
            IsLoading = false;
        }
        public void AgregarExpense(int expenese)
        {
            var x = ExpensesList[expenese-1].iExpenses;
            x.Add(new Expense() { TravelID = Reporte.ID, Category = 0, Name = "", Quantity = "", Currency = 0, Billable = 1, Refundable = 0, Attachments = "", DOF = 0.00m, SalesForceID = null, CategoryName = "Baggage Fees", DescriptionOn = false, ValorFinal = 0.00m, RefundableOn = false, MxOn = false });
            ExpensesList[expenese-1].iExpenses = null;
            ExpensesList[expenese-1].iExpenses = x;
            ExpensesList[expenese - 1].DisplayAdd = 40;
            ExpensesList[expenese - 1].DisplayInputs += 340;
            var pop = ExpensesList[expenese - 1];
            ExpensesList.RemoveAt(expenese - 1);
            ExpensesList.Insert(expenese - 1, pop);
            ExpensesList[expenese].UpdateInfo();

            /*var r = ExpensesList;
            ExpensesList = null;
            ExpensesList = r;*/
        }
        public void BorrarExpense(Expense item)
        {
            var index_lista = 0;
            foreach (ExpensesList listilla in ExpensesList)
            {
                var index = listilla.iExpenses.IndexOf(item);
                index_lista = ExpensesList.IndexOf(listilla);
                if (index != -1)
                {
                    listilla.iExpenses.RemoveAt(index);
                    listilla.DisplayInputs -= 340;
                    break;
                }
            }
            ExpensesList[index_lista].UpdateInfo();
            /*ExpensesList[index_lista].TotalExpenses = ExpensesList[index_lista].iExpenses.Count;
            ExpensesList[index_lista].TotalMoney = 0;
            foreach (Expense expense_item in ExpensesList[index_lista].iExpenses)
            {
                ExpensesList[index_lista].TotalMoney += expense_item.ValorFinal;
            }*/
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
                        foreach (Expense expenese in expense_cat.iExpenses)
                        {
                            //expenese.bgColor = "#F4F4F4";
                        }
                    }
                    else
                    {
                        expense_cat.DisplayAdd = 0;
                    }
                    expense_cat.DisplayInputs = ( (expense_cat.iExpenses.Count) * 337 );
                }
                else
                {
                    expense_cat.DisplayAdd = 0;
                    expense_cat.DisplayInputs = 0;
                    foreach (Expense expenese in expense_cat.iExpenses)
                    {
                        //expenese.bgColor = "White";
                    }
                }
            }
            expense_cat.UpdateInfo();
        }
        public async Task SaveExpenses()
        {
            IsLoading = true;
            HttpClient client = new HttpClient();

            var yeison = JsonConvert.SerializeObject(ExpensesList);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Usuario", Usuario.Id.ToString()),
                new KeyValuePair<string, string>("Travel", Reporte.ID.ToString()),
                new KeyValuePair<string, string>("info", yeison)
            }); ;
            //await App.Current.MainPage.DisplayAlert("H", yeison, "Ok");
            var result = await client.PostAsync("https://bepc.backnetwork.net/BEPCINC/api/SaveExpenses.php", formContent);
            if (result.IsSuccessStatusCode)
            {
                foreach (ExpensesList expense in ExpensesList)
                {
                    expense.UpdateInfo();
                }
                var responseData = await result.Content.ReadAsStringAsync();
                //await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                if (responseData == "Expenses Saved. Submit the expense report to send it for approval.")
                {
                    //foreach (NewTimecardNormal dia in Days)
                    //{
                    //    dia.Valor = 0.00m;
                    //    dia.Nota = "";
                    //}
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ReturnSave(Usuario, responseData));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", responseData, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong :(", "OK");
            }
            IsLoading = false;
        }
    }
}
