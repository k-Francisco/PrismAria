﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using PrismAria.Helpers;
using PrismAria.Models;
using PrismAria.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PrismAria.ViewModels
{
    public class CreateBandPopupPageViewModel : BindableBase
    {
        private readonly WebServices webServices;

        private ObservableCollection<string> _bandRoles = new ObservableCollection<string>() {
                "Vocalist",
                "Guitarist",
                "Drummer",
                "Pianist",
                "Basist"
        };
        public ObservableCollection<string> BandRoles
        {
            get { return _bandRoles; }
            set { SetProperty(ref _bandRoles, value); }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        private string _bandName;
        public string BandName
        {
            get { return _bandName; }
            set { SetProperty(ref _bandName, value); }
        }

        private DelegateCommand _createBandCommand;
        public DelegateCommand CreateBandCommand =>
            _createBandCommand ?? (_createBandCommand = new DelegateCommand(CreateBand));

        private async void CreateBand()
        {
            
            var fbprofile = new FacebookProfile();
            fbprofile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
            //Debug.WriteLine("Band name: " + _bandName + "\nBand Role: " + _bandRoles[_selectedIndex] + "\nUser id: " + fbprofile.Id);
            //await webServices.AddBandMember(fbprofile.Id, _bandName, _bandRoles[_selectedIndex]);
            Debug.WriteLine(await webServices.GetBandAlbum("1"));
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private void Close()
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        public CreateBandPopupPageViewModel()
        {
            webServices = new WebServices();
        }
	}
}
