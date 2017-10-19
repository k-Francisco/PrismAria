using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Models
{
    public class UserModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("fname")]
        public string Fname { get; set; }
        [JsonProperty("lname")]
        public string Lname { get; set; }
        [JsonProperty("fullname")]
        public string Fullname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("age")]
        public object Age { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("contact")]
        public string Contact { get; set; }
        [JsonProperty("bio")]
        public object Bio { get; set; }
        [JsonProperty("profile_pic")]
        public string ProfilePic { get; set; }
        [JsonProperty("remember_token")]
        public object RememberToken { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }


}
