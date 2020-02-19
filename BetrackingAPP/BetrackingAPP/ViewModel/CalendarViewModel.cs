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
    class CalendarViewModel : BaseViewModel
    {
        private DateTime _fecha { get; set; }
        public DateTime FechaSeleccionada
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
                OnPropertyChanged();
            }
        }
        private User _user { get; set; }
        public User Usuario
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        private int _isIndividual { get; set; }
        public int isIndividual
        {
            get
            {
                return _isIndividual;
            }
            set
            {
                _isIndividual = value;
                OnPropertyChanged();
            }
        }
        public Timecard Timecard { get; set; }
        public CalendarViewModel(User usuarioFrom, DateTime fecha, int Individual, Timecard _timecard = null)
        {
            Usuario = usuarioFrom;
            FechaSeleccionada = fecha;
            isIndividual = Individual;
            Timecard = _timecard;
            Console.WriteLine(FechaSeleccionada);
        }
        public void EnviarUpdate()
        {
            //MessagingCenter.Send<object, DateTime>(this, "UpdateFecha", fecha);
            //MessagingCenter.Send(this, "UpdateFecha", FechaSeleccionada);
            if (isIndividual == 1)
            {
                MessagingCenter.Send<IndividualCardViewModel, DateTime>(new IndividualCardViewModel(Usuario, Timecard, FechaSeleccionada), "UpdateFechaInd", FechaSeleccionada);
            }else if (isIndividual == 2)
            {
                MessagingCenter.Send<IndividualTimeInOutViewModel, DateTime>(new IndividualTimeInOutViewModel(Usuario, Timecard, FechaSeleccionada), "UpdateFechaIndInOut", FechaSeleccionada);
            }
            else
            {
                MessagingCenter.Send<TimecardsViewModel, DateTime>(new TimecardsViewModel(Usuario), "UpdateFecha", FechaSeleccionada);
            }
            
        }
    }
}
