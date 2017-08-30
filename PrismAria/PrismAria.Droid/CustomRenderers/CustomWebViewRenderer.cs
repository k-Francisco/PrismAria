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
using Java.Lang;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace PrismAria.Droid.CustomRenderers
{
    public class CustomWebViewRenderer : WebViewRenderer, Android.Webkit.IValueCallback
    {
        public void OnReceiveValue(Java.Lang.Object value)
        {
           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            Android.Webkit.WebView webView = Control;
            if (webView == null)
                return;

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                Android.Webkit.CookieManager.Instance.RemoveAllCookie();
            else
                Android.Webkit.CookieManager.Instance.RemoveAllCookies(this);

            webView.ClearCache(true);
            webView.ClearHistory();
            webView.ClearFormData();
            webView.ClearMatches();

        }

        
    }
}