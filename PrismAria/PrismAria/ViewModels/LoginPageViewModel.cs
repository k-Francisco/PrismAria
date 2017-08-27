using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class LoginPageViewModel : BindableBase
	{
        private bool _canSignUpProp;
        public bool CanSignUpProp
        {
            get { return _canSignUpProp; }
            set { SetProperty(ref _canSignUpProp, value); }
        }
        private DelegateCommand _signUpCommand;
        public DelegateCommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new DelegateCommand(SignUp).ObservesProperty(() => CanSignUpProp));

	    private void SignUp()
	    {
	        Debug.WriteLine("sign up");
	    }

	    public LoginPageViewModel()
        {

        }
	}
}
