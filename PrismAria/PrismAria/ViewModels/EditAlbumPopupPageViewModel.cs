using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services;
using PrismAria.Events;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class EditAlbumPopupPageViewModel : BindableBase
	{

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

        private DelegateCommand _editAlbumCommand;
        public DelegateCommand EditAlbumCommand =>
            _editAlbumCommand ?? (_editAlbumCommand = new DelegateCommand(EditAlbum));

        private async void EditAlbum()
        {
            if (await Singleton.Instance.webService.EditAlbum(AlbumName, AlbumDesc, Singleton.Instance.tobeModifiedAlbum.AlbumId.ToString(), Singleton.Instance.currBandId.ToString()))
            {
                eventAggregator.GetEvent<EditAlbumEvent>().Publish();
                Close();
            }
            else
            {
                Close();
                await pageDialogService.DisplayAlertAsync("Oops!", "There was a problem editing the album", "ok");
            }
        }

        private DelegateCommand _closeCommand;
        private readonly IEventAggregator eventAggregator;
        private readonly IPageDialogService pageDialogService;

        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private void Close()
        {
            PopupNavigation.Instance.PopAllAsync();
        }


        public EditAlbumPopupPageViewModel(IEventAggregator eventAggregator, IPageDialogService pageDialogService)
        {
            AlbumName = Singleton.Instance.tobeModifiedAlbum.AlbumName;
            AlbumDesc = Singleton.Instance.tobeModifiedAlbum.AlbumDesc;
            this.eventAggregator = eventAggregator;
            this.pageDialogService = pageDialogService;
        }
    }
}
