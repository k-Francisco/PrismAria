using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class PlaylistModel
    {
        [JsonProperty("pl_id")]
        public int PlId { get; set; }
        [JsonProperty("pl_title")]
        public string PlTitle { get; set; }
        [JsonProperty("pl_desc")]
        public string PlDesc { get; set; }
        [JsonProperty("pl_creator")]
        public string PlCreator { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("followers")]
        public object Followers { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

}
