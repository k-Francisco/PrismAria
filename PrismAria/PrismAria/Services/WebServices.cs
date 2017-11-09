using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using PrismAria.Helpers;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public string localAriaUrl = "http://192.168.254.106/Aria/public";
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

        public async Task<bool> RegisterUser(string userId, string fname, string lname, string completeName, string gender, string pic)
        {
            isSuccess = false;
            var contents = CreateBody("{\"user_id\":\"" + userId + "\"," +
                "\"fname\":\"" + fname + "\"," +
                "\"lname\":\"" + lname + "\"," +
                //"\"fullname\":\"" + "" + "\"," +
                "\"email\":\"" + "emailni@gmail.com" + "\"," +
                "\"age\":\"" + "" + "\"," +
                "\"gender\":\"" + gender + "\"," +
                "\"address\":\"" + "" + "\"," +
                "\"contact\":\"" + "" + "\"," +
                "\"bio\":\"" + "" + "\"," +
                "\"pic\":\"" + pic + "\"" +
                "}");
            try {
                var postResult = await client.PostAsync(localAriaUrl + "/api/saveUser", contents);
                //var postResult = await client.PostAsync(localAriaUrl + "/api/saveUser?" +
                //    "user_id=" + userId +
                //    "&fname=" + fname +
                //    "&lname=" + lname +
                //    "&email=" + "emailni@gmail.com" +
                //    "&age=" + "" +
                //    "&gender=" + gender +
                //    "&address=" + "" +
                //    "&contact=" + "" +
                //    "&bio=" + "" +
                //    "&pic=" + pic, contents);
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

        public async Task<bool> CreateBand(string userId, string bandName, string bandDesc,string bandRole, MediaFile pic, int firstGenre, int secondGenre)
        {
            isSuccess = false;

            var contents = CreateBody("{" +
                "\"user_id\":\"" + userId + "\"," +
                "\"band_role_create\":\"" + bandRole.ToString() + "\"," +
                "\"band_name\":\"" + bandName + "\"," +
                "\"genre_select_1\":\"" + firstGenre + "\"," +
                "\"genre_select_2\":\"" + secondGenre + "\"," +
                "\"bandDescr\":\"" + bandDesc + "\"" +
                "}");

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/createBand", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    isSuccess = true;
                    var data = JsonConvert.DeserializeObject<BandMembersModel>(await result.Content.ReadAsStringAsync());
                    await EditBandPic(pic, data.Band.Id.ToString());
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> GetAllBandMembers() {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/members");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetMembersOfTheBand(string bandId) {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/bandmembers?band_id="+bandId);
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

        public async Task<bool> EditBandPic(MediaFile file, string bandId) {
            isSuccess = false;
            
            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(bandId), "\"bandId\"");
            contents.Add(new StreamContent(file.GetStream()), "\"bandPic\"", file.Path);
            

            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/editbandPic", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
                

                var data = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(data);
                Debug.WriteLine("success");
                await Singleton.Instance.CollectionService.PopulateUserBands();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> GetAllSongs()
        {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/songs");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> AddSongs(string albumId, string songDesc, byte[] song, string genreId, string bandId, string songName) {
            isSuccess = false;
            
            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(albumId), "\"album_id\"");
            contents.Add(new StringContent(songName), "\"song_title\"");
            contents.Add(new StringContent(songDesc), "\"song_desc\"");
            contents.Add(new StringContent(genreId), "\"genre_id\"");
            contents.Add(new StringContent(bandId), "\"band_id\"");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(song);
            contents.Add(new StreamContent(stream), "\"song_audio\"", songName + ".mp3");
            try
            {
                var post = await client.PostAsync(localAriaUrl + "/api/addSongs", contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;

                var data = JsonConvert.DeserializeObject<Song>(await result.Content.ReadAsStringAsync());
                Singleton.Instance.BandSongCollection.Add(data);
                Debug.WriteLine("success");
                stream.Flush();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> EditSong(string songId, string songName, string songDesc, string genreId)
        {
            isSuccess = false;

            try
            {
                var response = await client.GetAsync(localAriaUrl+ "/api/updateSong?song_id=" + songId+ "&song_title="+songName+ "&song_desc="+songDesc+ "&genre_id="+genreId);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                var data = JsonConvert.DeserializeObject<Song>(await response.Content.ReadAsStringAsync());
                Singleton.Instance.toBeModifiedSong = data;
                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> DeleteSong(string songId) {
            isSuccess = false;
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/deleteSong?song_id=" + songId);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                var dummy = Singleton.Instance.BandSongCollection;
                foreach(var item in dummy)
                {
                    if (songId.Equals(item.SongId.ToString()))
                    {
                        Singleton.Instance.BandSongCollection.Remove(item);
                        break;
                    }
                }
                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> AddPlaylist(string title, string desc, MediaFile image) {

            string data;

            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(Settings.Token), "\"user_id\"");
            contents.Add(new StringContent(title), "\"pl_title\"");
            contents.Add(new StringContent(desc), "\"pl_desc\"");
            contents.Add(new StreamContent(image.GetStream()), "\"pl_image\"", image.Path);


            try
            {
                var response = await client.PostAsync(localAriaUrl + "/api/AddPlayList", contents);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                data = await response.Content.ReadAsStringAsync();
                return data;

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return e.Message;
            }
        }

        public async Task<bool> UpdatePlaylist(string playlistId, string title, string desc, MediaFile image)
        {
            isSuccess = false;

            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(playlistId), "\"pl_id\"");
            contents.Add(new StringContent(title), "\"pl_title\"");
            contents.Add(new StringContent(desc), "\"pl_desc\"");
            contents.Add(new StreamContent(image.GetStream()), "\"pl_image\"", image.Path);


            try
            {
                var response = await client.PostAsync(localAriaUrl + "/api/updateplaylist", contents);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> DeletePlaylist(string playlistId)
        {
            isSuccess = false;
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/DeletePlayList?pl_id="+playlistId);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<string> GetAllPlaylist()
        {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/getAllPlaylist");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetAllPlist() {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/getAllPlist");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> AddSongToPlaylist(string songId, string genreId, string playlistId)
        {
            isSuccess = false;
            try
            {
                var response = await client.GetAsync(localAriaUrl + "/api/addSongToPlaylist?song_id="+songId+"&genre_id="+genreId+"&pl_id="+playlistId);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    isSuccess = true;

                Debug.WriteLine("success");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

        public async Task<bool> RemoveSongFromPlaylist(string playlistId, string songId) {

            isSuccess = false;

            try
            {
                var response = await client.GetAsync(localAriaUrl+ "/api/removeSongFromPlaylist?pl_id="+playlistId+"&song_id="+songId);
                var ensure = response.EnsureSuccessStatusCode();
                if (ensure.IsSuccessStatusCode)
                    isSuccess = true;

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

        public async Task<string> GetAllAlbums()
        {
            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/AllAlbums");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<bool> AddAlbum(string albumName, string albumDesc, MediaFile albumPic, string bandId) {
            isSuccess = false;

            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(bandId), "\"band_id\"");
            contents.Add(new StringContent(albumName), "\"album_name\"");
            contents.Add(new StringContent(albumDesc), "\"album_desc\"");
            contents.Add(new StreamContent(albumPic.GetStream()), "\"album_pic\"", albumPic.Path);

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

        public async Task<bool> EditAlbum(string albumName, string albumDesc, string albumId, string bandId) {
            isSuccess = false;
            
            var contents = CreateBody(
                "{" +
                "\"album_name\":\"" + albumName + "\"," +
                "\"album_desc\":\"" + albumDesc + "\"," +
                "\"album_id\":\"" + albumId + "\"," +
                "\"band_id\":\"" + bandId + "\"" +
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

        public async Task<bool> DeleteAlbum(string albumId){
            isSuccess = false;

            try
            {
                var post = await client.GetAsync(localAriaUrl + "/api/deleteAlbum?album_id="+ albumId);
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

        public async Task<string> GetBandArticles(string bandId) {

            try
            {
                return await client.GetStringAsync(localAriaUrl + "/api/bandarticles?band_id="+bandId);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
