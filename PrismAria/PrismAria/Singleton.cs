﻿using Plugin.MediaManager.Abstractions.EventArguments;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria
{
    public sealed class Singleton 
    {

        private static volatile Singleton _instance;
        private static readonly object _syncLock = new object();

        private Singleton() {

        }

        public static Singleton Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_syncLock)
                {
                    if(_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }

                return _instance;
            }
        }

        #region Observable Collections
        public ObservableCollection<DiscoverPageModel> DiscoverCollection = new ObservableCollection<DiscoverPageModel>() { };
        public ObservableCollection<ArticlesModel> SubscriberArticlesCollection = new ObservableCollection<ArticlesModel>() { };
        public ObservableCollection<BandModel> FavoritesCollection = new ObservableCollection<BandModel>() { };
        public ObservableCollection<UserBandModelForEvent> UserBandCollection = new ObservableCollection<UserBandModelForEvent>() { };
        public ObservableCollection<Member> BandMemberCollection = new ObservableCollection<Member>() { };
        public ObservableCollection<ArticlesModel> BandArticlesCollection = new ObservableCollection<ArticlesModel>() { };
        public ObservableCollection<BandPagePopularModel> SongCollection = new ObservableCollection<BandPagePopularModel>() { };
        public ObservableCollection<BandPageAlbum> AlbumCollection = new ObservableCollection<BandPageAlbum>() { };
        #endregion


        #region Services
        public string AriaUrl = "http://192.168.254.106/Aria/public";
        public CollectionService CollectionService = new CollectionService();
        public WebServices webService = new WebServices();
        public MediaFileChangedEventArgs MediaFileArgs;
        #endregion

        #region User Preferences
        public UserPreferenceModel[] userPreference;
        public BandModel recentlyViewedBand;
        public ObservableCollection<PlaylistModel> UserPlaylists = new ObservableCollection<PlaylistModel>();
        #endregion

        #region Genres
        public List<GenreModel> genres = new List<GenreModel>() {
            new GenreModel(){ id=1, genreName = "Alternative", genreDesc = "Alternative"},
            new GenreModel(){ id=2, genreName = "Blues", genreDesc = "Blues"},
            new GenreModel(){ id=3, genreName = "Classical", genreDesc = "Classical"},
            new GenreModel(){ id=4, genreName = "Country", genreDesc = "Country"},
            new GenreModel(){ id=5, genreName = "Dance", genreDesc = "Dance"},
            new GenreModel(){ id=6, genreName = "Electronic", genreDesc = "Electronic"},
            new GenreModel(){ id=7, genreName = "Hiphop", genreDesc = "Hiphop"},
            new GenreModel(){ id=8, genreName = "Inspirational", genreDesc = "Inspirational"},
            new GenreModel(){ id=9, genreName = "Jazz", genreDesc = "Jazz"},
            new GenreModel(){ id=10, genreName = "Opera", genreDesc = "Opera"},
            new GenreModel(){ id=11, genreName = "Pop", genreDesc = "Pop"},
            new GenreModel(){ id=12, genreName = "Punk", genreDesc = "Punk"},
            new GenreModel(){ id=13, genreName = "R&B", genreDesc = "R&B"},
            new GenreModel(){ id=14, genreName = "Rap", genreDesc = "Rap"},
            new GenreModel(){ id=15, genreName = "Reggae", genreDesc = "Reggae"},
            new GenreModel(){ id=16, genreName = "Rock", genreDesc = "Rock"},
            new GenreModel(){ id=17, genreName = "Romance", genreDesc = "Romance"},
            new GenreModel(){ id=18, genreName = "Soul", genreDesc = "Soul"},
        };
        #endregion

        #region Current Band variables and utilities
        public ObservableCollection<Album> BandAlbumCollection = new ObservableCollection<Album>();
        public ObservableCollection<Song> BandSongCollection = new ObservableCollection<Song>() { };
        public int currBandId;
        public string currBandAlbumId = "";
        public Song lastSong;
        public Song toBeModifiedSong;
        public Album tobeModifiedAlbum;
        public bool isSubscriber = true;
        public int editIdentifier = 0;
        #endregion

    }

    public class GenreModel
    {
        public int id { get; set; }
        public string genreName { get; set; }
        public string genreDesc { get; set; }
    }
}
