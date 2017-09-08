using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismAria.Helpers;
using PrismAria.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class UserPopupPageViewModel : BindableBase
	{

        
        private readonly INavigationService _navigationService;

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
                false);
        }
        #endregion


        public string UserPic { get; set; }
        public string UserName { get; set; }

        public UserPopupPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var profile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
            UserPic = profile.Picture.Data.Url;
            UserName = profile.Name;
        }
	}
}
