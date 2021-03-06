﻿using Newtonsoft.Json;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria.Services
{
    public class FavoritesListService
    {
        public ObservableCollection<FavoritesModel> favoritesCollection = new ObservableCollection<FavoritesModel>();

        public ObservableCollection<FavoritesModel> GetFavorites() {
            
            if (favoritesCollection.Count == 0) {
                var profile = JsonConvert.DeserializeObject<UserModel>(Settings.Profile);
                favoritesCollection.Add( 
                    new FavoritesModel() {
                        bandImage = profile.ProfilePic,
                        bandName = "PATD!",
                        songsAndAlbums = "3 albums, 20 songs"}
                    );
                favoritesCollection.Add(
                    new FavoritesModel()
                    {
                        bandImage = profile.ProfilePic,
                        bandName = "Maroon 5",
                        songsAndAlbums = "3 albums, 20 songs"
                    }
                    );
                favoritesCollection.Add(
                    new FavoritesModel()
                    {
                        bandImage = profile.ProfilePic,
                        bandName = "Fall out boys",
                        songsAndAlbums = "3 albums, 20 songs"
                    }
                    );
                favoritesCollection.Add(
                    new FavoritesModel()
                    {
                        bandImage = profile.ProfilePic,
                        bandName = "Band Name Here",
                        songsAndAlbums = "3 albums, 20 songs"
                    }
                    );
                favoritesCollection.Add(
                    new FavoritesModel()
                    {
                        bandImage = profile.ProfilePic,
                        bandName = "Band Name Here",
                        songsAndAlbums = "3 albums, 20 songs"
                    }
                    );
            }

            return favoritesCollection;
        }
    }
}
