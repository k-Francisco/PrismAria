using Xamarin.Forms;

namespace PrismAria.Views
{
    public partial class SubscriberLeaderboardPage : ContentPage
    {
        public SubscriberLeaderboardPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform.Equals(Device.iOS))
                this.Title = "Leaderboard";
        }
    }
}
