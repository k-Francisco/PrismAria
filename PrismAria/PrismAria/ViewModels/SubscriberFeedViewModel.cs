using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Events;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class SubscriberFeedViewModel : ChildViewBaseModel
	{
        private Singleton _singleton;
        private readonly IEventAggregator eventAggregator;
        private readonly IPageDialogService pageDialogService;

        public ObservableCollection<ArticlesModel> Articles
        {
            get { return _singleton.SubscriberArticlesCollection; }
            set { SetProperty(ref _singleton.SubscriberArticlesCollection, value); }
        }

        private bool _isListRefreshing;
        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { SetProperty(ref _isListRefreshing, value); }
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh));

        private async void Refresh()
        {
            IsListRefreshing = true;
            if (_isConnected)
            {
                _singleton.SubscriberArticlesCollection.Clear();
                if (await _singleton.CollectionService.GenerateArticlesForSubscriber())
                    IsListRefreshing = false;
                else
                    await pageDialogService.DisplayAlertAsync("Ooops!", "It seems like we encountered a problem", "Ok");
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Connectivity Issues", "Your device is not connected to the internet!", "OK");
                IsListRefreshing = false;
            }
                
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;

            Debug.WriteLine("Articles Page Initialized");
        }

        public SubscriberFeedViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService):base(navigationService)
        {
            _singleton = Singleton.Instance;
            this.eventAggregator = eventAggregator;
            this.pageDialogService = pageDialogService;
            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleNotIsActive;
        }

        private void HandleNotIsActive(object sender, EventArgs e)
        {
            if (IsActive == false)
                Debug.WriteLine("Articles Page Not anymore active");
        }

        private async void HandleIsActive(object sender, EventArgs e)
        {
            if (IsActive)
            {
                if (!_singleton.SubscriberArticlesCollection.Any())
                {
                    if (_isConnected)
                    {
                        IsListRefreshing = true;
                        if (await _singleton.CollectionService.GenerateArticlesForSubscriber())
                            IsListRefreshing = false;
                        else
                        {
                            await pageDialogService.DisplayAlertAsync("Ooops!", "It seems like we encountered a problem", "Ok");
                            IsListRefreshing = false;
                        }
                    }
                    else
                    {
                        await pageDialogService.DisplayAlertAsync("Connectivity Issues", "Your device is not connected to the internet!", "OK");
                        IsListRefreshing = false;
                    }
                        
                }
            }
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleNotIsActive;
        }
    }
}
