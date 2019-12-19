using System;
using Xfx;
namespace BetrackingAPP
{
    public class XfxCustomized : XfxComboBox
    {
        protected override void OnItemSelected(XfxSelectedItemChangedEventArgs args)
        {
            base.OnItemSelected(args);
            this.Text = args.SelectedItem.ToString();
        }
    }
}