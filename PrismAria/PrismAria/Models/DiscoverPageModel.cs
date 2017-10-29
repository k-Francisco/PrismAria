using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace PrismAria.Models
{
    public class DiscoverPageModel
    {
        public string categoryName { get; set; }
        public List<BandModel> bandList { get; set; }
    }

    public class BandModel
    {
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("band_name")]
        public string BandName { get; set; }
        [JsonProperty("band_desc")]
        public string BandDesc { get; set; }
        [JsonProperty("num_followers")]
        public object NumFollowers { get; set; }
        [JsonProperty("visit_counts")]
        public object VisitCounts { get; set; }
        [JsonProperty("band_pic")]
        public string BandPic { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        public DelegateCommand<BandModel> BandClick { get; set; }
    }

}
