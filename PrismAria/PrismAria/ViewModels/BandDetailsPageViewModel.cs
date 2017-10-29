using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class BandDetailsPageViewModel : ChildViewBaseModel, INavigatedAware
	{
        private BandModel _band;

        private string _bandPic;
        public string BandPic
        {
            get { return _bandPic; }
            set { SetProperty(ref _bandPic, value); }
        }

        private string _bandName;
        public string BandName
        {
            get { return _bandName; }
            set { SetProperty(ref _bandName, value); }
        }

        private string _bandDesc;
        public string BandDesc
        {
            get { return _bandDesc; }
            set { SetProperty(ref _bandDesc, value); }
        }


        public ObservableCollection<Member> BandMembersCollection
        {
            get { return Singleton.Instance.BandMemberCollection; }
            set { SetProperty(ref Singleton.Instance.BandMemberCollection, value); }
        }

        private int _bandMemberCollectionHeight = 0;
        private readonly IPageDialogService pageDialogService;

        public int BandMemberCollectionHeight
        {
            get { return _bandMemberCollectionHeight; }
            set { SetProperty(ref _bandMemberCollectionHeight, value); }
        }

        public BandDetailsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService):base(navigationService)
        {
            IsActiveChanged += HandleIsActive;
            IsActiveChanged += HandleIsNotActive;
            this.pageDialogService = pageDialogService;
        }

        private void HandleIsNotActive(object sender, EventArgs e)
        {
           
        }

        private void HandleIsActive(object sender, EventArgs e)
        {
            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActive;
            IsActiveChanged -= HandleIsNotActive;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (HasInitialized == true) return;
            HasInitialized = true;
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            _band = (BandModel)parameters["model"];
            BandName = _band.BandName;
            BandPic = _band.BandPic;
            BandDesc = _band.BandDesc;
            Singleton.Instance.currBandId = _band.BandId;
            if (_isConnected)
            {
                try
                {
                    if (!Singleton.Instance.BandMemberCollection.Any())
                    {
                        var data = JsonConvert.DeserializeObject<Member[]>(await Singleton.Instance.webService.GetMembersOfTheBand(_band.BandId.ToString()));
                        var users = JsonConvert.DeserializeObject<UserModel[]>(await Singleton.Instance.webService.GetUsers());
                        foreach (var item in data.ToList())
                        {
                            foreach (var item2 in users.ToList())
                            {
                                if (item.UserId == item2.UserId)
                                {
                                    Singleton.Instance.BandMemberCollection.Add(new Member() { memberName = item2.Fullname, Bandrole = item.Bandrole, UserId = item2.UserId, memberPic = item2.ProfilePic });
                                }
                            }
                        }
                        BandMemberCollectionHeight = Singleton.Instance.BandMemberCollection.Count * 80;
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
