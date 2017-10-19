using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
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
       
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public ObservableCollection<ArticlesModel> Articles
        {
            get { return _singleton.SubscriberArticlesCollection; }
            set { SetProperty(ref _singleton.SubscriberArticlesCollection, value); }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;

            Debug.WriteLine("Articles Page Initialized");
        }

        public SubscriberFeedViewModel(INavigationService navigationService, IEventAggregator eventAggregator):base(navigationService)
        {
            _singleton = Singleton.Instance;
            this.eventAggregator = eventAggregator;

            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleNotIsActive;
        }

        private void HandleNotIsActive(object sender, EventArgs e)
        {
            if (IsActive == false)
                Debug.WriteLine("Articles Page Not anymore active");
        }

        private void HandleIsActive(object sender, EventArgs e)
        {
            if (IsActive)
                _singleton.CollectionService.GenerateArticlesForSubscriber(_singleton.SubscriberArticlesCollection);
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleNotIsActive;
        }
    }
}
