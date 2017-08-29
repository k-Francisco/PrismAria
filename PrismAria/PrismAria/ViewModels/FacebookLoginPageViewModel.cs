﻿using Prism.Commands;
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

namespace PrismAria.ViewModels
{
    public class FacebookLoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _ea;

        public FacebookLoginPageViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            _navigationService = navigationService;
            _ea = ea;
        }

	    public async Task SetFacebookUserProfileAsync(string accessToken)
	    {
	        var facebookServices = new FacebookLoginService();

	        var Fbprofile = await facebookServices.GetFacebookProfileAsync(accessToken);
            Debug.WriteLine(Fbprofile.Name);
            _ea.GetEvent<LoginEvent>().Publish(Fbprofile);
            //await _navigationService.GoBackAsync();
            await _navigationService.NavigateAsync("RootPage/SubscriberLanding", null, true, false);

	    }
    }

    public class RootPage : NavigationPage
    {
        public RootPage()
        {
            this.BarTextColor = Color.FromHex("#2C3E50");

        }
    }
}
