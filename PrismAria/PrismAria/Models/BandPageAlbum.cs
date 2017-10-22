using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    //public class BandPageAlbum
    //{
    //    public string AlbumPic { get; set; }
    //    public string AlbumTitle { get; set; }
    //    public string AlbumLikes { get; set; }
    //}

    public class BandPageAlbum
    {
        [JsonProperty("band")]
        public Band Band { get; set; }
        [JsonProperty("albums")]
        public List<Album> Albums { get; set; }
    }
    public class Band
    {
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("band_name")]
        public string BandName { get; set; }
        [JsonProperty("band_desc")]
        public string BandDesc { get; set; }
        [JsonProperty("num_followers")]
        public int NumFollowers { get; set; }
        [JsonProperty("visit_counts")]
        public int VisitCounts { get; set; }
        [JsonProperty("band_pic")]
        public string BandPic { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
    public class Album
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
        public object NumLikes { get; set; }
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }


}
