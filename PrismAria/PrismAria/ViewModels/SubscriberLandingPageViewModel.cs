using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using PrismAria.Events;
using PrismAria.Models;

namespace PrismAria.ViewModels
{
    public class SubscriberLandingPageViewModel : BaseViewModel
    {
        private IEventAggregator _ea;

        public SubscriberLandingPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }

        
    }
}
