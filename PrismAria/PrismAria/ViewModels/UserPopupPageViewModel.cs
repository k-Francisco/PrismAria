using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Helpers;
using PrismAria.Models;
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
        private readonly UserBandsService _userBandsService;
        #region Close User Popup

        private DelegateCommand _closePopupCommand;
        public DelegateCommand ClosePopupCommand =>
            _closePopupCommand ?? (_closePopupCommand = new DelegateCommand(ClosePopup));

        private void ClosePopup()
        {
            PopupNavigation.PopAllAsync();
        }
        #endregion

        #region Logout
        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new DelegateCommand(Logout));

        private async void Logout()
        {
            Settings.ClearEverything();
            await PopupNavigation.PopAllAsync(true);
            await _navigationService.NavigateAsync(new Uri("http://myapp.com/LoginPage", UriKind.Absolute),
                null,
                true,
                true);
        }
        #endregion

        private ObservableCollection<UserBandModel> _userBands;
        public ObservableCollection<UserBandModel> UserBands
        {
            get { return _userBands; }
            set { SetProperty(ref _userBands, value); }
        }

        public string UserPic { get; set; }
        public string UserName { get; set; }

        public UserPopupPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var profile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
            UserPic = profile.Picture.Data.Url;
            UserName = profile.Name;

            _userBandsService = new UserBandsService();
            _userBands = _userBandsService.GetUserBands();
        }
	}
}
