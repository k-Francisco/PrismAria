using Newtonsoft.Json;
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
                    Debug.WriteLine(e.Message);
                }

                if (isTherePreference)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        AddRelatedBandsToCollection(i, preferencedBands, bands, totalBandCount);
                    }
                }



                //    var showBandPage = new DelegateCommand<BandModel>((obj) => {
                //        navigationService.NavigateAsync("SubscriberViewBandPage", null, false, true);
                //    });

                //    var bands = new List<BandModel>() {
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Maroon 5", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Paramore", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Panic at the Disco!", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Sleeping with the sirens", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "All Time Low", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Slip Knot", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Up Dharma Down", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Rocksteady", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Orange and Lemons", BandClick = showBandPage },
                //    new BandModel() { imgSource = "sample_pic.png", bandName = "Sugar Free", BandClick = showBandPage },
                //};

                //    foreach (var item in categories)
                //        collection.Add(new DiscoverPageModel() { categoryName = item, bandList = bands });

                isSuccess = true;
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        private async void AddRelatedBandsToCollection(int i, List<BandModel> preferencedBands, List<BandModel> bands, int totalBandCount)
        {
            const int BasedOnGenre = 0;
            List<BandRecommendationScores> bandRecommendationRanking = new List<BandRecommendationScores>() { };
            foreach (var item in bands)
            {
                bandRecommendationRanking.Add(new BandRecommendationScores() { bandId = item.BandId, band = item, BandScore = 0.00});
            }
            var preferencedBandGenres = new List<BandGenreModel>() { };
            var notPreferencedBandGenres = new List<BandGenreModel>() { };
            switch (i)
            {
                case BasedOnGenre:
                    foreach (var item in preferencedBands)
                    {
                        var response = JsonConvert.DeserializeObject<BandGenreModel[]>(await Singleton.Instance.webService.GetBandGenres(item.BandId.ToString()));
                        foreach (var item2 in response.ToList())
                            preferencedBandGenres.Add(item2);

                        response = null;
                    }
                    
                    foreach(var item in bands)
                    {
                        var response = JsonConvert.DeserializeObject<BandGenreModel[]>(await Singleton.Instance.webService.GetBandGenres(item.BandId.ToString()));
                        foreach (var item2 in response.ToList())
                            notPreferencedBandGenres.Add(item2);

                        response = null;
                    }
                    //if the bands matched the genres of the user's preferenced bands
                    // +4 if it matched the 2 genres
                    // +2 if it only matched 1 genre
                    foreach (var item in bandRecommendationRanking) {
                        foreach(var item2 in notPreferencedBandGenres)
                        {
                            if(item.bandId == item2.BandId)
                            {
                                foreach (var item3 in preferencedBandGenres)
                                {
                                    if (item3.GenreId == item2.GenreId) {
                                        item.BandScore += 2.00;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    
                    //score of the bands according to ranking
                    //(((number of total bands - (rank-1))/number of total bands) *100) * weighted percentage = .03 or 30% of the perfect score which is 10
                    foreach (var item in bandRecommendationRanking)
                    {
                        item.BandScore += (((totalBandCount - (item.bandId - 1)) / (double)totalBandCount) * 100) * .03;
                    }



                    foreach (var item in bandRecommendationRanking)
                    {
                        Debug.WriteLine(item.bandId + " " + item.BandScore);
                    }

                    break;
            }
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
