using PrismAria.Controls;
using PrismAria.ViewModels;
using Rg.Plugins.Popup.Pages;
using System.Diagnostics;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class FacebookLoginPage : PopupPage
    {

        public FacebookLoginPage()
        {
            InitializeComponent();

            webby.Navigated += WebViewOnNavigated;
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
