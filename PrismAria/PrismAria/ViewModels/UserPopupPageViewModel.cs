using Newtonsoft.Json;
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
        private readonly UserBandsService _userBandsService;
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
            
            PopupNavigation.Instance.PopAllAsync();
            await _navigationService.NavigateAsync("BandCreationPage", null, true, true);
            
        }
        #endregion

        private ObservableCollection<UserBandModel> _userBands;
        public ObservableCollection<UserBandModel> UserBands
        {
            get { return _userBands; }
            set { SetProperty(ref _userBands, value); }
        }


        private DelegateCommand<UserBandModel> _navigateToBandCommand;
        public DelegateCommand<UserBandModel> NavigateToBandCommand =>
            _navigateToBandCommand ?? (_navigateToBandCommand = new DelegateCommand<UserBandModel>(NavigateToBand));

        private async void NavigateToBand(UserBandModel obj)
        {
            await PopupNavigation.Instance.PopAllAsync();
            try
            {
                if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/BandLandingPage/BandDetailsPage", UriKind.Absolute), null, true, true);
                }
                else
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/BandLandingPage/BandDetailsPage", UriKind.Absolute), null, true, true);
                }
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

                if (Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android))
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/RootPage/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
                }
                else
                {
                    await _navigationService.NavigateAsync(new Uri("http://myapp.com/SubscriberLanding/Discover", UriKind.Absolute), null, true, true);
                }
            }
        }

        public string UserPic { get; set; }
        public string UserName { get; set; }

        public UserPopupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            _eventAggregator = eventAggregator;
            var profile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
            UserPic = profile.Picture.Data.Url;
            UserName = profile.Name;
            _userBandsService = new UserBandsService();
            _userBands = _userBandsService.GetUserBands();
            _eventAggregator.GetEvent<UserBandsEvent>().Subscribe(PublishBand);
        }

        private void PublishBand(UserBandModelForEvent obj)
        {
            _userBandsService.AddBands(obj.userBandName, obj.userBandRole, obj.userBandImage);
        }
    }
}
