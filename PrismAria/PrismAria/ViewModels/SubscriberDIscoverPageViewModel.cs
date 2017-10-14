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
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using PrismAria.Services;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : BindableBase, INavigatedAware
    {

        private DiscoverListService _discoverService;
        private ObservableCollection<DiscoverPageModel> _discoverList;
        private Singleton _singleton;
        public ObservableCollection<DiscoverPageModel> DiscoverList
        {
            get { return _discoverList; }
            set { SetProperty(ref _discoverList, value); }
        }

        private IEventAggregator _ea;
        private readonly INavigationService _navigationService;

        public SubscriberDIscoverPageViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _ea = ea;
            _navigationService = navigationService;
            _discoverService = new DiscoverListService();
            _singleton = Singleton.Instance;
            _discoverList = _singleton.DiscoverCollection = _discoverService.GetDiscoverList(_navigationService);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

    }
}
