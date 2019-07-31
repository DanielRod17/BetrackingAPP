using System;
using System.Collections.Generic;
using System.Text;
using BetrackingAPP.Models;
using Xamarin.Forms;

namespace BetrackingAPP.ViewModel
{
    public class MainPageViewModel
    {
        public User User { get; set; }
        public string Firstname { get; set; }
        public TimecardsViewModel TimecardsViewModel { set; get; }
        public ExpensesViewModel ExpensesViewModel { set; get; }
        public HomeViewModel HomeViewModel { set; get; }
        public SettingsViewModel SettingsViewModel { set; get; }
        //private User usuarioLoged;

        public MainPageViewModel(User usuario)
        {
            //usuarioLoged = usuario;
            TimecardsViewModel = new TimecardsViewModel (usuario);
            HomeViewModel = new HomeViewModel(usuario);
            ExpensesViewModel = new ExpensesViewModel(usuario);
            SettingsViewModel = new SettingsViewModel(usuario);
            //Application.Current.MainPage.DisplayAlert("Oops", Firstname, "OK");
        }
    }
}
