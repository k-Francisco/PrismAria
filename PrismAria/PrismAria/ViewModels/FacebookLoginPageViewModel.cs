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
using PrismAria.Models;

namespace PrismAria.ViewModels
{
    public class FacebookLoginPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private IEventAggregator _ea;
        public string UserPhoto;

        public FacebookLoginPageViewModel(INavigationService navigationService, IEventAggregator ea)
        {
            _navigationService = navigationService;
            _ea = ea;
        }

	    public async Task SetFacebookUserProfileAsync(string accessToken)
	    {
	        var facebookServices = new FacebookLoginService();
            var Fbprofile = await facebookServices.GetFacebookProfileAsync(accessToken);
            UserPhoto = Fbprofile.Picture.Data.Url;
            //_ea.GetEvent<LoginEvent>().Publish(Fbprofile);
            var navigationParams = new NavigationParameters();
            navigationParams.Add("profile", Fbprofile);
            await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/SubscriberLanding/Discover", UriKind.Absolute), navigationParams, true, false);
        }
    }
}