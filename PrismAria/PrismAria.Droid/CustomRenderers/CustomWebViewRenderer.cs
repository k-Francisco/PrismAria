using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PrismAria.Controls;
using PrismAria.Droid.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace PrismAria.Droid.CustomRenderers
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            Control.ClearCache(true);
            Control.Settings.SetAppCacheEnabled(false);
            Control.Settings.CacheMode = Android.Webkit.CacheModes.NoCache;
            Control.ClearHistory();
        }

        
    }
}