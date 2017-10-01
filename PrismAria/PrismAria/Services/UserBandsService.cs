using Newtonsoft.Json;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria.Services
{
    public class UserBandsService
    {
        private ObservableCollection<UserBandModel> _userBands = new ObservableCollection<UserBandModel>();
        public ObservableCollection<UserBandModel> GetUserBands() {
            if(_userBands.Count == 0)
            {
                var profile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
                _userBands.Add(new UserBandModel() { userBandName="Band Name Here", userBandImage = profile.Cover.Source });
                _userBands.Add(new UserBandModel() { userBandName = "Band Name Here", userBandImage = profile.Cover.Source });
                _userBands.Add(new UserBandModel() { userBandName = "Band Name Here", userBandImage = profile.Cover.Source });
            }
            return _userBands;
        }
    }
}
