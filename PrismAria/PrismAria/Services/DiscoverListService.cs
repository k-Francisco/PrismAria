﻿
using Prism.Commands;
using Prism.Navigation;
using PrismAria.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace PrismAria.Services
{
    public class DiscoverListService
    {
        //ObservableCollection<DiscoverPageModel> discoverList = new ObservableCollection<DiscoverPageModel>();
        //List<BandModel> bandList = new List<BandModel>();
        //public ObservableCollection<DiscoverPageModel> GetDiscoverList(INavigationService navigationService) {
        //    if (discoverList.Count == 0) {
        //        var categories = new List<string>() {
        //            "Top 10",
        //            "Trending",
        //            "Most Favorite",
        //            "Recommended",
        //            "Rising Stars",
        //            "Rockers",
        //            "Showbands",
        //            "BisRock",
        //            "New"
        //        };
        //        foreach (var item in categories) {
        //            discoverList.Add(new DiscoverPageModel() { categoryName = item, bandList = GetBands(navigationService) });
        //        }
        //    }


        //    return discoverList;
        //}

        //private List<BandModel> GetBands(INavigationService navigationService) {

        //    var showBandPage = new DelegateCommand<BandModel>((obj) => {
        //        if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
        //            navigationService.NavigateAsync("SubscriberViewBandPage", null, false, true);
        //        else
        //            navigationService.NavigateAsync("SubscriberViewBandPage", null, true, true);

        //    });

        //    if (bandList.Count == 0)
        //    {
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Maroon 5", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Paramore", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Panic at the Disco", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Sleeping with the sirens", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Up Dharma Down", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Fall Out Boys", BandClick = showBandPage });
        //        bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "All Time Low", BandClick = showBandPage });
        //    }
        //    return bandList;
        //}
    }
}
