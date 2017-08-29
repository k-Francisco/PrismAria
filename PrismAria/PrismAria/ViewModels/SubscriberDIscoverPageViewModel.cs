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
    public class SubscriberDIscoverPageViewModel : BindableBase, INavigatingAware
    {
        private string _sampleText;
        private readonly IEventAggregator _ea;
        private bool HasInitialized { get; set; }

        public string SampleText
        {
            get { return _sampleText; }
            set { SetProperty(ref _sampleText, value); }
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<LoginEvent>().Subscribe(SetProfile);
        }

        private void SetProfile(FacebookProfile obj)
        {
            Debug.WriteLine(obj.Name);
            _sampleText = obj.Name;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized)
                return;

            HasInitialized = true;
        }
    }
}
