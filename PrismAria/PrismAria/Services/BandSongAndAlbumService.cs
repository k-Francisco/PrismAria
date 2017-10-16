using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria.Services
{
    public class BandSongAndAlbumService
    {
        public void AddSongs(ObservableCollection<BandPagePopularModel> collection) {
            collection.Add(new BandPagePopularModel() { SongName = "Sample song here", SongAlbum="album", SongListenedCount = "0"});
        }

        public void AddAlbum(ObservableCollection<BandPageAlbum> collection) {
            collection.Add(new BandPageAlbum() { AlbumPic = "sample_pic.png", AlbumTitle = "Album Title"});
        }
    }
}
