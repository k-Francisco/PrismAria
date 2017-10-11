using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberFeed : ContentPage
    {
        public SubscriberFeed()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Articles";
        }
    }
}
