
using System;

using Xamarin.Forms;
using System.Globalization;
using System.Diagnostics;

namespace BetrackingAPP
{
    class decimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valor = (decimal)value;
            return valor.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valor = (string)value;
            return valor;
        }
    }
}
