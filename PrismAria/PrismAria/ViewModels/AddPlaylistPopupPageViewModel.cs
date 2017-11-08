using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services;
using PrismAria.Events;
using PrismAria.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismAria.ViewModels
{
	public class AddPlaylistPopupPageViewModel : BindableBase
	{
        private MediaFile file;

        private string _playlistName;
        public string PlaylistName
        {
            get { return _playlistName; }
            set { SetProperty(ref _playlistName, value); }
        }

        private string _playlistDesc;
        public string PlaylistDesc
        {
            get { return _playlistDesc; }
            set { SetProperty(ref _playlistDesc, value); }
        }

        private ImageSource _playlistImage;
        public ImageSource PlaylistImage
        {
            get { return _playlistImage; }
            set { SetProperty(ref _playlistImage, value); }
        }

        private DelegateCommand _addPlaylistCommand;
        public DelegateCommand AddPlaylistCommand =>
            _addPlaylistCommand ?? (_addPlaylistCommand = new DelegateCommand(AddPlaylist));

        private async void AddPlaylist()
        {
            try
            {
                var playlist = JsonConvert.DeserializeObject<PlaylistModel>(await Singleton.Instance.webService.AddPlaylist(PlaylistName, PlaylistDesc, file));
                eventAggregator.GetEvent<AddPlaylistEvent>().Publish(playlist);
                Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                await pageDialogService.DisplayAlertAsync("Oops", "There is a problem adding a playlist", "OK");
                Close();
            }
        }

        private DelegateCommand _chooseImageCommand;
        public DelegateCommand ChooseImageCommand =>
            _chooseImageCommand ?? (_chooseImageCommand = new DelegateCommand(ChooseImage));

        private async void ChooseImage()
        {
            var image = await CrossMedia.Current.PickPhotoAsync();
            if (image == null)
                return;

            file = image;
            PlaylistImage = image.Path;
        }

        private DelegateCommand _closeCommand;
        private readonly IPageDialogService pageDialogService;
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private void Close()
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        public AddPlaylistPopupPageViewModel(IPageDialogService pageDialogService, IEventAggregator eventAggregator)
        {
            PlaylistImage = "sample_pic.png";
            this.pageDialogService = pageDialogService;
            this.eventAggregator = eventAggregator;
        }
	}
}
