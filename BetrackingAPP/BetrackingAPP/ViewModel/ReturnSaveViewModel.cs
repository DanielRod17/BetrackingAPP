using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BetrackingAPP.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using BetrackingAPP.Views;

namespace BetrackingAPP.ViewModel
{
    public class ReturnSaveViewModel : BaseViewModel
    {
        public User Usuario { get; set; }
        private string _textContent;
        public string TextContent {
            get {
                    return _textContent;
            } set {
                _textContent = value;
                OnPropertyChanged();
            }
        }

        public ReturnSaveViewModel(User usuarioFrom, string Texto)
        {
            TextContent = Texto;
        }
    }
}
