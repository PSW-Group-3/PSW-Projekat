using HospitalLibrary.Core.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.HospitalConnection
{
    public class HospitalHTTPConnection : IHospitalConnection
    {
        private static HttpClient client;
        public bool StoreBlood(Blood blood)
        {
            client = new()
            {
                BaseAddress = new Uri("http://localhost:16177/")
            };

            return PutAsync(client, blood).Result;
        }

        static async Task<bool> PutAsync(HttpClient httpClient, Blood blood)
        {
            client.Timeout = TimeSpan.FromSeconds(120);
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(blood), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("api/Blood/Store", stringContent);

            response.EnsureSuccessStatusCode();

            string isSuccessful = await response.Content.ReadAsStringAsync();
            return Boolean.Parse(isSuccessful);
        }
    }
}
