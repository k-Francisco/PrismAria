using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismAria.Models
{
    //public class UserBandModel
    //{
    //    public ImageSource userBandImage { get; set; }
    //    public string userBandName { get; set; }
    //}

    public class UserBandModelForEvent
    {
        public string userBandImage { get; set; }
        public string userBandName { get; set; }
        public string userBandRole { get; set; }
    }

    public class UserBandModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("bandrole")]
        public string Bandrole { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

}
