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
	public partial class LoadingPopupPage : PopupPage
	{
		public LoadingPopupPage ()
		{
			InitializeComponent ();
		}
	}
}