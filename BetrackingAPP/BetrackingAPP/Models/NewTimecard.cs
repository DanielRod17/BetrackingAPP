using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BetrackingAPP.Models
{
    public class NewTimecardNormal : INotifyPropertyChanged
    {
        public string Day { get; set; }
        public int Numero { get; set; }
        private decimal _valor { get; set; }
        public decimal Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                _valor = value;
                OnPropertyChanged();
            }
        }
        private string _nota { get; set; }
        public string Nota
        {
            get
            {
                return _nota;
            }
            set
            {
                _nota = value;
                OnPropertyChanged();
            }
        }
        public int DisplayInputs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
