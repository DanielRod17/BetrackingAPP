using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BetrackingAPP
{
    class CustomEntry : Entry
    {
        public CustomEntry()
        {
            PlaceholderColor = Color.FromHex("#f4f4f4");
            BackgroundColor = Color.Transparent;
        }
    }
}
