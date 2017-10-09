using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrismAria.Services
{
    public class WebServices
    {
        private HttpClient client;
        private Boolean isSuccess = false;
        private string localAriaUrl = "http://192.168.1.11/Aria/public";
        //private string localAriaUrl = "http://ariaitproject.herokuapp.com";
        public WebServices() {
            client = CreateClient();
        }

        public HttpClient CreateClient() {
            HttpClient client = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(item: new NameValueHeaderValue("odata", "verbose"));
            client.DefaultRequestHeaders.Accept.Add(mediaType);

            return client;
        }

        public StringContent CreateBody(string body) {
            //var contents = new StringContent(body);
            //contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            var contents = new StringContent(body, Encoding.UTF8, "application/json");
            return contents;
        }

        public async Task<bool> RegisterUser(string userId, string fname, string lname, string completeName, string address, string pic)
        {
            isSuccess = false;
            var contents = CreateBody("{\"user_id\":\"" + userId + "\"," +
                "\"fname\":\"" + fname + "\"," +
                "\"lname\":\"" + lname + "\"," +
                "\"fullname\":\"" + "" + "\"," +
                "\"email\":\"" + "emailni@gmail.com" + "\"," +
                "\"age\":\"" + "17" + "\"," +
                "\"gender\":\"" + "Male" + "\"," +
                "\"address\":\"" + address + "\"," +
                "\"contact\":\"" + "09178882349" + "\"," +
                "\"bio\":\"" + "" + "\"," +
                "\"pic\":\"" + pic + "\"}");
            try {
                var postResult = await client.PostAsync( localAriaUrl + "/api/saveUser", contents);
                var result = postResult.EnsureSuccessStatusCode();
                if (postResult.IsSuccessStatusCode)
                    isSuccess = true;
                
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
            
        }

        public async Task<string> GetUsers()
        {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/users");
            }
            catch (Exception e)
            {
                Debug.WriteLine("error"+e.Message);
                return e.Message;
            }

        }


        public async Task<string> GetBands() {

            try {
                return await client.GetStringAsync(localAriaUrl + "/api/getBands");
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return e.Message;
            }

        }

        public async Task<bool> CreateBand(string userId, string bandName, string bandRole)
        {
            isSuccess = false;
            var contents = CreateBody("{" +
                "\"user_id\":\"" + userId + "\"," +
                "\"band_role_create\":\"" + bandRole + "\"," +
                "\"band_name\":\"" + bandName + "\"" +
                "}");

            try
            {
                var post = await client.PostAsync(localAriaUrl+"/api/createBand",contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;

                var data = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(data);
                Debug.WriteLine("success");
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> AddBandMember(string bandId, string userId, string bandRole) {

            isSuccess = false;
            var contents = CreateBody("{" +
                "\"add-band-member-id\":\"" + "1748536635159896" + "\"," +
                "\"add-band-member-band-id\":\"" + "21" + "\"," +
                "\"add-band-member-role\":\"" + "Vocalist" + "\"" +
                "}");

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/addmember", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> EditBandDetails(string bandId) {
            isSuccess = false;
            //var contents = CreateBody("{\"band_id\":\"" + bandId + "\"," +
            //    "\"user_id\":\"" + userId + "\"," +
            //    "\"band_role\":\"" + bandRole + "\"," +
            //    "}");

            //try
            //{
            //    var post = await client.PostAsync(localAriaUrl + "/api/addbandmember", contents);
            //    var result = post.EnsureSuccessStatusCode();
            //    if (result.IsSuccessStatusCode)
            //        isSuccess = true;
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //}

            return isSuccess;
        }

        public async Task<string> GetBandAlbum(string bandId) {

            try
            {
                return await client.GetStringAsync(localAriaUrl+"/api/albums?band_id=" + bandId);
            }
            catch (Exception e) {
                return e.Message;
            }

        }

        public async Task<string> GetBandVideos(string bandId)
        {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/videos?band_id=" + bandId);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<string> GetBandSongs(string bandId)
        {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/songs?band_id=" + bandId);
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<string> GetGenres()
        {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/genres");
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<bool> AddAlbum() {
            isSuccess = false;

            var contents = CreateBody(
                "{" +
                "\"album_name\":\"" + "album numba wan" + "\"," +
                "\"album_desc\":\"" + "asus numba wan" + "\"," +
                "\"band_id\":\"" + "1" + "\"" +
                "}"
                );

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/addAlbum", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;

                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

    }
}
