using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class SubscriberViewBandPageViewModel : BindableBase
	{
        private readonly INavigationService navigationService;

        private ObservableCollection<BandPagePopularModel> _popularList = new ObservableCollection<BandPagePopularModel>();
        public ObservableCollection<BandPagePopularModel> PopularList
        {
            get { return _popularList; }
            set { SetProperty(ref _popularList, value); }
        }

        private ObservableCollection<BandPageAlbum> _albumList = new ObservableCollection<BandPageAlbum>();
        public ObservableCollection<BandPageAlbum> AlbumList
        {
            get { return _albumList; }
            set { SetProperty(ref _albumList, value); }
        }

        private DelegateCommand _closeBandPageCommand;
        

        public DelegateCommand CloseBandPageCommand =>
            _closeBandPageCommand ?? (_closeBandPageCommand = new DelegateCommand(CloseBandPage));

        private void CloseBandPage()
        {
            navigationService.GoBackAsync();
        }

        private int _popularHeight;
        public int PopularHeight
        {
            get { return _popularHeight; }
            set { SetProperty(ref _popularHeight, value); }
        }

        private int _albumHeight;
        public int AlbumHeight
        {
            get { return _albumHeight; }
            set { SetProperty(ref _albumHeight, value); }
        }


        public SubscriberViewBandPageViewModel(INavigationService navigationService)
        {
            for (int i = 0; i < 3; i++)
            {
                _popularList.Add(new BandPagePopularModel()
                {
                    SongAlbum = "sample_pic.png",
                    SongName = "Lami ilaba",
                    SongListenedCount = "23,000,032"
                });
            }

            _popularHeight = 3 * 80;

            for (int i = 0; i < 4; i++)
            {
                _albumList.Add(new BandPageAlbum()
                {
                    AlbumPic = "sample_pic.png",
                    AlbumTitle = "Album Title",
                    AlbumLikes = "15,000 liked this"
                });
            }

            _albumHeight = (4 / 2) * 150;

            this.navigationService = navigationService;
        }
    }
}
