using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Events;
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
        private Album _album;
        private IMediaManager media = CrossMediaManager.Current;

        private string _albumTitle;
        public string AlbumTitle
        {
            get { return _albumTitle; }
            set { SetProperty(ref _albumTitle, value); }
        }

        //private ObservableCollection<Song> _songCollection = new ObservableCollection<Song>();
        public ObservableCollection<Song> SongCollection
        {
            get { return Singleton.Instance.BandSongCollection; }
            set { SetProperty(ref Singleton.Instance.BandSongCollection, value); }
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

        private async void PlaySong(Song obj)
        {
            var path = new System.Uri("/Aria/public/assets/music/" + obj.SongAudio).AbsolutePath;
            await media.Play("http://192.168.254.102"+path);
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

        public SongsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<AddSongEvent>().Subscribe(AddSong);
        }

        private async void AddSong(SongModel obj)
        {
            //System.Diagnostics.Debug.WriteLine(obj.SongName);
            //System.Diagnostics.Debug.WriteLine(obj.SongDesc);
            //System.Diagnostics.Debug.WriteLine(obj.GenreId);
            //System.Diagnostics.Debug.WriteLine(obj.SongFile.FileName);
            //System.Diagnostics.Debug.WriteLine(_album.AlbumId);
            //System.Diagnostics.Debug.WriteLine(Singleton.Instance.currBandId.ToString());
            if (await Singleton.Instance.webService.AddSongs(_album.AlbumId.ToString(), obj.SongDesc,obj.SongFile.DataArray,obj.GenreId, Singleton.Instance.currBandId.ToString(), obj.SongName))
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
    }
}
