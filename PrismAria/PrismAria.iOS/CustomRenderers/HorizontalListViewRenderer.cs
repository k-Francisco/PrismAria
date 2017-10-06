using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using PrismAria.Controls;
using PrismAria.iOS.CustomRenderers;

[assembly: ExportRenderer(typeof(HorizontalListView), typeof(HorizontalListViewRenderer))]
namespace PrismAria.iOS.CustomRenderers
{
    public class HorizontalListViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.ShowsHorizontalScrollIndicator = false;
            var element = e.NewElement as HorizontalListView;
            this.ShowsHorizontalScrollIndicator = false;
            this.ShowsVerticalScrollIndicator = false;
            element?.Render();

        }
    }
}