using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BetrackingAPP.Models;
using Xamarin.Forms;

namespace BetrackingAPP.ViewModel
{
    public class IndividualReportViewModel : BaseViewModel
    {
        public Reports Reporte;

        ObservableCollection<Expense> expenses = new ObservableCollection<Expense>();
        public ObservableCollection<Expense> ExpensesList { get { return expenses; } }

        ObservableCollection<File> files = new ObservableCollection<File>();
        public ObservableCollection<File> FilesList { get { return files; } }
        public string Name { get; set; }
        public string ProjectName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal BaggageExp { get; set; }
        public decimal AirfareExp { get; set; }
        public decimal CarRentalExp { get; set; }
        public decimal GasolineExp { get; set; }
        public decimal LodgingExp { get; set; }
        public decimal ParkingExp { get; set; }
        public decimal MiscExp { get; set; }
        public decimal MealsExp { get; set; }
        public decimal ToolsExp { get; set; }
        public decimal ChangeExp { get; set; }
        public decimal TransportationExp { get; set; }
        public decimal TollExp { get; set; }
        public decimal PerDiemExp { get; set; }
        public decimal Billable { get; set; }
        public decimal Refundable { get; set; }
        public decimal Total { get; set; }
        public Command DownloadCommand { get; private set; }

        void DownloadFile(string URL)
        {
            URL = "https://bepc.backnetwork.net/BEPCINC" + URL;
            WebClient myWebClient = new WebClient();
            Uri Ur = new Uri(URL);
            Device.OpenUri(Ur);
        }

        public IndividualReportViewModel(User usuarioFrom, Reports reportFrom)
        {

            DownloadCommand = new Command<string>(DownloadFile);
            Reporte = reportFrom;
            var Expenses_List = reportFrom.Expenses;
            var Files_List = reportFrom.Files;
            decimal Valor_expense = 0;

            ProjectName = reportFrom.ProjectName;
            FromDate = reportFrom.FromDate;
            ToDate = reportFrom.ToDate;

            foreach (File file_item in Files_List)
            {
                files.Add(file_item);
            }
            foreach (Expense expense_item in Expenses_List)
            {
                expenses.Add(expense_item);
                Valor_expense = expense_item.Quantity;
                if (expense_item.Currency == 0)
                {
                    Valor_expense = Valor_expense / expense_item.DOF;
                }

                if (expense_item.Refundable == 1)
                {
                    Refundable += Valor_expense;
                }
                Billable += Valor_expense;
                Total += Valor_expense;

                switch (expense_item.Category)
                {
                    case 1:
                        BaggageExp += Valor_expense;
                        break;
                    case 2:
                        AirfareExp += Valor_expense;
                        break;
                    case 3:
                        CarRentalExp += Valor_expense;
                        break;
                    case 4:
                        LodgingExp += Valor_expense;
                        break;
                    case 5:
                        ParkingExp += Valor_expense;
                        break;
                    case 6:
                        GasolineExp += Valor_expense;
                        break;
                    case 7:
                        MiscExp += Valor_expense;
                        break;
                    case 8:
                        MealsExp += Valor_expense;
                        break;
                    case 9:
                        ToolsExp += Valor_expense;
                        break;
                    case 10:
                        ChangeExp += Valor_expense;
                        break;
                    case 11:
                        TransportationExp += Valor_expense;
                        break;
                    case 12:
                        TollExp += Valor_expense;
                        break;
                    case 13:
                        PerDiemExp += Valor_expense;
                        break;
                }
            }
        }
    }
}
