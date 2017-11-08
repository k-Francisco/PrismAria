using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.EventArguments;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Events;
using PrismAria.Helpers;
using PrismAria.Models;
using PrismAria.PopupPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class SongsPageViewModel : BindableBase, INavigatedAware
	{
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly IPageDialogService dialogService;
        private Album _album;
        private IMediaManager media = CrossMediaManager.Current;
        public bool isSubscriber { get; set; }

        private string _albumTitle;
        public string AlbumTitle
        {
            get { return _albumTitle; }
            set { SetProperty(ref _albumTitle, value); }
        }

        public ObservableCollection<Song> SongCollection
        {
            get { return Singleton.Instance.BandSongCollection; }
            set { SetProperty(ref Singleton.Instance.BandSongCollection, value); }
        }


        private DelegateCommand<Song> _openSongMenu;
        public DelegateCommand<Song> OpenSongMenu =>
            _openSongMenu ?? (_openSongMenu = new DelegateCommand<Song>(SongMenu));

        private async void SongMenu(Song obj)
        {
            var userPlaylistTitles = new ObservableCollection<string>();
            var userPlaylists = new List<PlaylistModel>();
            var playlists = JsonConvert.DeserializeObject<PlaylistModel[]>(await Singleton.Instance.webService.GetAllPlaylist());
            foreach(var item in playlists)
            {
                if (item.PlCreator.Equals(Settings.Token))
                {
                    userPlaylists.Add(item);
                    userPlaylistTitles.Add(item.PlTitle);
                }
            }


            if (Singleton.Instance.isSubscriber)
            {
                var choice = await dialogService.DisplayActionSheetAsync("Add to Playlist", "Close", "", userPlaylistTitles.ToArray());
                foreach(var item in userPlaylists)
                {
                    if (choice.Equals(item.PlTitle))
                    {
                        await Singleton.Instance.webService.AddSongToPlaylist(obj.SongId.ToString(), obj.GenreId.ToString(), item.PlId.ToString());
                        break;
                    }
                }
            }
            else
            {
                var choice2 = await dialogService.DisplayActionSheetAsync("Options", "Close", "", new string[] {
                    "Edit Song",
                    "Delete Song"
                });

                if(choice2.Equals("Edit Song"))
                {
                    Singleton.Instance.editIdentifier = 1;
                    Singleton.Instance.toBeModifiedSong = obj;
                    await PopupNavigation.Instance.PushAsync(new EditPopupPage(), true);
                }
                if(choice2.Equals("Delete Song"))
                {
                    var delete = await dialogService.DisplayAlertAsync("","Are you sure you want to delete this song?", "OK", "CANCEL");
                    if (delete)
                    {
                        await Singleton.Instance.webService.DeleteSong(obj.SongId.ToString());
                    }
                }
            }
            
        }

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(GoBack));

        private void GoBack()
        {
            navigationService.GoBackAsync();
        }

        private DelegateCommand _addSongCommand;
        public DelegateCommand AddSongCommand =>
            _addSongCommand ?? (_addSongCommand = new DelegateCommand(AddSong));

        private async void AddSong()
        {
            await PopupNavigation.Instance.PushAsync(new AddSongPopupPage());
        }


        private DelegateCommand<Song> _playSongCommand;
        public DelegateCommand<Song> PlaySongCommand =>
            _playSongCommand ?? (_playSongCommand = new DelegateCommand<Song>(PlaySong));

        //private Song lastSong;
        private async void PlaySong(Song obj)
        {
            if (Singleton.Instance.lastSong != null)
            {
                if (Singleton.Instance.lastSong.SongId != obj.SongId)
                {
                    await media.Pause();
                    media.MediaQueue.Clear();
                    Singleton.Instance.lastSong.IsPlaying = false;

                    Singleton.Instance.lastSong = obj;
                    Singleton.Instance.lastSong.IsPlaying = true;
                    var path = new System.Uri("/assets/music/" + obj.SongAudio).AbsolutePath;
                    await media.Play(Singleton.Instance.AriaUrl + path);
                }
                else
                {
                    if(Singleton.Instance.lastSong.IsPlaying == true)
                    {
                        Singleton.Instance.lastSong.IsPlaying = false;
                        await media.Pause();
                    }
                    else
                    {
                        Singleton.Instance.lastSong.IsPlaying = true;
                        await media.Play();
                    }
                    
                }
            }
            else
            {
                Singleton.Instance.lastSong = obj;
                var path = new System.Uri("/assets/music/" + obj.SongAudio).AbsolutePath;
                await media.Play(Singleton.Instance.AriaUrl + path);
                Singleton.Instance.lastSong.IsPlaying = true;
            }
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            _album = (Album)parameters["model"];
            AlbumTitle = _album.AlbumName;

            try
            {
                await Singleton.Instance.CollectionService.PopulateBandPageSongs(_album.AlbumId.ToString(), SongCollection);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public SongsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;
            eventAggregator.GetEvent<AddSongEvent>().Subscribe(AddSong);
            eventAggregator.GetEvent<EditSongEvent>().Subscribe(EditSong);
            isSubscriber = !Singleton.Instance.isSubscriber;
        }

        private void EditSong()
        {
            //Singleton.Instance.BandSongCollection.Clear();
            //await Singleton.Instance.CollectionService.PopulateBandPageSongs("0", SongCollection);
            var dummy = SongCollection;
            foreach (var item in dummy)
            {
                if(item.SongId == Singleton.Instance.toBeModifiedSong.SongId)
                {
                    item.SongTitle = Singleton.Instance.toBeModifiedSong.SongTitle;
                    item.SongDesc = Singleton.Instance.toBeModifiedSong.SongDesc;
                    item.GenreId = Singleton.Instance.toBeModifiedSong.GenreId;
                    
                    break;
                }
            }
        }

        private async void AddSong(SongModel obj)
        {
            if (await Singleton.Instance.webService.AddSongs(_album.AlbumId.ToString(), obj.SongDesc,obj.SongFile.DataArray,obj.GenreId, Singleton.Instance.currBandId.ToString(), obj.SongName))
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
    }
}
