using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismAria.Events;
using PrismAria.Models;
using PrismAria.PopupPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class AddSongPopupPageViewModel : BindableBase
	{
        private FileData file;

        private string _songName;
        public string SongName
        {
            get { return _songName; }
            set { SetProperty(ref _songName, value); }
        }

        private string _songDesc;
        public string SongDesc
        {
            get { return _songDesc; }
            set { SetProperty(ref _songDesc, value); }
        }

        private string _fileTitle;
        public string FileTitle
        {
            get { return _fileTitle; }
            set { SetProperty(ref _fileTitle, value); }
        }

        private ObservableCollection<string> _genres = new ObservableCollection<string>() {
                "Alternative",
                "Blues",
                "Classical",
                "Country",
                "Dance",
                "Electronic",
                "HipHop",
                "Inspirational",
                "Jazz",
                "Opera",
                "Pop",
                "Punk",
                "R&B",
                "Rap",
                "Reggae",
                "Rock",
                "Romance",
                "Soul"
        };
        public ObservableCollection<string> Genres
        {
            get { return _genres; }
            set { SetProperty(ref _genres, value); }
        }


        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetProperty(ref _selectedIndex, value); }
        }

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));

        private void Cancel()
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        private DelegateCommand _pickFileCommand;
        public DelegateCommand PickFileCommand =>
            _pickFileCommand ?? (_pickFileCommand = new DelegateCommand(PickFile));

        private async void PickFile()
        {
            file = await CrossFilePicker.Current.PickFile();
            if (file == null)
                return;

            FileTitle = file.FileName;
        }

        private DelegateCommand _addSongCommand;
        private readonly IEventAggregator eventAggregator;

        public DelegateCommand AddSongCommand =>
            _addSongCommand ?? (_addSongCommand = new DelegateCommand(AddSong));

        private async void AddSong()
        {
            var model = new SongModel() {
            GenreId = (SelectedIndex + 1).ToString(),
            SongDesc = SongDesc,
            SongFile = file,
            SongName = SongName};
            eventAggregator.GetEvent<AddSongEvent>().Publish(model);
            await PopupNavigation.Instance.PushAsync(new LoadingPopupPage());
        }

        public AddSongPopupPageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
	}
}
