using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberNotificationPage : ContentPage
    {
        public SubscriberNotificationPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Notifications";
        }
    }
}
