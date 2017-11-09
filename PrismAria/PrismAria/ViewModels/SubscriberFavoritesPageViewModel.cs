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

        private ObservableCollection<PlaylistModel> _FollowedPlaylists;
        public ObservableCollection<PlaylistModel> FollowedPlaylists
        {
            get { return _FollowedPlaylists; }
            set { SetProperty(ref _FollowedPlaylists, value); }
        }

        private DelegateCommand _addPlaylistCommand;
        public DelegateCommand AddPlaylistCommand =>
            _addPlaylistCommand ?? (_addPlaylistCommand = new DelegateCommand(AddPlaylist));

        private void AddPlaylist()
        {
            PopupNavigation.Instance.PushAsync(new AddPlaylistPopupPage());
        }

        private DelegateCommand<PlaylistModel> _viewPlaylistCommand;
        public DelegateCommand<PlaylistModel> ViewMyPlaylistCommand =>
            _viewPlaylistCommand ?? (_viewPlaylistCommand = new DelegateCommand<PlaylistModel>(ViewMyPlaylist));

        private void ViewMyPlaylist(PlaylistModel obj)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("model", obj);

            _navigationService.NavigateAsync("ViewAllSongsInPlaylistPage", parameters, true, true);
        }

        private DelegateCommand _viewAllPlaylistCommand;
        public DelegateCommand ViewAllPlaylistCommand =>
            _viewAllPlaylistCommand ?? (_viewAllPlaylistCommand = new DelegateCommand(ViewAllPlaylist));

        private void ViewAllPlaylist()
        {
            _navigationService.NavigateAsync("ViewAllPlaylistPage", null,true,true);
        }

        public SubscriberFavoritesPageViewModel(IEventAggregator ea, INavigationService navigationService):base(navigationService)
        {
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
            if (IsActive)
            {
                if(!MyPlaylists.Any())
                    Singleton.Instance.CollectionService.PopulateMyPlaylist(MyPlaylists);
            }
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;

            Debug.WriteLine("Favorites Page Initialized");
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleNotIsActive;
        }
    }

}
