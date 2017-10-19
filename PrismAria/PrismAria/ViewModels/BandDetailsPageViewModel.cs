using Prism.Commands;
using Prism.Mvvm;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class BandDetailsPageViewModel : BindableBase
	{
        private Singleton _singleton;
        private BandDetailsService _service;
        
        public ObservableCollection<BandMembersModel> BandMembersCollection
        {
            get { return _singleton.BandMemberCollection; }
            set { SetProperty(ref _singleton.BandMemberCollection, value); }
        }

        private int _bandMemberCollectionHeight = 0;
        public int BandMemberCollectionHeight
        {
            get { return _bandMemberCollectionHeight; }
            set { SetProperty(ref _bandMemberCollectionHeight, value); }
        }

        public BandDetailsPageViewModel()
        {
            _singleton = Singleton.Instance;
            _service = new BandDetailsService();
            for(int i = 0; i < 5; i++)
            {
                _service.AddBandMembers(_singleton.BandMemberCollection);
            }
            _bandMemberCollectionHeight = (_singleton.BandMemberCollection.Count/2) * 80;
        }
	}
}
