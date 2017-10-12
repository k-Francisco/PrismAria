using Newtonsoft.Json;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Services
{
    public class UserBandsService
    {
        private ObservableCollection<UserBandModel> _userBands = new ObservableCollection<UserBandModel>();
        FacebookProfile profile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
        public ObservableCollection<UserBandModel> GetUserBands() {
            return _userBands;
        }

        public void AddBands(string bandName, string bandRole, string bandPic) {
            _userBands.Add(new UserBandModel() { userBandName = "Band Name Here", userBandImage = ImageSource.FromFile(bandPic) });
        }
    }
}
