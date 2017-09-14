﻿using Prism.Commands;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace PrismAria.Services
{
    public class DiscoverListService
    {
        ObservableCollection<DiscoverPageModel> discoverList = new ObservableCollection<DiscoverPageModel>();
        List<BandModel> bandList = new List<BandModel>();
        public ObservableCollection<DiscoverPageModel> GetDiscoverList() {
            if (discoverList.Count == 0) {
                var categories = new List<string>() {
                    "Trending",
                    "Most Favorite",
                    "Recommended",
                    "Rising Stars",
                    "Rockers",
                    "Showbands",
                    "BisRock"
                };

                foreach (var item in categories) {
                    discoverList.Add(new DiscoverPageModel() { categoryName = item, bandList = GetBands() });
                }
            }


            return discoverList;
        }

        private List<BandModel> GetBands() {
            
            if(bandList.Count == 0)
            {
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Maroon 5", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Paramore", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Panic at the Disco", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Sleeping with the sirens", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Up Dharma Down", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "Fall Out Boys", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
                bandList.Add(new BandModel() { imgSource = "logo.png", bandName = "All Time Low", BandClick = new DelegateCommand<BandModel>((obj) => { Debug.WriteLine(obj.bandName); }) });
            }
            return bandList;
        }
    }
}