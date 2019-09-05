using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace BetrackingAPP.Models
{
    public class NewTimeOutCard : INotifyPropertyChanged
    {
        private bool _break1Enabled { get; set; }
        public bool Break1Enabled
        {
            get
            {
                return _break1Enabled;
            }
            set
            {
                _break1Enabled = value;
                OnPropertyChanged();
            }
        }
        private bool _break2Enabled { get; set; }
        public bool Break2Enabled
        {
            get
            {
                return _break2Enabled;
            }
            set
            {
                _break2Enabled = value;
                OnPropertyChanged();
            }
        }
        private bool _break3Enabled { get; set; }
        public bool Break3Enabled
        {
            get
            {
                return _break3Enabled;
            }
            set
            {
                _break3Enabled = value;
                OnPropertyChanged();
            }
        }
        public string Day { get; set; }
        public int Numero { get; set; }
        private int _inputHeight { get; set; }
        public int InputHeight
        {
            get
            {
                return _inputHeight;
            }
            set
            {
                _inputHeight = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan _timein = new TimeSpan(0, 0, 0);
        public TimeSpan TimeIn {
            get
            {
                return _timein;
            }
            set
            {
                _timein = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        private TimeSpan _timeout = new TimeSpan(0, 0, 0);
        public TimeSpan TimeOut {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
                UpdateValor();
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
        private TimeSpan _break1Out = new TimeSpan(0, 0, 0);
        private TimeSpan _break1In = new TimeSpan(0, 0, 0);
        private TimeSpan _break2Out = new TimeSpan(0, 0, 0);
        private TimeSpan _break2In = new TimeSpan(0, 0, 0);
        private TimeSpan _break3Out = new TimeSpan(0, 0, 0);
        private TimeSpan _break3In = new TimeSpan(0, 0, 0);
        public TimeSpan Break1Out {
            get
            {
                return _break1Out;
            }
            set
            {
                _break1Out = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        public TimeSpan Break1In {
            get
            {
                return _break1In;
            }
            set
            {
                _break1In = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        public TimeSpan Break2Out {
            get
            {
                return _break2Out;
            }
            set
            {
                _break2Out = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        public TimeSpan Break2In {
            get
            {
                return _break2In;
            }
            set
            {
                _break2In = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        public TimeSpan Break3Out {
            get
            {
                return _break3Out;
            }
            set
            {
                _break3Out = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        public TimeSpan Break3In {
            get
            {
                return _break3In;
            }
            set
            {
                _break3In = value;
                UpdateValor();
                OnPropertyChanged();
            }
        }
        private decimal _valor = 0;
        public decimal Valor {
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
        public string Nota { get; set; }
        private int _displayInputs { get; set; }
        public int DisplayInputs {
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
        private int _Break1 = 0;
        public int Break1 {
            get
            {
                return _Break1;
            }
            set
            {
                _Break1 = value;
                OnPropertyChanged();
            }
        }
        private int _Break2 = 0;
        public int Break2
        {
            get
            {
                return _Break2;
            }
            set
            {
                _Break2 = value;
                OnPropertyChanged();
            }
        }
        private int _Break3 = 0;
        public int Break3
        {
            get
            {
                return _Break3;
            }
            set
            {
                _Break3 = value;
                OnPropertyChanged();
            }
        }
        public void UpdateValor()
        {
            if (TimeOut < TimeIn)
            {
                TimeOut = TimeSpan.FromSeconds(TimeIn.TotalSeconds + 32400);
            }

            if (Break1 != 0)
            {
                if (Break1Out < TimeIn)
                {
                    Break1Out = TimeSpan.FromSeconds(TimeIn.TotalSeconds + 60);
                }
                if (Break1In < Break1Out)
                {
                    Break1In = TimeSpan.FromSeconds(Break1Out.TotalSeconds + 60);
                    if (TimeOut < Break1In)
                    {
                        TimeOut = TimeSpan.FromSeconds(Break1In.TotalSeconds + 60);
                    }
                }
                if (Break2 != 0)
                {
                    if (Break2Out < Break1In)
                    {
                        Break2Out = TimeSpan.FromSeconds(Break1In.TotalSeconds + 60);
                    }
                    if (Break2In < Break2Out)
                    {
                        Break2In = TimeSpan.FromSeconds(Break1Out.TotalSeconds + 60);
                    }
                    if (TimeOut < Break2In)
                    {
                        TimeOut = TimeSpan.FromSeconds(Break2In.TotalSeconds + 60);
                    }
                    if (Break3 != 0)
                    {
                        if (Break3Out < Break2In)
                        {
                            Break3Out = TimeSpan.FromSeconds(Break2In.TotalSeconds + 60);
                        }
                        if (Break3In < Break3Out)
                        {
                            Break3In = TimeSpan.FromSeconds(Break3Out.TotalSeconds + 60);
                        }
                        if (TimeOut < Break3In)
                        {
                            TimeOut = TimeSpan.FromSeconds(Break3In.TotalSeconds + 60);
                        }
                    }
                }
            }

            var tempSeconds = TimeOut.TotalSeconds - TimeIn.TotalSeconds;

            if (Break1 != 0)
            {
                tempSeconds = tempSeconds - (Break1In.TotalSeconds - Break1Out.TotalSeconds);
            }
            if (Break2 != 0)
            {
                tempSeconds = tempSeconds - (Break2In.TotalSeconds - Break2Out.TotalSeconds);
            }
            if (Break3 != 0)
            {
                tempSeconds = tempSeconds - (Break3In.TotalSeconds - Break3Out.TotalSeconds);
            }

            var tempSpan = TimeSpan.FromSeconds(tempSeconds);
            decimal hh = tempSpan.Hours;
            decimal mm = (decimal.Parse(tempSpan.Minutes.ToString()) / 60);
            var decimalVal = hh + mm;
            var decimalStr = decimalVal.ToString("0.##");
            Valor = decimal.Parse(decimalStr);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
