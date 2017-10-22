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
using Prism.Services;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : ChildViewBaseModel
    {
       
        private Singleton _singleton;
        public ObservableCollection<DiscoverPageModel> DiscoverList
        {
            get { return _singleton.DiscoverCollection; }
            set { SetProperty(ref _singleton.DiscoverCollection, value); }
        }

        private bool _isListRefreshing;
        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { SetProperty(ref _isListRefreshing, value); }
        }

        private IEventAggregator _ea;
        private readonly IPageDialogService _pageDialogService;

        

        private void HandleNotIsActive(object sender, EventArgs e)
        {
            if (IsActive == false)
                Debug.WriteLine("Discover Page Not anymore active");
            
        }

        private void HandleIsActive(object sender, EventArgs e)
        {
            if (IsActive)
            {
                if (_singleton.DiscoverCollection.Count == 0)
                {
                    IsListRefreshing = true;
                    PopulateCollection();
                }
                else
                {
                    return;
                }
            }

        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));

        private void Refresh()
        {
            IsListRefreshing = true;
            PopulateCollection();
        }


        private DelegateCommand<DiscoverPageModel> _sampleCommand;
        public DelegateCommand<DiscoverPageModel> SampleCommand =>
            _sampleCommand ?? (_sampleCommand = new DelegateCommand<DiscoverPageModel>(Sample));

        private void Sample(DiscoverPageModel obj)
        {
            Debug.WriteLine(obj.categoryName);
        }

        private async void PopulateCollection()
        {
            if (_isConnected)
            {
                _singleton.DiscoverCollection.Clear();
                var pleaseWait = await _singleton.CollectionService.GenerateBandsToExplore(_singleton.DiscoverCollection, _navigationService);
                if (pleaseWait == true)
                {
                   
                    Debug.WriteLine("refreshing should hide");
                }
                else
                {
                  
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Connectivity issues", "Cannot load because your device is not connected to the internet", "ok");
                
            }
            IsListRefreshing = false;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;

            Debug.WriteLine("Discover Page Initialized");
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleNotIsActive;
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea, INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _ea = ea;
            _pageDialogService = pageDialogService;
            _singleton = Singleton.Instance;

            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleNotIsActive;
        }
    }
}
