using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    //public class BandMembersModel
    //{
    //    public string MemberPic { get; set; }
    //    public string MemberName { get; set; }
    //    public string MemberRole { get; set; }
    //}

    public class BandMembersModel
    {
        [JsonProperty("band")]
        public Bandz Band { get; set; }
        [JsonProperty("member")]
        public Member Member { get; set; }
    }
    public class Bandz
    {
        [JsonProperty("band_name")]
        public string BandName { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
    public class Member
    {
        [JsonProperty("band_id")]
        public int BandId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("bandrole")]
        public string Bandrole { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        
        public string memberName { get; set; }
        public string memberPic { get; set; }
    }

}
