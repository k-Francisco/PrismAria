using Prism.Commands;
using Prism.Mvvm;
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
        public ObservableCollection<ArticlesModel> Articles
        {
            get { return _articles; }
            set { SetProperty(ref _articles, value); }
        }

        public SubscriberFeedViewModel()
        {
            _service = new ArticlesService();
            _articles = _service.GetArticles();
        }
	}
}
