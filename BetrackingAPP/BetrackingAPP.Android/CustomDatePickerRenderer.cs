using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BetrackingAPP;
using BetrackingAPP.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace BetrackingAPP.Droid
{
    class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.Mutate();
                gd.SetShape(0);
                //gd.SetCornerRadius(5);
                gd.SetStroke(5, global::Android.Graphics.Color.DarkGray);
                gd.SetCornerRadii(new float[] { 15, 15, 15, 15, 15, 15, 15, 15 });
                gd.SetTint(global::Android.Graphics.Color.White);
                gd.SetColor(global::Android.Graphics.Color.DarkGray);
                Control.SetBackgroundDrawable(gd);
                //Control.SetBackgroundColor(global::Android.Graphics.Color.White);
            }
        }
        /*protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            base.CreateDatePickerDialog(year, month, day);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.Mutate();
                gd.SetShape(0);
                //gd.SetCornerRadius(5);
                gd.SetStroke(5, global::Android.Graphics.Color.DarkGray);
                gd.SetCornerRadii(new float[] { 15, 15, 15, 15, 15, 15, 15, 15 });
                gd.SetTint(global::Android.Graphics.Color.White);
                gd.SetColor(global::Android.Graphics.Color.DarkGray);
                Control.SetBackgroundDrawable(gd);
                //Control.SetBackgroundColor(global::Android.Graphics.Color.White);
            }
            return base.CreateDatePickerDialog(year, month, day);
        }*/
    }
}