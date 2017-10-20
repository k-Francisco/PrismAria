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
                    var userPreference = JsonConvert.DeserializeObject<UserPreferenceModel[]>(await Singleton.Instance.webService.GetUserPreference(Settings.Token));
                    isTherePreference = true;

                    foreach (var item in response.ToList())
                    {
                        foreach(var item2 in userPreference.ToList())
                        {
                            if (item.BandId.Equals(item2.BandId))
                            {
                                bands.Remove(item);
                                preferencedBands.Add(item);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    //if the user has no preferences yet
                    Debug.WriteLine(e.Message);
                }

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

                    var showBandPage = new DelegateCommand<BandModel>((obj) =>
                    {
                        navigationService.NavigateAsync("SubscriberViewBandPage", null, false, true);
                    });
                    for (int i = 0; i < 3; i++)
                    {
                        AddRelatedBandsToCollection(i, preferencedBands, bands, totalBandCount, preferencedBandGenres, notPreferencedBandGenres, showBandPage);
                    }
                }

                isSuccess = true;
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        private void AddRelatedBandsToCollection(int i, 
            List<BandModel> preferencedBands, 
            List<BandModel> bands, 
            int totalBandCount, 
            List<BandGenreModel> preferencedBandGenres, 
            List<BandGenreModel> notPreferencedBandGenres,
            DelegateCommand<BandModel> ShowBandPage)
        {
            const int BasedOnGenre = 0, BasedOnRanking = 1, BasedOnPopularity = 2;
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
                    category = "The top bands for this week!";
                    break;

                case BasedOnPopularity:
                    GenreScore = 1.5;
                    RankingWeightedPercentage = .03;
                    PopularityWeightedPercentage = .04;
                    category = "Popular";
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
                //Debug.WriteLine(item.band.BandName + " " + item.BandScore);
                item.band.BandClick = ShowBandPage;
                modelList.Add(item.band);
            }

            Singleton.Instance.DiscoverCollection.Add(new DiscoverPageModel() { categoryName = category, bandList = modelList });
            bandRecommendationRanking.Clear();
        }

       

        public void GenerateArticlesForSubscriber(ObservableCollection<ArticlesModel> collection) {
            for (int i = 0; i < 10; i++)
            {
                collection.Add(
                new ArticlesModel()
                {
                    BandName = "Maroon 5",
                    BandPic = "sample_pic.png",
                    ArticleTitle = "New Album",
                    Article = "Our album named shit is very nice and shit"
                });
            }
        }

        public void AddBandArticles(ObservableCollection<ArticlesModel> collection) {
            for(int i = 0; i < 10; i++)
            {
                collection.Add(
                new ArticlesModel()
                {
                    BandName = "Maroon 5",
                    BandPic = "sample_pic.png",
                    ArticleTitle = "New Album",
                    Article = "Our album named shit is very nice and shit"
                });
            }
        }

    }

    class BandRecommendationScores
    {
        public int bandId { get; set; }
        public BandModel band { get; set; }
        public double BandScore { get; set; }
    }

}
