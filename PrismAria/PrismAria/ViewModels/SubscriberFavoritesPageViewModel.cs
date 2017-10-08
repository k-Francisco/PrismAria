using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
    public class SubscriberFavoritesPageViewModel : BindableBase
    {
        private IEventAggregator _ea;
        private readonly INavigationService navigationService;
        private FavoritesListService _favoritesService;
        private ObservableCollection<FavoritesModel> _favoritesCollection;
        public ObservableCollection<FavoritesModel> FavoritesCollection
        {
            get { return _favoritesCollection; }
            set { SetProperty(ref _favoritesCollection, value); }
        }

        private DelegateCommand<FavoritesModel> _sampleCommand;
        public DelegateCommand<FavoritesModel> SampleCommand =>
            _sampleCommand ?? (_sampleCommand = new DelegateCommand<FavoritesModel>(Sample));

        private void Sample(FavoritesModel obj)
        {
            if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
                navigationService.NavigateAsync("SubscriberViewBandPage", null, false, true);
            else
                navigationService.NavigateAsync("SubscriberViewBandPage", null, true, true);
        }

        public SubscriberFavoritesPageViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _ea = ea;
            this.navigationService = navigationService;
            _favoritesService = new FavoritesListService();
            _favoritesCollection = _favoritesService.GetFavorites();
        }
    }
}
