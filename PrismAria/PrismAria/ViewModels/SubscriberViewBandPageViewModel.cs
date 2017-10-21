using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace PrismAria.ViewModels
{
	public class SubscriberViewBandPageViewModel : BindableBase, INavigationAware
	{
        private readonly INavigationService navigationService;
        private BandModel _band;

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ImageSource _bandImage;
        public ImageSource BandImage
        {
            get { return _bandImage; }
            set { SetProperty(ref _bandImage, value); }
        }

        private string _bandDesc;
        public string BandDesc
        {
            get { return _bandDesc; }
            set { SetProperty(ref _bandDesc, value); }
        }

        private string _followerCount;
        public string FollowerCount
        {
            get { return _followerCount; }
            set { SetProperty(ref _followerCount, value); }
        }

        private ObservableCollection<BandPagePopularModel> _popularList = new ObservableCollection<BandPagePopularModel>();
        public ObservableCollection<BandPagePopularModel> PopularList
        {
            get { return _popularList; }
            set { SetProperty(ref _popularList, value); }
        }

        private ObservableCollection<BandPageAlbum> _albumList = new ObservableCollection<BandPageAlbum>();
        public ObservableCollection<BandPageAlbum> AlbumList
        {
            get { return _albumList; }
            set { SetProperty(ref _albumList, value); }
        }

        private DelegateCommand _followBandCommand;
        public DelegateCommand FollowBandCommand =>
            _followBandCommand ?? (_followBandCommand = new DelegateCommand(FollowBand));

        private void FollowBand()
        {
            if (ButtonState)
            {
                ChangeButtonStyle(ButtonState);
                ButtonState = false;
            }
            else
            {
                ChangeButtonStyle(ButtonState);
                ButtonState = true;
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _band = (BandModel)parameters["model"];
            Title = _band.BandName;
            BandImage = _band.BandPic;
            BandDesc = _band.BandDesc;
            FollowerCount = _band.NumFollowers.ToString() + "\n Followers";
            //Debug.WriteLine(CheckIfFollowed().ToString());
            ChangeButtonStyle(CheckIfFollowed());
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        private bool ButtonState = false;

        private int _popularHeight;
        public int PopularHeight
        {
            get { return _popularHeight; }
            set { SetProperty(ref _popularHeight, value); }
        }

        private int _albumHeight;
        public int AlbumHeight
        {
            get { return _albumHeight; }
            set { SetProperty(ref _albumHeight, value); }
        }

        private string _buttonText = "";
        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }

        private Color _buttonTextColor = Color.FromHex("#212121");
        public Color ButtonTextColor
        {
            get { return _buttonTextColor; }
            set { SetProperty(ref _buttonTextColor, value); }
        }

        private Color _buttonBackgroundColor = Color.Transparent;
        public Color ButtonBackgroundColor
        {
            get { return _buttonBackgroundColor; }
            set { SetProperty(ref _buttonBackgroundColor, value); }
        }


        public SubscriberViewBandPageViewModel(INavigationService navigationService)
        {
            for (int i = 0; i < 3; i++)
            {
                _popularList.Add(new BandPagePopularModel()
                {
                    SongAlbum = "sample_pic.png",
                    SongName = "Lami ilaba",
                    SongListenedCount = "23,000,032"
                });
            }

            _popularHeight = 3 * 80;

            for (int i = 0; i < 4; i++)
            {
                _albumList.Add(new BandPageAlbum()
                {
                    AlbumPic = "sample_pic.png",
                    AlbumTitle = "Album Title",
                    AlbumLikes = "15,000 liked this"
                });
            }

            _albumHeight = (4 / 2) * 150;

            this.navigationService = navigationService;
        }

        private bool CheckIfFollowed()
        {
            var isFollowing = false;
            if(Singleton.Instance.userPreference != null)
            {
                foreach(var item in Singleton.Instance.userPreference)
                {
                    if(item.BandId == _band.BandId)
                    {
                        isFollowing = true;
                        ButtonState = true;
                    }
                }
            }

            return isFollowing;
        }

        private void ChangeButtonStyle(bool v)
        {
            System.Diagnostics.Debug.WriteLine("changing button style");
            if(v)
            {
                ButtonText = "Following";
                ButtonTextColor = Color.FromHex("#FAFAFA");
                ButtonBackgroundColor = Color.FromHex("#F79012");
            }
            else
            {
                ButtonText = "Follow";
                ButtonTextColor = Color.FromHex("#212121");
                ButtonBackgroundColor = Color.Transparent;
            }
        }
    }
}
