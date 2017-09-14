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
        private IEventAggregator _ea;
        private ObservableCollection<DiscoverPageModel> _discoverList;
        public ObservableCollection<DiscoverPageModel> DiscoverList
        {
            get { return _discoverList; }
            set { SetProperty(ref _discoverList, value); }
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _discoverService = new DiscoverListService();
            _discoverList = _discoverService.GetDiscoverList();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

    }
}
