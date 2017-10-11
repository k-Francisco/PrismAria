
using PrismAria.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismAria.PopupPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPopupPage : PopupPage
	{
        private readonly bool isSubscriber;

        public UserPopupPage (bool isSubscriber)
		{
			InitializeComponent ();
            this.isSubscriber = isSubscriber;
            var picTapped = new TapGestureRecognizer();
            picTapped.Tapped += (sender, e) => {
                var vm = BindingContext as UserPopupPageViewModel;
                vm.WhereToNavigate(isSubscriber);
            };
            UserPiciOS.GestureRecognizers.Add(picTapped);
            UserPicAndroid.GestureRecognizers.Add(picTapped);
        }
	}
}