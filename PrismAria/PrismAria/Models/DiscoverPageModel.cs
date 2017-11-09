using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace PrismAria.Models
{
    public class DiscoverPageModel
    {
        public string categoryName { get; set; }
        public ObservableCollection<BandModel> bandList { get; set; }
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
        [JsonProperty("weekly_score")]
        public object WeeklyScore { get; set; }
        [JsonProperty("band_score")]
        public object BandScore { get; set; }
        [JsonProperty("scored_updated_date")]
        public string ScoredUpdatedDate { get; set; }

        public DelegateCommand<BandModel> BandClick { get; set; }
    }

}
