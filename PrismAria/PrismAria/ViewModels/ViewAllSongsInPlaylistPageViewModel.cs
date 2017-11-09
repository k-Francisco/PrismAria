using Newtonsoft.Json;
using Plugin.MediaManager;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PrismAria.ViewModels
{
	public class ViewAllSongsInPlaylistPageViewModel : BindableBase, INavigatedAware
	{
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        private PlaylistModel playlist;

        private ObservableCollection<Song> _playlistSongs = new ObservableCollection<Song>();
        public ObservableCollection<Song> PlaylistSongs
        {
            get { return _playlistSongs; }
            set { SetProperty(ref _playlistSongs, value); }
        }

        private ImageSource _playlistPic;
        public ImageSource PlaylistPic
        {
            get { return _playlistPic; }
            set { SetProperty(ref _playlistPic, value); }
        }

        private string _playlistName;
        public string PlaylistName
        {
            get { return _playlistName; }
            set { SetProperty(ref _playlistName, value); }
        }

        private string _creatorName;
        public string CreatorName
        {
            get { return _creatorName; }
            set { SetProperty(ref _creatorName, value); }
        }

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(GoBack));

        private void GoBack()
        {
            navigationService.GoBackAsync();
        }

        private DelegateCommand<Song> _openSongMenuCommand;
        public DelegateCommand<Song> OpenSongMenuCommand =>
            _openSongMenuCommand ?? (_openSongMenuCommand = new DelegateCommand<Song>(OpenSongMenu));

        private async void OpenSongMenu(Song obj)
        {
            var choice = await pageDialogService.DisplayActionSheetAsync("Options", "Close", "", new string[] {
                    "Remove from Playlist",
                    "Go to Band Page"
                }); 

            if(choice.Equals("Remove from Playlist"))
            {
                var remove = await pageDialogService.DisplayAlertAsync("","Do you really want to remove this song from the playlist?", "OK", "CANCEL");
                if (remove)
                {
                    if(await Singleton.Instance.webService.RemoveSongFromPlaylist(playlist.PlId.ToString(), obj.SongId.ToString()))
                    {
                        PlaylistSongs.Remove(obj);
                    }
                }
            }
        }


        private DelegateCommand<Song> _playSongCommand;
        public DelegateCommand<Song> PlaySongCommand =>
            _playSongCommand ?? (_playSongCommand = new DelegateCommand<Song>(PlaySong));

        private async void PlaySong(Song obj)
        {
            await CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.MediaQueue.Clear();
            var path = new System.Uri("/assets/music/" + obj.SongAudio).AbsolutePath;
            await CrossMediaManager.Current.Play(Singleton.Instance.AriaUrl + path);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            playlist = (PlaylistModel)parameters["model"];
            PlaylistPic = playlist.Image;
            PlaylistName = playlist.PlTitle;
            var users = JsonConvert.DeserializeObject<UserModel[]>(await Singleton.Instance.webService.GetUsers());
            if (users.Any())
            {
                foreach(var item in users)
                {
                    if (item.UserId.Equals(playlist.PlCreator)) {
                        CreatorName = item.Fullname;
                    }
                }
            }
            Singleton.Instance.CollectionService.PopulateSongsInPlaylist(PlaylistSongs, playlist.PlId);
        }

        public ViewAllSongsInPlaylistPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;

            CrossMediaManager.Current.MediaFinished += (sender, e) => {
                System.Diagnostics.Debug.WriteLine("finish na ang boang");
            };
            
        }
	}
}
