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
using System.Linq;

namespace PrismAria.ViewModels
{
	public class SubscriberFeedViewModel : BindableBase
	{
        private ArticlesService _service;
        private ObservableCollection<ArticlesModel> _articles;
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public ObservableCollection<ArticlesModel> Articles
        {
            get { return _articles; }
            set { SetProperty(ref _articles, value); }
        }

        public SubscriberFeedViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _service = new ArticlesService();
            _articles = _service.GetArticles();
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
        }
	}
}
