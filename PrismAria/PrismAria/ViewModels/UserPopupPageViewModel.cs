using GetLocalFilePath.Plugin;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Connectivity;
using Plugin.FilePicker;
using Plugin.Media.Abstractions;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Events;
using PrismAria.Helpers;
using PrismAria.Models;
using PrismAria.PopupPages;
using PrismAria.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class UserPopupPageViewModel : BindableBase
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService pageDialogService;
        private readonly IEventAggregator _eventAggregator;
        private bool _isConnected = CrossConnectivity.Current.IsConnected;
        private Singleton _singleton;
        #region Close User Popup

        private DelegateCommand _closePopupCommand;
        public DelegateCommand ClosePopupCommand =>
            _closePopupCommand ?? (_closePopupCommand = new DelegateCommand(ClosePopup));

        private void ClosePopup()
        {
            PopupNavigation.Instance.PopAllAsync();
        }
        #endregion

        #region Logout
        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new DelegateCommand(Logout));

        private async void Logout()
        {
            Settings.ClearEverything();
            await PopupNavigation.Instance.PopAllAsync(true);
            await _navigationService.NavigateAsync(new Uri("http://myapp.com/LoginPage", UriKind.Absolute),
                null,
                true,
                true);
        }
        #endregion

        #region create band
        private DelegateCommand _createBandCommand;
        public DelegateCommand CreateBandCommand =>
            _createBandCommand ?? (_createBandCommand = new DelegateCommand(CreateBand));

        private async void CreateBand()
        {

            await _navigationService.NavigateAsync("BandCreationPage", null, true, true);
            await PopupNavigation.Instance.PopAllAsync();


            
            //Play a song code
            //var path = new System.Uri("/Aria/public/assets/music/Charlie Puth covers How Deep Is Your Love by Calvin Harris in the Live Lounge.mp3").AbsolutePath;
            //Debug.WriteLine(path);
            //await CrossMediaManager.Current.Play("http://192.168.254.107" + path);


            //Add a song


        }
        #endregion

        public ObservableCollection<UserBandModelForEvent> UserBands
        {
            get { return Singleton.Instance.UserBandCollection; }
            set { SetProperty(ref Singleton.Instance.UserBandCollection, value); }
        }


        private DelegateCommand<UserBandModelForEvent> _navigateToBandCommand;
        public DelegateCommand<UserBandModelForEvent> NavigateToBandCommand =>
            _navigateToBandCommand ?? (_navigateToBandCommand = new DelegateCommand<UserBandModelForEvent>(NavigateToBand));

        private async void NavigateToBand(UserBandModelForEvent obj)
        {
            await PopupNavigation.Instance.PopAllAsync();
            try
            {
                await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/BandLandingPage/BandDetailsPage", UriKind.Absolute), null, true, true);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }

        public async void WhereToNavigate(bool isSubscriber) {

            if (isSubscriber)
            { }
            else {
                await PopupNavigation.Instance.PopAllAsync();
                await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
            }
        }

        public string UserPic { get; set; }
        public string UserName { get; set; }

        public UserPopupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            _eventAggregator = eventAggregator;
            var profile = JsonConvert.DeserializeObject<UserModel>(Settings.Profile);
            UserPic = profile.ProfilePic;
            UserName = profile.Fullname;
            _singleton = Singleton.Instance;
            PopulateUserBands();

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                _isConnected = args.IsConnected;
            };
        }

        private async void PopulateUserBands()
        {
            if (!_singleton.UserBandCollection.Any())
            {
                if (_isConnected)
                {
                    if (!await _singleton.CollectionService.PopulateUserBands())
                    {
                        await pageDialogService.DisplayAlertAsync("Ooops!", "It seems like we encountered a problem", "Ok");
                    }
                }
                else
                    await pageDialogService.DisplayAlertAsync("Connectivity issues", "Cannot load because your device is not connected to the internet", "ok");
            }
        }

        private void PublishBand(UserBandModelForEvent obj)
        {
            //_userBandsService.AddBands(obj.userBandName, obj.userBandRole, obj.userBandImage);
        }
        
    }
}
