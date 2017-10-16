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
	public class BandArticlesPageViewModel : BindableBase
	{
        private Singleton _singleton;
        private ArticlesService service;

        public ObservableCollection<ArticlesModel> BandArticlesCollection
        {
            get { return _singleton.BandArticles; }
            set { SetProperty(ref _singleton.BandArticles, value); }
        }

        public BandArticlesPageViewModel()
        {
            _singleton = Singleton.Instance;
            service = new ArticlesService();
            for (int i = 0; i < 8; i++)
            {
                service.AddArticles(_singleton.BandArticles);
            }
        }
	}
}
