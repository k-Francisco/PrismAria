using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services;
using PrismAria.Events;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class EditPopupPageViewModel : BindableBase
	{

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

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private void Close()
        {
            PopupNavigation.Instance.PopAllAsync();
        }

        private DelegateCommand _editCommand;
        private readonly IEventAggregator eventAggregator;
        private readonly IPageDialogService pageDialogService;

        public DelegateCommand EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand(Edit));

        private async void Edit()
        {
            var success = await Singleton.Instance.webService.EditSong(Singleton.Instance.toBeModifiedSong.SongId.ToString(), 
                SongName, 
                SongDesc, 
                (SelectedIndex+1).ToString());

            if (success)
            {
                eventAggregator.GetEvent<EditSongEvent>().Publish();
                Close();
            }
            else
            {
                Close();
                await pageDialogService.DisplayAlertAsync("Oops!", "There was a problem editing the song", "ok");
            }
        }

        public EditPopupPageViewModel(IEventAggregator eventAggregator, IPageDialogService pageDialogService)
        {
            SongName = Singleton.Instance.toBeModifiedSong.SongTitle;
            SongDesc = Singleton.Instance.toBeModifiedSong.SongDesc;

            foreach(var item in Singleton.Instance.genres)
            {
                if(item.id == Singleton.Instance.toBeModifiedSong.GenreId)
                {
                    SelectedIndex = item.id-1;
                    break;
                }
            }

            this.eventAggregator = eventAggregator;
            this.pageDialogService = pageDialogService;
        }
	}
}
