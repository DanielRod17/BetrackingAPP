using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using BetrackingAPP;
using MBAutoComplete;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xfx;
using Xfx.Controls.iOS.Renderers;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Xfx.Controls.iOS.Controls;
using Xfx.Controls.iOS.Extensions;


[assembly: ExportRenderer(typeof(XfxCustomized), typeof(XfxCustom))]
namespace BetrackingAPP
{
	public class XfxCustom:XfxComboBoxRendererTouch
	{
        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				SetElement(null); //trigger elementchange before base dispose happens https://github.com/XamFormsExtended/Xfx.Controls/issues/97
			}

			base.Dispose(disposing);
		}
        private MbAutoCompleteTextField NativeControl => (MbAutoCompleteTextField)Control;
        private XfxComboBox ComboBox => (XfxComboBox)Element;
        protected override void OnElementChanged(ElementChangedEventArgs<XfxEntry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                // unsubscribe
                NativeControl.AutoCompleteViewSource.Selected -= AutoCompleteViewSourceOnSelected;
                var elm = (XfxComboBox)e.OldElement;
                elm.CollectionChanged -= ItemsSourceCollectionChanged;
            }
        }
        private void AutoCompleteViewSourceOnSelected(object sender, XfxSelectedItemChangedEventArgs args)
        {
            //ComboBox.OnItemSelectedInternal(Element, args);
            // TODO : Florell, Chase (Contractor) 02/15/17 SET FOCUS
        }
        private void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            SetItemsSource();
        }
        private void SetItemsSource()
        {
            var items = ComboBox.ItemsSource.ToList();
            NativeControl.UpdateItems(items);
        }
    }
}
