﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAria.Services
{
    public class CollectionService
    {
        private bool isSuccess;
        public async Task<bool> GenerateBandsToExplore(ObservableCollection<DiscoverPageModel> collection, INavigationService navigationService)
        {
            isSuccess = false;
            bool isTherePreference = false;
            try
            {
                var response = JsonConvert.DeserializeObject<BandModel[]>(await Singleton.Instance.webService.GetBands());
                int totalBandCount = response.Count();
                List<BandModel> bands = new List<BandModel>() { };
                List<BandModel> preferencedBands = new List<BandModel>() { };
                foreach (var item in response)
                    bands.Add(item);
                
                try
                {
                    Singleton.Instance.userPreference = null;
                    Singleton.Instance.userPreference = JsonConvert.DeserializeObject<UserPreferenceModel[]>(await Singleton.Instance.webService.GetUserPreference(Settings.Token));

                    if(Singleton.Instance.userPreference.Any())
                    {
                        isTherePreference = true;
                        Singleton.Instance.FavoritesCollection.Clear();
                        foreach (var item in response.ToList())
                        {
                            foreach (var item2 in Singleton.Instance.userPreference.ToList())
                            {
                                Debug.WriteLine(item2.UserId + " " + item2.BandId);
                                if (item.BandId.Equals(item2.BandId))
                                {
                                    bands.Remove(item);
                                    preferencedBands.Add(item);
                                    Singleton.Instance.FavoritesCollection.Add(item);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //if the user has no preferences yet
                    Debug.WriteLine(e.Message);
                }

                var showBandPage = new DelegateCommand<BandModel>((obj) =>
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("model", obj);
                    navigationService.NavigateAsync(new Uri("SubscriberViewBandPage", UriKind.Relative), navigationParameters, false, true);
                });

                if (isTherePreference)
                {
                    var preferencedBandGenres = new List<BandGenreModel>() { };
                    var notPreferencedBandGenres = new List<BandGenreModel>() { };

                    foreach (var item in preferencedBands)
                    {
                        var response2 = JsonConvert.DeserializeObject<BandGenreModel[]>(await Singleton.Instance.webService.GetBandGenres(item.BandId.ToString()));
                        foreach (var item2 in response2.ToList())
                            preferencedBandGenres.Add(item2);

                        response2 = null;
                    }

                    foreach (var item in bands)
                    {
                        var response2 = JsonConvert.DeserializeObject<BandGenreModel[]>(await Singleton.Instance.webService.GetBandGenres(item.BandId.ToString()));
                        foreach (var item2 in response2.ToList())
                            notPreferencedBandGenres.Add(item2);

                        response2 = null;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        AddRelatedBandsToCollection(i, preferencedBands, bands, totalBandCount, preferencedBandGenres, notPreferencedBandGenres, showBandPage);
                    }

                    for (int i = 0; i < 3; i++)
                        AddNotRelatedBandsToCollection(response, i, preferencedBands, showBandPage);

                }
                else
                {
                    for(int i = 0; i < 3; i++)
                        AddNotRelatedBandsToCollection(response, i, preferencedBands, showBandPage);
                }

                isSuccess = true;
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        private void AddNotRelatedBandsToCollection(BandModel[] response, int i, List<BandModel> prefered, DelegateCommand<BandModel> showBandPage)
        {
            const int NewlyFormed = 0, MostFollowed = 1, Preferenced = 2;
            string category = "";
            
            switch (i)
            {
                case NewlyFormed:
                    var sortedList = response.OrderByDescending(d => Convert.ToDateTime(d.CreatedAt).Day).ToList();
                    foreach(var item in sortedList)
                    {
                        item.BandClick = showBandPage;
                    }
                    category = "Newly Formed Bands";
                    
                    Singleton.Instance.DiscoverCollection.Add(new DiscoverPageModel() { categoryName = category, bandList = sortedList});
                    break;

                case MostFollowed:
                    var sortedList2 = response.OrderByDescending(d => Convert.ToInt32(d.NumFollowers.ToString())).ToList();
                    foreach (var item in sortedList2)
                    {
                        item.BandClick = showBandPage;
                    }
                    category = "Most Followed";
                    Singleton.Instance.DiscoverCollection.Add(new DiscoverPageModel() { categoryName = category, bandList = sortedList2 });
                    break;

                case Preferenced:
                    var sortedList3 = prefered.OrderBy(p => Convert.ToInt32(p.NumFollowers.ToString())).ToList();
                    foreach (var item in sortedList3)
                    {
                        item.BandClick = showBandPage;
                    }
                    category = "Listen to them again";
                    Singleton.Instance.DiscoverCollection.Add(new DiscoverPageModel() { categoryName = category, bandList = sortedList3 });
                    break;
            }
        }

        private void AddRelatedBandsToCollection(int i, 
            List<BandModel> preferencedBands, 
            List<BandModel> bands, 
            int totalBandCount, 
            List<BandGenreModel> preferencedBandGenres, 
            List<BandGenreModel> notPreferencedBandGenres,
            DelegateCommand<BandModel> ShowBandPage)
        {
            const int BasedOnGenre = 0, BasedOnRanking = 1, BasedOnPopularity = 2, RisingStars = 3; 
            double GenreScore = 0.0, PopularityWeightedPercentage = 0.0, RankingWeightedPercentage = 0.0;
            List<BandRecommendationScores> bandRecommendationRanking = new List<BandRecommendationScores>() { };
            string category = "";
            foreach (var item in bands)
            {
                bandRecommendationRanking.Add(new BandRecommendationScores() { bandId = item.BandId, band = item, BandScore = 0.00 });
            }
        
            switch (i)
            {
                case BasedOnGenre:
                    GenreScore = 2.0;
                    PopularityWeightedPercentage = .03;
                    RankingWeightedPercentage = .03;
                    category = "Genre Based"; 
                    break;

                case BasedOnRanking:
                    GenreScore = 1.5;
                    RankingWeightedPercentage = .04;
                    PopularityWeightedPercentage = .03;
                    category = "Weekly Top Bands!";
                    break;

                case BasedOnPopularity:
                    GenreScore = 1.5;
                    RankingWeightedPercentage = .03;
                    PopularityWeightedPercentage = .04;
                    category = "Popular";
                    break;

                case RisingStars:
                    GenreScore = 1.5;
                    RankingWeightedPercentage = .04;
                    PopularityWeightedPercentage = .03;
                    category = "The Rising Stars";
                    break;

            }

            

            //if the bands matched the genres of the user's preferenced bands
            foreach (var item in bandRecommendationRanking)
            {
                foreach (var item2 in notPreferencedBandGenres)
                {
                    if (item.bandId == item2.BandId)
                    {
                        foreach (var item3 in preferencedBandGenres)
                        {
                            if (item3.GenreId == item2.GenreId)
                            {
                                item.BandScore += GenreScore;
                                break;
                            }
                        }
                    }
                }
            }

            //score of the bands according to ranking
            //(((number of total bands - (rank-1))/number of total bands) *100) * weighted percentage 
            foreach (var item in bandRecommendationRanking)
            {
                item.BandScore += (((totalBandCount - (item.bandId - 1)) / (double)totalBandCount) * 100) * RankingWeightedPercentage;
            }

            //score of the bands according to popularity
            //(((number of total bands - (popularity rank - 1))/number of total bands) * 100) * weighted percentage
            foreach (var item in bandRecommendationRanking)
            {
                item.BandScore += (((totalBandCount - (item.bandId - 1)) / (double)totalBandCount) * 100) * PopularityWeightedPercentage;
            }

            
            var sortedList = bandRecommendationRanking.OrderByDescending(p => p.BandScore);
            var modelList = new List<BandModel>() { };
            foreach (var item in sortedList)
            {
                if (i == 3)
                {
                    var start = Convert.ToDateTime(item.band.CreatedAt);
                    if((DateTime.Now.Month - start.Month) == 0)
                    {
                        Debug.WriteLine((DateTime.Now.Month - start.Month).ToString());
                        item.band.BandClick = ShowBandPage;
                        modelList.Add(item.band);
                    }
                }
                else
                {
                    item.band.BandClick = ShowBandPage;
                    modelList.Add(item.band);
                }
            }

            Singleton.Instance.DiscoverCollection.Add(new DiscoverPageModel() { categoryName = category, bandList = modelList });
            bandRecommendationRanking.Clear();
        }

        public async Task<bool> GenerateArticlesForSubscriber() {
            isSuccess = false;
            var list = new List<ArticlesModel>();
            
            try
            {
                foreach (var item in Singleton.Instance.FavoritesCollection)
                {
                    var response = JsonConvert.DeserializeObject<ArticlesModel[]>(await Singleton.Instance.webService.GetBandArticles(item.BandId.ToString()));
                    foreach (var item2 in response)
                    {
                        item2.BandName = item.BandName;
                        item2.BandPic = item.BandPic;
                        list.Add(item2);
                    }
                }

                var sortedList = list.OrderByDescending(i => i.ArtId).ToList();
                foreach (var item in sortedList)
                    Singleton.Instance.SubscriberArticlesCollection.Add(item);

                isSuccess = true;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool>PopulateBandAlbums(string bandId) {

            isSuccess = false;
            try
            {
                Singleton.Instance.AlbumCollection.Clear();
                var response = JsonConvert.DeserializeObject<BandPageAlbum>(await Singleton.Instance.webService.GetBandAlbum(bandId));
                Singleton.Instance.AlbumCollection.Add(new BandPageAlbum() { Band = response.Band, Albums = response.Albums });
                isSuccess = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return isSuccess;   
        }

        public async Task<bool> PopulateBandSongs(List<string> albumIds)
        {
            isSuccess = false;
            try
            {
                Singleton.Instance.SongCollection.Clear();
                
                foreach(var item in albumIds)
                {
                    var response = JsonConvert.DeserializeObject<BandPagePopularModel>(await Singleton.Instance.webService.GetBandSongs(item));
                    Singleton.Instance.SongCollection.Add(response);
                }
                isSuccess = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> PopulateUserBands() {

            isSuccess = false;
            try
            {
                Singleton.Instance.UserBandCollection.Clear();
                var bands = new List<UserBandModel>() { };
                var response = JsonConvert.DeserializeObject<UserBandModel[]>(await Singleton.Instance.webService.GetAllBandMembers());
                foreach(var item in response)
                {
                    if (item.UserId.Equals(Settings.Token))
                        bands.Add(item);
                }

                var response2 = JsonConvert.DeserializeObject<BandModel[]>(await Singleton.Instance.webService.GetBands());
                foreach (var item in bands)
                {
                    foreach (var item2 in response2) {
                        if(item.BandId == item2.BandId)
                        {
                            Singleton.Instance.UserBandCollection.Add(new UserBandModelForEvent() { userBandImage = item2.BandPic, userBandName = item2.BandName, userBandRole = item.Bandrole});
                        }
                    }
                }

                isSuccess = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

    }

    class BandRecommendationScores
    {
        public int bandId { get; set; }
        public BandModel band { get; set; }
        public double BandScore { get; set; }
    }

}
