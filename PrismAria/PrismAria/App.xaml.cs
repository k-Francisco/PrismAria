﻿using System;
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
using Rg.Plugins.Popup.Services;
using DLToolkit.Forms.Controls;
using PrismAria.Controls;
using System.Collections.ObjectModel;
using Prism.Events;
using PrismAria.Events;

namespace PrismAria
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            FlowListView.Init();
            if (Settings.Token.Equals(string.Empty))
               await NavigationService.NavigateAsync("LoginPage", null, true, true);
            else {
               await NavigationService.NavigateAsync("RootPage/SubscriberLanding");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<UserPopupPage, UserPopupPageViewModel>();
            Container.RegisterTypeForNavigation<RootPage>();
            Container.RegisterTypeForNavigation<SubscriberLandingPage>("SubscriberLanding");
            Container.RegisterTypeForNavigation<SubscriberDIscoverPage>("Discover");
            Container.RegisterTypeForNavigation<SubscriberFavoritesPage>();
            Container.RegisterTypeForNavigation<SubscriberNotificationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<FacebookLoginPage, FacebookLoginPageViewModel>();
            Container.RegisterTypeForNavigation<SubscriberViewBandPage>();
            Container.RegisterTypeForNavigation<SubscriberFeed>();
            Container.RegisterTypeForNavigation<BandLandingPage>();
            Container.RegisterTypeForNavigation<BandDetailsPage>();
            Container.RegisterTypeForNavigation<BandSongsAndAlbumsPage>();
            Container.RegisterTypeForNavigation<BandStatisticsPage>();
            Container.RegisterTypeForNavigation<BandArticlesPage>();
            Container.RegisterTypeForNavigation<BandCreationPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<AddAlbumPopupPage, AddAlbumPopupPageViewModel>();
            Container.RegisterTypeForNavigation<SongsPage>("SongsPagez");
            Container.RegisterTypeForNavigation<AddSongPopupPage, AddSongPopupPageViewModel>();
            Container.RegisterTypeForNavigation<EditPopupPage, EditPopupPageViewModel>();
            Container.RegisterTypeForNavigation<EditAlbumPopupPage, EditAlbumPopupPageViewModel>();
            Container.RegisterTypeForNavigation<AddPlaylistPopupPage, AddPlaylistPopupPageViewModel>();
            Container.RegisterTypeForNavigation<ViewAllSongsInPlaylistPage>();
            Container.RegisterTypeForNavigation<ViewAllPlaylistPage>();
        }
    }

    public class RootPage : CustomNavigationPage, INavigatedAware
    {
        FacebookProfile _profile;
        public ICommand HelloCommand { get; private set; }
        ToolbarItem userPageItem = new ToolbarItem()
        {
            Icon = "ic_user.png",
        };

        private bool isSubscriber = true;
        private readonly IEventAggregator eventAggregator;
        

        public RootPage(IEventAggregator eventAggregator)
        {
            
            HelloCommand = new Command(ShowUserOption);
            userPageItem.Command = HelloCommand;
            this.ChildAdded += RootPage_ChildAdded;
            this.ChildRemoved += RootPage_ChildRemoved;
            this.eventAggregator = eventAggregator;
            this.BackgroundColor = Color.FromHex("#FAFAFA");
        }

        private void RootPage_ChildRemoved(object sender, ElementEventArgs e)
        {
            BarBackgroundColor = Color.FromHex("#FAFAFA");
            BarTextColor = Color.FromHex("#212121");
            if(ToolbarItems.Count == 0)
            ToolbarItems.Add(userPageItem);
        }

        private void RootPage_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element.AutomationId.Equals("BandLandingPage"))
            {
                isSubscriber = false;
                Singleton.Instance.isSubscriber = false;
            }
                

            if (e.Element.AutomationId.Equals("LandingPage") || e.Element.AutomationId.Equals("BandLandingPage"))
            {
                BarBackgroundColor = Color.FromHex("#FAFAFA");
                BarTextColor = Color.FromHex("#212121");
                ToolbarItems.Add(userPageItem);
            }
            else
            {
                BarBackgroundColor = Color.FromHex("#F79012");
                BarTextColor = Color.White;
                ToolbarItems.Remove(userPageItem);
            }
        }

        private void ShowUserOption(object obj)
        {
            if (PopupNavigation.Instance.PopupStack.Count == 0) {
                var popup = new UserPopupPage(isSubscriber);
                Navigation.PushPopupAsync(popup, true);
            }
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
