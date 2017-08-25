using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await Task.WhenAll(
            //    this.ScaleTo(0,100, Easing.BounceIn));
        }
    }
}
