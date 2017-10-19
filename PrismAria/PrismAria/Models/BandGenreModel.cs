using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class BandGenreModel
    {
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("genre_id")]
        public int GenreId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

}
