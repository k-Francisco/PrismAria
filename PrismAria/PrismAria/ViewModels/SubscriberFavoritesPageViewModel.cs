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
        public ObservableCollection<BandModel> FavoritesCollection
        {
            get { return Singleton.Instance.FavoritesCollection; }
            set { SetProperty(ref Singleton.Instance.FavoritesCollection, value); }
        }

        private DelegateCommand<BandModel> _sampleCommand;
        public DelegateCommand<BandModel> SampleCommand =>
            _sampleCommand ?? (_sampleCommand = new DelegateCommand<BandModel>(Sample));

        private void Sample(BandModel obj)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("model", obj);
            navigationService.NavigateAsync(new Uri("SubscriberViewBandPage", UriKind.Relative), navigationParameters, false, true);
        }

        public SubscriberFavoritesPageViewModel(IEventAggregator ea, INavigationService navigationService)
        {
            _ea = ea;
            this.navigationService = navigationService;
        }
    }
}
