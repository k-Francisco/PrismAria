using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class PlistModel
    {
        [JsonProperty("genre_id")]
        public int GenreId { get; set; }
        [JsonProperty("song_id")]
        public int SongId { get; set; }
        [JsonProperty("pl_id")]
        public int PlId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

}
