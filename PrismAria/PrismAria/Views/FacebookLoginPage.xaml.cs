using PrismAria.Controls;
using PrismAria.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class FacebookLoginPage : ContentPage
    {
        private string _clientId = "1936064290001318";
        public FacebookLoginPage()
        {
            InitializeComponent();
            WebViewSource apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + _clientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new CustomWebView
            {
                Source = apiRequest,
                HeightRequest = 1,
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookLoginPageViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
