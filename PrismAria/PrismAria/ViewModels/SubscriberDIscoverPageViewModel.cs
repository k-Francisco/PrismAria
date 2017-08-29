using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using PrismAria.Models;
using System.Diagnostics;
using Prism.Navigation;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : BindableBase, INavigatedAware
    {
        private string _sampleText;
        private IEventAggregator _ea;
        private FacebookProfile _profile;

        public string SampleText
        {
            get { return _sampleText; }
            set { SetProperty(ref _sampleText, value); }
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }

        private void SetProfile()
        {
            SampleText = _profile.Name;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _profile = (FacebookProfile)parameters["profile"];
            SetProfile();
        }
    }
}
