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
        public User Usuario {get; set;}
        public Reports Reporte { get; set; }
        public AddExpensesViewModel(Reports reporte, User usuario)
        {
            Usuario = usuario;
            Reporte = reporte;
        }
    }
}
