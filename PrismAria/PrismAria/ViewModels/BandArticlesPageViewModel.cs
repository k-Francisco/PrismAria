using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class BandArticlesPageViewModel : ChildViewBaseModel
	{
        private Singleton _singleton;
        //private ArticlesService service;

        public ObservableCollection<ArticlesModel> BandArticlesCollection
        {
            get { return _singleton.BandArticlesCollection; }
            set { SetProperty(ref _singleton.BandArticlesCollection, value); }
        }

        public BandArticlesPageViewModel(INavigationService navigationService):base(navigationService)
        {
            _singleton = Singleton.Instance;
            //service = new ArticlesService();
            //_singleton.CollectionService.AddBandArticles(_singleton.BandArticlesCollection);
        }
	}
}
