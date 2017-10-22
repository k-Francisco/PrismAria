using Plugin.Media.Abstractions;
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
        private string localAriaUrl = "http://192.168.254.106/Aria/public";
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
                "\"email\":\"" + "" + "\"," +
                "\"age\":\"" + "" + "\"," +
                "\"gender\":\"" + "Male" + "\"," +
                "\"address\":\"" + address + "\"," +
                "\"contact\":\"" + "09178882349" + "\"," +
                "\"bio\":\"" + "" + "\"," +
                "\"pic\":\"" + pic + "\"}");
            try {
                var postResult = await client.PostAsync(localAriaUrl + "/api/saveUser", contents);
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
                return await client.GetStringAsync(localAriaUrl + "/api/getusers");
            }
            catch (Exception e)
            {
                Debug.WriteLine("error" + e.Message);
                return e.Message;
            }

        }

        public async Task<string> GetUserPreference(string userId) {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/preferences?user_id="+userId);
            }
            catch (Exception e)
            {
                Debug.WriteLine("error" + e.Message);
                return e.Message;
            }
        }

        public async Task<string> GetUserHistory(string userId) {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/userhistory?user_id=" + userId);
            }
            catch (Exception e)
            {
                Debug.WriteLine("error" + e.Message);
                return e.Message;
            }
        }

        public async Task<string> GetBands() {

            try {
                return await client.GetStringAsync(localAriaUrl + "/api/bands");
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return e.Message;
            }

        }

        public async Task<string> GetBandGenres(string bandId) {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/bandgenres?band_id=" + bandId);
            }
            catch (Exception e)
            {
                Debug.WriteLine("error" + e.Message);
                return e.Message;
            }
        }

        public async Task<bool> CreateBand(string userId, string bandName, string bandRole)
        {
            isSuccess = false;
            var contents = CreateBody("{" +
                "\"user_id\":\"" + userId + "\"," +
                "\"band_role_create\":\"" + bandRole.ToString() + "\"," +
                "\"band_name\":\"" + bandName + "\"" +
                "}");

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/createBand", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;

                //var data = await result.Content.ReadAsStringAsync();
                //Debug.WriteLine(data);
                //Debug.WriteLine("success");
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> GetBandMembers() {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/getmembers");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> AddBandMember() {

            isSuccess = false;
            var contents = CreateBody("{" +
                "\"add-band-member-id\":\"" + "1642999552378791" + "\"," +
                "\"add-band-member-band-id\":\"" + "2" + "\"," +
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

        public async Task<bool> EditBandPic(System.IO.Stream bandPic) {
            isSuccess = false;
            //var contents = CreateBody("{" +
            //    "\"band_pic\":\"" + bandPic + "\"," +
            //    "\"band_id\":\"" + "2" + "\"" +
            //    "}");
            var contents = CreateBody("");

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/editbandPic?band_id=2&band_pic=" + bandPic, contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
                

                var data = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(data);
                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> EditBandDetails() {
            isSuccess = false;
            //var contents = CreateBody("{" +
            //    "\"bandId\":\"" + "1" + "\"," +
            //    "\"bandName\":\"" + "libang" + "\"," +
            //    "\"bandDesc\":\"" + "the description of tae" + "\"" +
            //    "}");

            var contents = CreateBody("");

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/editBand?band_id=1&band_name=pakyu&band_desc=shet", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;

                Debug.WriteLine(result.ReasonPhrase);
                var data = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(data);
                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> GetBandAlbum(string bandId) {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/bandalbums?band_id=" + bandId);
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

        public async Task<string> GetBandSongs(string albumId)
        {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/bandsongs?album_id=" + albumId);
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
            // wala pay picture kay shet
            //var contents = CreateBody(
            //    "{" +
            //    "\"album_name\":\"" + "album numba wan" + "\"," +
            //    "\"album_desc\":\"" + "asus numba wan" + "\"," +
            //    "\"band_id\":\"" + "1" + "\"" +
            //    "}"
            //    );
            var contents = CreateBody(
                ""
                );

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/addAlbum?"+"album_name=pak album&"+"album_desc=no desc&"+"band_id=1", contents);
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

        public async Task<bool> EditAlbum() {
            isSuccess = false;
            // wala pay picture kay shet
            var contents = CreateBody(
                "{" +
                "\"album_name\":\"" + "album numba two" + "\"," +
                "\"album_desc\":\"" + "asus numba two" + "\"," +
                "\"album_id\":\"" + "1" + "\"," +
                "\"band_id\":\"" + "1" + "\"" +
                "}"
                );

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/updateAlbum", contents);
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

        public async Task<bool> DeleteAlbum(){
            isSuccess = false;
            // wala pay picture kay shet
            var contents = CreateBody(
                ""
                );

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/deleteAlbum?album_id=2", contents);
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

        public async Task<bool> FollowBand(string userId, string bandId) {

            isSuccess = false;
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/followBand?user_id=" + userId + "&band_id=" + bandId);
                var result = response.EnsureSuccessStatusCode();
                if(result.IsSuccessStatusCode)
                isSuccess = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> UnFollowBand(string userId, string bandId)
        {

            isSuccess = false;
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/unfollowBand?user_id=" + userId + "&band_id=" + bandId);
                var result = response.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
                Debug.WriteLine("Success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async void VisitBand(string bandId) {
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/visitCount?band_id=" + bandId);
                var result = response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }
}
