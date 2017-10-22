using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class ArticlesModel
    {
        [JsonProperty("art_id")]
        public int ArtId { get; set; }
        [JsonProperty("art_title")]
        public string ArtTitle { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        public string BandName { get; set; }
        public string BandPic { get; set; }
    }

}
