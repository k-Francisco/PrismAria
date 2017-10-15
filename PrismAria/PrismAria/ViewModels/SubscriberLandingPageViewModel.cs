using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using PrismAria.Events;
using PrismAria.Models;
using System.Diagnostics;

namespace PrismAria.ViewModels
{
    public class SubscriberLandingPageViewModel : BindableBase, INavigatedAware
    {
        private IEventAggregator _ea;
        private readonly INavigationService navigationService;

        public SubscriberLandingPageViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _ea = ea;
            this.navigationService = navigationService;
           
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
