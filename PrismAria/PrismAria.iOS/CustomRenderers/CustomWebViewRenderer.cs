using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace PrismAria.iOS.CustomRenderers
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var cookies = NSHttpCookieStorage.SharedStorage.Cookies;
            foreach (NSHttpCookie cookie in cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }

            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            NSUrlCache.SharedCache.MemoryCapacity = 0;
            NSUrlCache.SharedCache.DiskCapacity = 0;
        }
    }
}