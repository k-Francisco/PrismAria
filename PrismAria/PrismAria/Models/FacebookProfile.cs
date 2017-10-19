using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PrismAria.Models
{
    public class FacebookProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("picture")]
        public Picture Picture { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("cover")]
        public Cover Cover { get; set; }
        [JsonProperty("age_range")]
        public AgeRange AgeRange { get; set; }
        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class Picture
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
    public class Data
    {
        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
    public class Cover
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("offset_x")]
        public int Offsetx { get; set; }
        [JsonProperty("offset_y")]
        public int Offsety { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
    }
    public class AgeRange
    {
        [JsonProperty("max")]
        public int Max { get; set; }
        [JsonProperty("min")]
        public int Min { get; set; }
    }
    public class Device
    {
        [JsonProperty("os")]
        public string Os { get; set; }
    }

}
