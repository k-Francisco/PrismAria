using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PrismAria.Controls;
using PrismAria.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace PrismAria.iOS.CustomRenderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
        }
    }
}