using Prism.Commands;
using Prism.Mvvm;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class BandSongsAndAlbumsPageViewModel : BindableBase
	{
        private Singleton _singleton;
        private BandSongAndAlbumService service;
        public ObservableCollection<BandPagePopularModel> SongCollection
        {
            get { return _singleton.SongCollection; }
            set { SetProperty(ref _singleton.SongCollection, value); }
        }

        public ObservableCollection<BandPageAlbum> AlbumCollection
        {
            get { return _singleton.AlbumCollection; }
            set { SetProperty(ref _singleton.AlbumCollection, value); }
        }

        private int _songCollectionHeight;
        public int SongCollectionHeight
        {
            get { return _songCollectionHeight; }
            set { SetProperty(ref _songCollectionHeight, value); }
        }

        private int _albumCollectionHeight;
        public int AlbumCollectionHeight
        {
            get { return _albumCollectionHeight; }
            set { SetProperty(ref _albumCollectionHeight, value); }
        }


        public BandSongsAndAlbumsPageViewModel()
        {
            _singleton = Singleton.Instance;
            service = new BandSongAndAlbumService();

            for (int i = 0; i < 5; i++)
            {
                service.AddSongs(_singleton.SongCollection);
                service.AddAlbum(_singleton.AlbumCollection);
            }
            _songCollectionHeight = _singleton.SongCollection.Count * 50;
            _albumCollectionHeight = (_singleton.AlbumCollection.Count / 2) * 150;
        }
	}
}
