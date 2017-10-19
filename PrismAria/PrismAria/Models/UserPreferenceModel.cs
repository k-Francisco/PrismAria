using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class UserPreferenceModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("album_id")]
        public object AlbumId { get; set; }
    }

}
