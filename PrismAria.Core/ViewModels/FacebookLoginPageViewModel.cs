using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrismAria.Services;

namespace PrismAria.ViewModels
{
	public class FacebookLoginPageViewModel : BindableBase
	{
        public FacebookLoginPageViewModel()
        {
            
        }
	    public async Task SetFacebookUserProfileAsync(string accessToken)
	    {
	        var facebookServices = new FacebookLoginService();

	        var Fbprofile = await facebookServices.GetFacebookProfileAsync(accessToken);
	    }
    }
}
