using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Events;
using PrismAria.Models;
using PrismAria.PopupPages;
using PrismAria.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class BandSongsAndAlbumsPageViewModel : ChildViewBaseModel
	{

        public ObservableCollection<Album> AlbumCollection
        {
            get { return Singleton.Instance.BandAlbumCollection; }
            set { SetProperty(ref Singleton.Instance.BandAlbumCollection, value); }
        }

        private DelegateCommand _addAlbumCommand;
        private readonly IEventAggregator eventAggregator;
        private readonly IPageDialogService pageDialogService;

        public DelegateCommand AddAlbumCommand =>
            _addAlbumCommand ?? (_addAlbumCommand = new DelegateCommand(AddAlbum));

        private void AddAlbum()
        {
            PopupNavigation.Instance.PushAsync(new AddAlbumPopupPage(), true);
        }


        private DelegateCommand<Album> _showOptionCommand;
        public DelegateCommand<Album> ShowOptionCommand =>
            _showOptionCommand ?? (_showOptionCommand = new DelegateCommand<Album>(ShowOption));

        private async void ShowOption(Album obj)
        {
            var choice = await pageDialogService.DisplayActionSheetAsync("", "Cancel", "", new string[] { "Edit Album", "Delete Album" } );
            if(choice.Equals("Delete Album"))
            {
                var delete = await pageDialogService.DisplayAlertAsync("", "Are you sure you want to delete this album?", "OK", "CANCEL");
                if (delete)
                {
                    if (!await Singleton.Instance.webService.DeleteAlbum(obj.AlbumId.ToString()))
                    {
                        await pageDialogService.DisplayAlertAsync("Oops!", "There was a problem deleting the album", "Ok");
                    }
                    else
                    {
                        await Singleton.Instance.CollectionService.PopulateBandPageAlbums(Singleton.Instance.currBandId.ToString(), AlbumCollection);
                    }
                }
            }
            if(choice.Equals("Edit Album"))
            {
                try
                {
                    Singleton.Instance.tobeModifiedAlbum = obj;
                    await PopupNavigation.Instance.PushAsync(new EditAlbumPopupPage(), true);
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                
            }
        }

        private DelegateCommand<Album> _albumTappedCommand;
        public DelegateCommand<Album> AlbumTappedCommand =>
            _albumTappedCommand ?? (_albumTappedCommand = new DelegateCommand<Album>(AlbumTapped));

        private async void AlbumTapped(Album obj)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("model", obj);
            try
            {
                await _navigationService.NavigateAsync("SongsPagez", parameters, true, true);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public BandSongsAndAlbumsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService):base(navigationService)
        {
            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleIsNotActive;
            this.eventAggregator = eventAggregator;
            this.pageDialogService = pageDialogService;
            eventAggregator.GetEvent<AddAlbumEvent>().Subscribe(AddAlbumVersion2);
            eventAggregator.GetEvent<EditAlbumEvent>().Subscribe(AddAlbumVersion2);
        }

        private async void AddAlbumVersion2()
        {
            await Singleton.Instance.CollectionService.PopulateBandPageAlbums(Singleton.Instance.currBandId.ToString(), AlbumCollection);
        }

        private void HandleIsNotActive(object sender, EventArgs e)
        {
            
        }

        private async void HandleIsActive(object sender, EventArgs e)
        {
            if (IsActive)
            {
                if(AlbumCollection.Count == 0)
                {
                    await Singleton.Instance.CollectionService.PopulateBandPageAlbums(Singleton.Instance.currBandId.ToString(), AlbumCollection);
                }
            }
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleIsNotActive;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;
        }
    }
}
