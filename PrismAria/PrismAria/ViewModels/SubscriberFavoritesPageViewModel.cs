using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Events;
using PrismAria.Helpers;
using PrismAria.Models;
using PrismAria.PopupPages;
using PrismAria.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
    public class SubscriberFavoritesPageViewModel : ChildViewBaseModel
    {
        private IEventAggregator _ea;

        public ObservableCollection<BandModel> FavoritesCollection
        {
            get { return Singleton.Instance.FavoritesCollection; }
            set { SetProperty(ref Singleton.Instance.FavoritesCollection, value); }
        }

        public ObservableCollection<PlaylistModel> MyPlaylists
        {
            get { return Singleton.Instance.UserPlaylists; }
            set { SetProperty(ref Singleton.Instance.UserPlaylists, value); }
        }

        private DelegateCommand _addPlaylistCommand;
        public DelegateCommand AddPlaylistCommand =>
            _addPlaylistCommand ?? (_addPlaylistCommand = new DelegateCommand(AddPlaylist));

        private void AddPlaylist()
        {
            PopupNavigation.Instance.PushAsync(new AddPlaylistPopupPage());
        }

        public SubscriberFavoritesPageViewModel(IEventAggregator ea, INavigationService navigationService):base(navigationService)
        {
            Singleton.Instance.CollectionService.PopulateMyPlaylist(MyPlaylists);
            _ea = ea;
            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleNotIsActive;
            _ea.GetEvent<AddPlaylistEvent>().Subscribe(AddPlaylistFromEvent);
        }

        private void AddPlaylistFromEvent(PlaylistModel obj)
        {
            MyPlaylists.Add(obj);
        }

        private void HandleNotIsActive(object sender, EventArgs e)
        {
            
        }

        private void HandleIsActive(object sender, EventArgs e)
        {
            
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;

            Debug.WriteLine("Favorites Page Initialized");
        }
    }
}
