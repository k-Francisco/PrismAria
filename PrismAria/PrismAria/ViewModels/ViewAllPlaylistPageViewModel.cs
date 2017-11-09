using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismAria.ViewModels
{
	public class ViewAllPlaylistPageViewModel : BindableBase
	{
        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;

        public ObservableCollection<PlaylistModel> Playlists
        {
            get { return Singleton.Instance.UserPlaylists; }
            set { SetProperty(ref Singleton.Instance.UserPlaylists, value); }
        }


        private DelegateCommand _gobackCommand;
        public DelegateCommand GoBackCommand =>
            _gobackCommand ?? (_gobackCommand = new DelegateCommand(GoBack));

        private void GoBack()
        {
            navigationService.GoBackAsync();
        }


        private DelegateCommand<PlaylistModel> _openPlaylistMenuCommand;
        public DelegateCommand<PlaylistModel> OpenPlaylistMenuCommand =>
            _openPlaylistMenuCommand ?? (_openPlaylistMenuCommand = new DelegateCommand<PlaylistModel>(OpenPlaylistMenu));

        private async void OpenPlaylistMenu(PlaylistModel obj)
        {
            var choice2 = await pageDialogService.DisplayActionSheetAsync("Options", "Close", "", new string[] {
                    "Edit Playlist",
                    "Delete Playlist"
                });

            if (choice2.Equals("Edit Playlist"))
            {
                
            }
            if (choice2.Equals("Delete Playlist"))
            {
                var delete = await pageDialogService.DisplayAlertAsync("","Do you really want to delete this playlist?", "OK", "CANCEL");
                if (delete)
                {
                    if(await Singleton.Instance.webService.DeletePlaylist(obj.PlId.ToString()))
                    {
                        Singleton.Instance.UserPlaylists.Remove(obj);
                    }
                    
                }
            }
        }

        public ViewAllPlaylistPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
        }

    }
}
