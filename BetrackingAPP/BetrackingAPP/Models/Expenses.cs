using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BetrackingAPP.Models;
using Xamarin.Forms;

namespace BetrackingAPP.Models
{
    public class ExpensesList : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string NameCategory { get; set; }
        private decimal _totalMoney { get; set; }
        public decimal TotalMoney
        {
            get
            {
                return _totalMoney;
            }
            set
            {
                _totalMoney = value;
                OnPropertyChanged();
            }
        }
        private int _totalExpenses { get; set; }
        public int TotalExpenses
        {
            get
            {
                return _totalExpenses;
            }
            set
            {
                _totalExpenses = value;
                OnPropertyChanged();
            }
        }
        private int _displayAdd { get; set; }
        public int DisplayAdd
        {
            get
            {
                return _displayAdd;
            }
            set
            {
                _displayAdd = value;
                OnPropertyChanged();
            }
        }
        private int _displayInputs { get; set; }
        public int DisplayInputs
        {
            get
            {
                return _displayInputs;
            }
            set
            {
                _displayInputs = value;
                OnPropertyChanged();
            }
        }
        private string _bgColor { get; set; }
        public string bgColor { 
            get
            {
                return _bgColor;
            }
            set
            {
                _bgColor = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Expense> _iexpenses { get; set; }
        public ObservableCollection<Expense> iExpenses
        {
            get
            {
                return _iexpenses;
            }
            set
            {
                _iexpenses = value;
                if (value != null)
                {
                    OnPropertyChanged();
                    UpdateInfo();
                }
            }
        }
        public void UpdateInfo()
        {
            TotalExpenses = iExpenses.Count;
            TotalMoney = 0;
            if ( iExpenses.Count > 0 )
            {
                foreach (Expense expense_item in iExpenses)
                {
                    if (!string.IsNullOrEmpty(expense_item.Quantity))
                    {
                        TotalMoney += expense_item.ValorFinal;
                    }
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
