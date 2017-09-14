using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
    public class SubscriberFavoritesPageViewModel : BindableBase
    {
        private IEventAggregator _ea;
        private FavoritesListService _favoritesService;
        private ObservableCollection<FavoritesModel> _favoritesCollection;
        public ObservableCollection<FavoritesModel> FavoritesCollection
        {
            get { return _favoritesCollection; }
            set { SetProperty(ref _favoritesCollection, value); }
        }

        public SubscriberFavoritesPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _favoritesService = new FavoritesListService();
            _favoritesCollection = _favoritesService.GetFavorites();
        }
    }
}
