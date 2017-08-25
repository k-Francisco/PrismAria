using Prism.Unity;
using PrismAria.Views;
using Xamarin.Forms;

namespace PrismAria
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("RootPage/SubscriberLanding");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootPage>();
            Container.RegisterTypeForNavigation<SubscriberLandingPage>("SubscriberLanding");
            Container.RegisterTypeForNavigation<SubscriberDIscoverPage>();
            Container.RegisterTypeForNavigation<SubscriberFavoritesPage>();
            Container.RegisterTypeForNavigation<SubscriberNotificationPage>();
            Container.RegisterTypeForNavigation<SubscriberLeaderboardPage>();
        }
    }

    class RootPage : NavigationPage
    {
        public RootPage()
        {
            this.BarTextColor = Color.FromHex("#2C3E50");
            
        }
    }
}
