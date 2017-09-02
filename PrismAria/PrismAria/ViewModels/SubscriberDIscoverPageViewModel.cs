﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using PrismAria.Models;
using System.Diagnostics;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : BindableBase, INavigatedAware
    {
        
        private IEventAggregator _ea;
        private FacebookProfile _profile;
        private List<BandModel> _bandList = new List<BandModel>()
        {
                    new BandModel(){ imgSource = "logo.png", bandName="Maroon 5"},
                    new BandModel(){ imgSource = "logo.png", bandName="Paramore"},
                    new BandModel(){ imgSource = "logo.png", bandName="Panic at the Disco"},
                    new BandModel(){ imgSource = "logo.png", bandName="Sleeping with the sirens"},
                    new BandModel(){ imgSource = "logo.png", bandName="Up Dharma Down"},
                    new BandModel(){ imgSource = "logo.png", bandName="Fall Out Boys"},
                    new BandModel(){ imgSource = "logo.png", bandName="All Time Low"},
        };
        ObservableCollection<DiscoverPageModel> discovery = new ObservableCollection<DiscoverPageModel>();
        public List<BandModel> BandList
        {
            get { return _bandList; }
            set { _bandList = value; }
        }

        public ObservableCollection<DiscoverPageModel> Discovery
        {
            get
            {
                if (!discovery.Any())
                {
                    discovery.Add(new DiscoverPageModel() { categoryName = "Trending", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "Most Favorite", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "Recommended", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "Rising Stars", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "Rockers", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "Show Bands", bandList = _bandList });
                    discovery.Add(new DiscoverPageModel() { categoryName = "BisRock", bandList = _bandList });
                }
                return discovery;
            }
            set { discovery = value; }
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _profile = (FacebookProfile)parameters["profile"];
        }

        private ObservableCollection<string> sample = new ObservableCollection<string>();
        public ObservableCollection<string> Sample {
            get {
                if (!sample.Any()) {
                    sample.Add("sample 1");
                    sample.Add("sample 2");
                    sample.Add("sample 3");
                    sample.Add("sample 4");
                    sample.Add("sample 5");
                }
                return sample; }
        }

    }
}
