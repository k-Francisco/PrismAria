using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberFavoritesPage : ContentPage
    {
        public SubscriberFavoritesPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Favorites";
        }
    }
}
