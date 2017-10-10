using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
            //var media = CrossMedia.Current;
            //try
            //{
            //    var file = await media.PickPhotoAsync();

            //    while (File.ReadAllBytes(file.Path).Length == 0)
            //    {
            //        await Task.Delay(1000);
            //    }
            //    var upFileBytes = File.ReadAllBytes(file.Path);

            //    //MultipartFormDataContent content = new MultipartFormDataContent();
            //    ByteArrayContent baContent = new ByteArrayContent(upFileBytes);
            //    //content.Add(baContent, "File", "bandPic.ext");

            //    await webServices.EditBandPic(upFileBytes);

            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //}


            await webServices.EditBandDetails();
            //var fbprofile = new FacebookProfile();
            //fbprofile = JsonConvert.DeserializeObject<FacebookProfile>(Settings.Profile);
            //Debug.WriteLine("Band name: " + _bandName + "\nBand Role: " + _bandRoles[_selectedIndex] + "\nUser id: " + fbprofile.Id);
            //await webServices.CreateBand(fbprofile.Id, _bandName, _bandRoles[_selectedIndex]);
            //Debug.WriteLine(await webServices.GetBandAlbum("1"));
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
