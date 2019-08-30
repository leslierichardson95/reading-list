using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReadingList.Controllers
{
    public class HttpHelper
    {
        private HttpClient Client;
        private string BaseUrl;

        public HttpHelper(string baseUrl)
        {
            Client = new HttpClient();
            BaseUrl = baseUrl;
        }

        public async Task<T> GetAsync<T>(string extraUrl)
        {
            string url = BaseUrl + "" + extraUrl;
            HttpResponseMessage response = await Client.GetAsync(url);
            T result;
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                result = default(T);
            }
            return result;
        }

        public async Task<string> GetAsStringAsync(string extraUrl)
        {
            string url = BaseUrl + "" + extraUrl;
            HttpResponseMessage response = await Client.GetAsync(url);
            string result;
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = default(string);
            }
            return result;
        }

        public async Task PutAsync(string extraUrl, long id)
        {
            //T result = default(T);
            string url = BaseUrl + "" + extraUrl;
            List<KeyValuePair<string, string>> content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("parameter", id.ToString())
            };
            HttpResponseMessage response = await Client.PutAsync(url, new FormUrlEncodedContent(content));
        }
    }
}
