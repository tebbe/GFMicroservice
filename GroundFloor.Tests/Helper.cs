using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GroundFloor.Tests
{
    public class Helper
    {
        HttpClient _client = new HttpClient();
        public async Task<T> GetDataFromApiAsync<T>(string token, string apiUrl)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }
        public async Task<int> GetDemoFromApiAsync<T>(string token, string apiUrl)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                var result = await _client.SendAsync(requestMessage);
                return (int)result.StatusCode;
            }
        }

        public async Task<T> PostDataFromApiAsync<T>(string token, string apiUrl, string data)
        {

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");

                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public async Task<T> PostDataFromApiAsync<T>(string token, string apiUrl)
        {

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());

                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public async Task<T> PutDataFromApiAsync<T>(string token, string apiUrl, string data)
        {

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");

                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public async Task<T> PutDataFromApiAsync<T>(string token, string apiUrl)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public async Task<T> DeleteDataFromApiAsync<T>(string token, string apiUrl)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }
        public async Task<T> GetDataFromApiAsync<T>(string token, string apiUrl,string data)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {
                string[] splitToken = token.Split(" ");
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(splitToken[0].Trim(), splitToken[1].Trim());
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await _client.SendAsync(requestMessage);
                var jsonString = await result.Content.ReadAsStringAsync();
                JObject m = JObject.Parse(jsonString);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
        }


        public async Task<string> GetTokenAsync()
        {
            var userName = "tiauser";
            var password = "tiauser";

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", userName),
                new KeyValuePair<string, string>("Password", password)
            });

            var myHttpClient = new HttpClient();
            var response = await _client.PostAsync("https://dev-api.premisehq.co/accounts/token", formContent);
            var jsonString = await response.Content.ReadAsStringAsync();
            JObject m = JObject.Parse(jsonString);
            return m["accessToken"].ToString();
        }

    }
}

