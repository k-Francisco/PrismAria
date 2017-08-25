using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PrismAria.Controls;
using PrismAria.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace PrismAria.Droid.CustomRenderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".otf");
                Control.Typeface = font;
                Control.SetMaxLines(2);
            }
        }
    }
}