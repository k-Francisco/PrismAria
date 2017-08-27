using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Plugin.Connectivity;
using PrismAria.Helpers;

namespace PrismAria.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
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
                //TODO implement sign up animation and functionality
	        }
	        else
	        {
	            //TODO implement dialog to alert user that he/she is offline
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
	            Debug.WriteLine("hello");
	        }
	        else
	        {
	        }
	    }

	    #endregion

        #endregion

	    public LoginPageViewModel()
	    {
	        CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
	        {
                _isConnected = args.IsConnected;
	        };
	    }
	}

}
