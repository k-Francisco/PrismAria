using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Events;
using PrismAria.PopupPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismAria.ViewModels
{
	public class AddAlbumPopupPageViewModel : BindableBase
	{
        private MediaFile _mediaFile;

        private string _albumName;
        public string AlbumName
        {
            get { return _albumName; }
            set { SetProperty(ref _albumName, value); }
        }

        private string _albumDesc;
        public string AlbumDesc
        {
            get { return _albumDesc; }
            set { SetProperty(ref _albumDesc, value); }
        }


        private ImageSource _albumPic;
        public ImageSource AlbumPic
        {
            get { return _albumPic; }
            set { SetProperty(ref _albumPic, value); }
        }

        private DelegateCommand _pickPhotoCommand;
        public DelegateCommand PickPhotoCommand =>
            _pickPhotoCommand ?? (_pickPhotoCommand = new DelegateCommand(PickPhoto));

        private async void PickPhoto()
        {
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;

            AlbumPic = ImageSource.FromFile(_mediaFile.Path);
        }

        private DelegateCommand _closePopupCommand;
        public DelegateCommand ClosePopupCommand =>
            _closePopupCommand ?? (_closePopupCommand = new DelegateCommand(ClosePopup));

        private void ClosePopup()
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        private DelegateCommand _createAlbumCommand;
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand CreateAlbumCommand =>
            _createAlbumCommand ?? (_createAlbumCommand = new DelegateCommand(CreateAlbum));

        private async void CreateAlbum()
        {
            await PopupNavigation.Instance.PushAsync(new LoadingPopupPage());
            if(await Singleton.Instance.webService.AddAlbum(AlbumName, AlbumDesc, _mediaFile, Singleton.Instance.currBandId.ToString()))
            {
                await PopupNavigation.Instance.PopAllAsync();
                eventAggregator.GetEvent<AddAlbumEvent>().Publish();
            }
        }

        public AddAlbumPopupPageViewModel(IEventAggregator eventAggregator)
        {
            AlbumPic = "sample_pic.png";
            this.eventAggregator = eventAggregator;
        }
	}
}
