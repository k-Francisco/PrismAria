using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrismAria.Services;
using Xamarin.Forms;
using Prism.Navigation;
using Prism.Events;
using PrismAria.Events;
using System.Diagnostics;
using PrismAria.Models;
using PrismAria.Helpers;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Prism.Services;

namespace PrismAria.ViewModels
{
    public class FacebookLoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private IEventAggregator _ea;
        private readonly IPageDialogService pageDialogService;

        public WebViewSource ApiRequest { get; set; }
        private string _clientId = "1936064290001318";

        private DelegateCommand _closeLoginPageCommand;
        public DelegateCommand CloseLoginPageCommand =>
            _closeLoginPageCommand ?? (_closeLoginPageCommand = new DelegateCommand(CloseLoginPage));

        private void CloseLoginPage()
        {
            PopupNavigation.Instance.PopAllAsync(false);
        }

        public FacebookLoginPageViewModel(INavigationService navigationService, IEventAggregator ea, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _ea = ea;
            this.pageDialogService = pageDialogService;
            ApiRequest = "https://www.facebook.com/dialog/oauth?client_id="
                + _clientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
        }

	    public async Task SetFacebookUserProfileAsync(string accessToken)
	    {
            try {
                var webserve = new WebServices();
                var facebookServices = new FacebookLoginService();
                var Fbprofile = await facebookServices.GetFacebookProfileAsync(accessToken);

                Settings.Token = accessToken;
                Settings.Profile = JsonConvert.SerializeObject(Fbprofile);
                CloseLoginPage();
                if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
                }
                else
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            

            //bool register = await webserve.RegisterUser(Fbprofile.Id,
            //    Fbprofile.FirstName,
            //    Fbprofile.LastName,
            //    Fbprofile.Name,
            //    Fbprofile.Locale,
            //    Fbprofile.Picture.Data.Url);

            //if (register)
            //{
            //    Settings.Token = accessToken;
            //    Settings.Profile = JsonConvert.SerializeObject(Fbprofile);
            //    CloseLoginPage();
            //    if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
            //    {
            //        await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
            //    }
            //    else
            //    {
            //        await _navigationService.NavigateAsync(new Uri("http://myapp.com/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
            //    }
            //}
            //else
            //    await pageDialogService.DisplayAlertAsync("GAGO", "NAAY SAYOP GAGO", "OK");
        }
    }
}
