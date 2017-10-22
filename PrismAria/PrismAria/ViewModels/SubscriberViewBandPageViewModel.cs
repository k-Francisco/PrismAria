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

        private ObservableCollection<Song> _popularList = new ObservableCollection<Song>();
        public ObservableCollection<Song> PopularList
        {
            get { return _popularList; }
            set { SetProperty(ref _popularList, value); }
        }

        private ObservableCollection<Song> _wholeBandSong = new ObservableCollection<Song>();
        public ObservableCollection<Song> WholeBandSong
        {
            get { return _wholeBandSong; }
            set { SetProperty(ref _wholeBandSong, value); }
        }

        private ObservableCollection<Album> _albumList = new ObservableCollection<Album>();
        public ObservableCollection<Album> AlbumList
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
            ChangeButtonStyle(CheckIfFollowed());
            GetBandAlbums();
        }

        private async void GetBandSongs()
        {
            var albumIds = new List<string>();
            if (AlbumList.Count != 0)
            {
                foreach (var item in AlbumList)
                    albumIds.Add(item.AlbumId.ToString());

                try
                {
                    if (await Singleton.Instance.CollectionService.PopulateBandSongs(albumIds))
                    {
                        foreach (var item in Singleton.Instance.SongCollection)
                        {
                            foreach (var item2 in item.Songs)
                            {
                                item2.AlbumPic = item.Album.AlbumPic;
                                WholeBandSong.Add(item2);
                                
                                if (item2.NumPlays == null)
                                    item2.NumPlays = "0";
                            }
                        }

                        if(WholeBandSong.Count != 0)
                        {
                            var sortedList = WholeBandSong.OrderByDescending(s => Convert.ToInt32(s.NumPlays.ToString())).ToList();

                            if (WholeBandSong.Count >= 5)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    Debug.WriteLine(sortedList[i].SongAudio);
                                    PopularList.Add(sortedList[i]);
                                    PopularHeight += 80;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < WholeBandSong.Count; i++)
                                {
                                    Debug.WriteLine(sortedList[i].SongAudio);
                                    PopularList.Add(sortedList[i]);
                                    PopularHeight += 80;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                
            }
           
        }

        private async void GetBandAlbums()
        {
            if(await Singleton.Instance.CollectionService.PopulateBandAlbums(_band.BandId.ToString()))
            {
                foreach(var item in Singleton.Instance.AlbumCollection[0].Albums)
                {
                    var isNull = false;
                    if (item.NumLikes == null)
                        isNull = true;

                        AlbumList.Add(new Album()
                        {
                            AlbumName = item.AlbumName,
                            AlbumDesc = item.AlbumDesc,
                            AlbumId = item.AlbumId,
                            AlbumPic = item.AlbumPic,
                            BandId = item.BandId,
                            CreatedAt = item.CreatedAt,
                            NumLikes = isNull ? "0 likes" : item.NumLikes.ToString() + " likes",
                            UpdatedAt = item.UpdatedAt
                        });
                }
                if((Singleton.Instance.AlbumCollection[0].Albums.Count%2) == 1)
                    AlbumHeight = ((Singleton.Instance.AlbumCollection[0].Albums.Count / 2) * 150) + 150;
                else
                    AlbumHeight = ((Singleton.Instance.AlbumCollection[0].Albums.Count / 2) * 150);

                GetBandSongs();
            } 
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

        private string _buttonText = "Follow";
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
