using Prism.Navigation;
using Prism.Unity;
using PrismAria.Helpers;
using PrismAria.Models;
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
            if(!Settings.Token.Equals(string.Empty))
                NavigationService.NavigateAsync("LoginPage");
            else
                NavigationService.NavigateAsync("RootPage/SubscriberLanding");

           
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootPage>();
            Container.RegisterTypeForNavigation<SubscriberLandingPage>("SubscriberLanding");
            Container.RegisterTypeForNavigation<SubscriberDIscoverPage>("Discover");
            Container.RegisterTypeForNavigation<SubscriberFavoritesPage>();
            Container.RegisterTypeForNavigation<SubscriberNotificationPage>();
            Container.RegisterTypeForNavigation<SubscriberLeaderboardPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<FacebookLoginPage>();
        }
    }

    class RootPage : NavigationPage, INavigatedAware
    {
        FacebookProfile _profile;

        public RootPage()
        {
            this.BarTextColor = Color.FromHex("#2C3E50");
            ToolbarItems.Add(new ToolbarItem { Icon = "ic_search.png"});
            ToolbarItems.Add(new ToolbarItem { Icon = "ic_discover.png" });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _profile = (FacebookProfile)parameters["profile"];
            ToolbarItems.Add(new ToolbarItem { Icon = _profile.Picture.Data.Url});
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
