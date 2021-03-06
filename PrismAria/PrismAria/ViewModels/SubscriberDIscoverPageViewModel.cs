﻿using Prism.Commands;
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
using Newtonsoft.Json;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : ChildViewBaseModel
    {
       
        private Singleton _singleton;
        //private ObservableCollection<DiscoverPageModel> _discoverList;
        //public ObservableCollection<DiscoverPageModel> DiscoverList
        //{
        //    get { return _singleton.DiscoverCollection; }
        //    set { SetProperty(ref _singleton.DiscoverCollection, value); }
        //}
        private ObservableCollection<DiscoverPageModel> _discoverList = new ObservableCollection<DiscoverPageModel>();
        public ObservableCollection<DiscoverPageModel> DiscoverList
        {
            get { return _discoverList; }
            set { SetProperty(ref _discoverList, value); }
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
            //if (IsActive == false)
                //Debug.WriteLine("Discover Page Not anymore active");
            
        }

        private void HandleIsActive(object sender, EventArgs e)
        {
            if (IsActive)
            {
                if (DiscoverList.Count == 0)
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

        private async void PopulateCollection()
        {
            if (_isConnected)
            {
                DiscoverList.Clear();
                var pleaseWait = await _singleton.CollectionService.GenerateBandsToExplore(DiscoverList, _navigationService);
                if (!pleaseWait)
                {
                    await _pageDialogService.DisplayAlertAsync("Ooops!", "It seems like we encountered a problem", "Ok");
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
