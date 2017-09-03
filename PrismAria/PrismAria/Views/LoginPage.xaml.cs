using PrismAria.PopupPages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LogLog.Clicked += LogLog_Clicked;
        }

        private void LogLog_Clicked(object sender, System.EventArgs e)
        {
            var popPage = new Page1();
            Navigation.PushPopupAsync(popPage, true);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await Task.WhenAll(
            //    this.ScaleTo(0,100, Easing.BounceIn));
        }
    }
}
