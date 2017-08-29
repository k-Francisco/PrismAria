using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using PrismAria.Models;
using System.Diagnostics;
using Prism.Navigation;

namespace PrismAria.ViewModels
{
    public class SubscriberDIscoverPageViewModel : BindableBase, INavigatedAware
    {
        
        private IEventAggregator _ea;
        private FacebookProfile _profile;

        private string _coverPhoto;
        public string CoverPhoto
        {
            get { return _coverPhoto; }
            set { SetProperty(ref _coverPhoto, value); }
        }

        private string _profilePicture;
        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }

        private string _sampleText;
        public string SampleText
        {
            get { return _sampleText; }
            set { SetProperty(ref _sampleText, value); }
        }

        public SubscriberDIscoverPageViewModel(IEventAggregator ea)
        {
            _ea = ea;
        }

        private void SetProfile()
        {
            SampleText = _profile.Name;
            ProfilePicture = _profile.Picture.Data.Url;
            CoverPhoto = _profile.Cover.Source;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _profile = (FacebookProfile)parameters["profile"];
            SetProfile();
        }
    }
}
