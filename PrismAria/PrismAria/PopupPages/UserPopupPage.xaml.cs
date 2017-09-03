using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismAria.PopupPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPopupPage : PopupPage
	{
        private string _currentPic, name;
		public UserPopupPage (string currentPic, string name)
		{
			InitializeComponent ();
            BindingContext = this;
            this.name = name;
            _currentPic = currentPic;

            Picture.Source = currentPic;
            Name.Text = name;
		}

	}
}