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
        private string _bgColor { get; set; }
        public string bgColor
        {
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
        private int _displayInputsNotes { get; set; }
        public int DisplayInputsNotes
        {
            get
            {
                return _displayInputsNotes;
            }
            set
            {
                _displayInputsNotes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
