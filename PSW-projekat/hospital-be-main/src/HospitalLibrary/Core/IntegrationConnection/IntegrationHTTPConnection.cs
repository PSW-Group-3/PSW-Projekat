using HospitalLibrary.Core.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.IntegrationConnection
{
    public class IntegrationHTTPConnection : IIntegrationConnection
    {
        private static HttpClient client;
        private static LoginUserDto user;
        private static Boolean integrationResponse;
        private static List<BloodRequestDTO> requests = new List<BloodRequestDTO>();
        public bool CheckIfExists(LoginUserDto _user)
        {
            client = new()
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
            user = _user;

            PostAsync(client).Wait();
            return integrationResponse;
        }


        public List<BloodRequestDTO> GetBloodRequests()
        {
            client = new()
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };

            SendGetAsync(client).Wait();
            return requests;
        }

        static async Task<Boolean> PostAsync(HttpClient httpClient)
        {
            client.Timeout = TimeSpan.FromSeconds(120);
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PostAsync("api/BloodBanks/Login", stringContent);

            response.EnsureSuccessStatusCode();

            string isSuccessful = await response.Content.ReadAsStringAsync();
            integrationResponse = Boolean.Parse(isSuccessful);
            return integrationResponse;
        }

        private static async Task SendGetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/BloodRequest");
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            requests = JsonConvert.DeserializeObject<List<BloodRequestDTO>>(jsonContent);
        }
    }
}
