using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Helpers;


namespace PrismAria.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private IPageDialogService _dialogService;
        private INavigationService _navigationService;

        #region Setting variables, getters and setters

        private bool _isConnected = CrossConnectivity.Current.IsConnected;

        #region SignUp

        private DelegateCommand _signUpCommand;
	    public DelegateCommand SignUpCommand =>
	        _signUpCommand ?? (_signUpCommand = new DelegateCommand(SignUp));

	    
        
	    private void SignUp()
	    {
           
	        if (_isConnected)
	        {
                
	        }
	        else
	        {
	            _dialogService.DisplayAlertAsync("Connectivity Issues", "Your device is not connected to the internet!", "OK");
            }
        }

        #endregion

        #region Facebook Integration
        private DelegateCommand _fbLoginCommand;
        public DelegateCommand FbLoginCommand =>
            _fbLoginCommand ?? (_fbLoginCommand = new DelegateCommand(FbLogin));

	    private void FbLogin()
	    {
            if (_isConnected)
	        {
	            _navigationService.NavigateAsync("FacebookLoginPage", null, true, false);
            }
	        else
	        {
	            _dialogService.DisplayAlertAsync("Connectivity Issues","Your device is not connected to the internet!", "OK");
	        }
	    }

	    #endregion

        #endregion

	    public LoginPageViewModel(IPageDialogService dialogService, INavigationService navigationService)
	    {
	        _dialogService = dialogService;
	        _navigationService = navigationService;
	        CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
	        {
                _isConnected = args.IsConnected;
	        };
	    }
	}

}
