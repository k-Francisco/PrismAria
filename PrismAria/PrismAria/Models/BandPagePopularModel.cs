using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class BandPagePopularModel
    {
        [JsonProperty("songs")]
        public List<Song> Songs { get; set; }
        [JsonProperty("album")]
        public Album Album { get; set; }
    }
    public class Song
    {
        [JsonProperty("song_id")]
        public int SongId { get; set; }
        [JsonProperty("song_title")]
        public string SongTitle { get; set; }
        [JsonProperty("song_desc")]
        public string SongDesc { get; set; }
        [JsonProperty("song_audio")]
        public string SongAudio { get; set; }
        [JsonProperty("num_plays")]
        public object NumPlays { get; set; }
        [JsonProperty("genre_id")]
        public int GenreId { get; set; }
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        public string AlbumPic { get; set; }
        public bool isPlaying { get; set; }
    }
    public class SongAlbum
    {
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("album_name")]
        public string AlbumName { get; set; }
        [JsonProperty("album_desc")]
        public string AlbumDesc { get; set; }
        [JsonProperty("album_pic")]
        public string AlbumPic { get; set; }
        [JsonProperty("num_likes")]
        public int NumLikes { get; set; }
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }


}
