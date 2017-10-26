using GetLocalFilePath.Plugin;
using Plugin.FilePicker;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.MediaManager;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace PrismAria.ViewModels
{
	public class BandCreationPageViewModel : BindableBase
	{
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        private MediaFile _mediaFile;
        private ObservableCollection<string> _roleList= new ObservableCollection<string>() {
             "Vocalist",
            "Guitarist",
            "Drummer",
            "Pianist",
            "Basist",
            "Pakapin bobot"
        };
        public ObservableCollection<string> RoleList
        {
            get { return _roleList; }
            set { SetProperty(ref _roleList, value); }
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

        private string _bandDesc;
        public string BandDesc
        {
            get { return _bandDesc; }
            set { SetProperty(ref _bandDesc, value); }
        }

        private ImageSource _bandPic;
        public ImageSource BandPic
        {
            get { return _bandPic; }
            set { SetProperty(ref _bandPic, value); }
        }

        private DelegateCommand _pickBandImageCommand;
        public DelegateCommand PickBandImageCommand =>
            _pickBandImageCommand ?? (_pickBandImageCommand = new DelegateCommand(PickBandImage));

        private async void PickBandImage()
        {  
            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                await pageDialogService.DisplayAlertAsync("Error", "The device deos not support picking of photos", "Ok");
                return;
            }
            
            
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null)
                    return;


            
            BandPic = ImageSource.FromFile(_mediaFile.Path);
        }

        private DelegateCommand _createBandCommand;
        public DelegateCommand CreateBandCommand =>
            _createBandCommand ?? (_createBandCommand = new DelegateCommand(CreateBand));

        private async void CreateBand()
        {
            //await Singleton.Instance.webService.EditBandPic(_mediaFile);   
            //await Singleton.Instance.webService.AddAlbum("1st album","album shot", _mediaFile, "1");

            var shet = await CrossFilePicker.Current.PickFile();
            await Singleton.Instance.webService.AddSongs("1", "description here", shet.DataArray, "1", "1", "song name here");
        }

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(GoBack));

        private void GoBack()
        {
            navigationService.GoBackAsync();
        }

        public BandCreationPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            CrossMedia.Current.Initialize();
            _bandPic = "sample_pic.png";
        }
	}
}
