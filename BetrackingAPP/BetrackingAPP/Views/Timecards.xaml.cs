﻿using BetrackingAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BetrackingAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Timecards : ContentPage
    {
        public TimecardsViewModel ViewModel { get; set; }
        public Timecards()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
                case Device.Android:
                case Device.UWP:
                default:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }
            InitializeComponent();
            /*switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
                default:
                    NavigationPage.SetHasNavigationBar(this, true);
                    break;
            }*/
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var pippo = this.FindByName<ListView>("TimecardsList");
            pippo.SelectedItem = null;
            var vm = BindingContext as TimecardsViewModel;
            _ = vm.LoadTimecards(vm.usuario, vm.FechaSeleccionada);
        }

        private async void ViewCell_Appearing(object sender, EventArgs e)
        {
            var cell = sender as ViewCell;
            var view = cell.View;
            view.TranslationX = -100;
            view.Opacity = 0;
            await Task.WhenAny<bool>
            (
                view.TranslateTo(0, 0, 250, Easing.SinIn),
                view.FadeTo(1, 500, Easing.BounceIn)
            );
        }

    }
}