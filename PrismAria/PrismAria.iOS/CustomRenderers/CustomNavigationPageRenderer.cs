using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using PrismAria.Controls;
using PrismAria.iOS.CustomRenderers;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace PrismAria.iOS.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null) {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("BryantBoldAlt Regular", 24);
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }
    }
}