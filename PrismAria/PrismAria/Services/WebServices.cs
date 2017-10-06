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
        private string localAriaUrl = "http://192.168.1.15/Aria/public";
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
            var contents = new StringContent(body);
            contents.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");

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
                "\"bio\":\"" + "bio ni" + "\"," +
                "\"pic\":\"" + pic + "\"}");


            try {
                var postResult = await client.PostAsync( localAriaUrl + "/api/saveUser", contents);
                var result = postResult.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
            
        }

        public async Task<bool> CreateBand(string userId)
        {
            isSuccess = false;
            var contents = CreateBody("{\"band_name\":\"loom band\"," +
                "\"band_role\":\"vocalist\"," +
                "\"user_id\":\""+ userId +"\"," +
                "}");

            try
            {
                var post = await client.PostAsync(localAriaUrl+"/api/createBand",contents);
                var result = post.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                    isSuccess = true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            return isSuccess;
        }

    }
}
