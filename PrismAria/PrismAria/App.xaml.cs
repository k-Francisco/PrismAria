using System;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Unity;
using PrismAria.Helpers;
using PrismAria.Models;
using PrismAria.Views;
using Xamarin.Forms;
using System.Diagnostics;
using PrismAria.PopupPages;
using Rg.Plugins.Popup.Extensions;
using PrismAria.ViewModels;

namespace PrismAria
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            if(Settings.Token.Equals(string.Empty))
                NavigationService.NavigateAsync("LoginPage");
            else
                NavigationService.NavigateAsync("RootPage/SubscriberLanding");

           
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<UserPopupPage, UserPopupPageViewModel>();
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
        public ICommand HelloCommand { get; private set; }

        public RootPage()
        {
            this.BarTextColor = Color.FromHex("#2C3E50");
            HelloCommand = new Command(ShowUserOption);
            ToolbarItems.Add(new ToolbarItem { Icon = "ic_discover.png",
                Command = HelloCommand });
        }

        private void ShowUserOption(object obj)
        {
            var popup = new UserPopupPage();
            Navigation.PushPopupAsync(popup, true);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            _profile = (FacebookProfile)parameters["profile"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
