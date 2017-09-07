using Rg.Plugins.Popup.Extensions;
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
		public UserPopupPage ()
		{
			InitializeComponent ();
            BindingContext = this;
		}

        public void ClosePopup(object sender, EventArgs e) {
            Navigation.PopPopupAsync(true);
        }

	}
}