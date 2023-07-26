using System;
using System.Threading.Tasks;
using System.Net.Http;
using IntegrationLibrary.Core.Model;

namespace IntegrationLibrary.Core.BloodBankConnection
{
    public class BloodBankHTTPConnection : IBloodBankConnection
    {
        private static HttpClient client;
        private static string bloodType;
        private static int quantity;
        private static String bankEmail;
        private static String bankAPI;
        private static byte[] pdfFile;

        public bool SendBloodRequest(BloodBank bank, string bType, int quant)
        {
            client = new()
            {
                BaseAddress = new Uri(bank.ServerAddress)
            };  
            bloodType = bType;
            quantity = quant;
            bankEmail = bank.Email.EmailAddress;
            bankAPI = bank.ApiKey;

            return GetAsync(client).Result;
        }

        static async Task<bool> GetAsync(HttpClient httpClient)
        {
            client.Timeout = TimeSpan.FromSeconds(15);
            client.DefaultRequestHeaders.Add("Authorization","Bearer " + bankAPI);
            using HttpResponseMessage response = await httpClient.GetAsync("api/bloodbank/" + bankEmail + "/"  + bloodType + "/" + quantity);
            
            response.EnsureSuccessStatusCode();
            
            string hasBlood = await response.Content.ReadAsStringAsync();
            return Boolean.Parse(hasBlood);
        }

        public async Task<bool> SendBloodReports(BloodBank bank, byte[] pdf)
        {
            client = new()
            {
                BaseAddress = new Uri(bank.ServerAddress)
            };
            pdfFile = pdf;
            bankEmail = bank.Email.EmailAddress;
            bankAPI = bank.ApiKey;

            return PostAsync(client).Result;
        }

        static async Task<bool> PostAsync(HttpClient httpClient)
        {
            string isSuccessful = "false";
            client.Timeout = TimeSpan.FromSeconds(120);
            ByteArrayContent byteContent = new ByteArrayContent(pdfFile);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bankAPI);
            using HttpResponseMessage response = await httpClient.PostAsync("api/bloodbank/" + bankEmail, byteContent);

            response.EnsureSuccessStatusCode();

            isSuccessful = await response.Content.ReadAsStringAsync();
            return Boolean.Parse(isSuccessful);

        }

        public async Task<int> GetBlood(BloodBank bank, string bType, int quant)
        {
            client = new()
            {
                BaseAddress = new Uri(bank.ServerAddress)
            };

            return GetAsync(client, bank, bType, quant).Result;
        }

        public async Task<int> GetEmergencyBlood(BloodBank bank, string bType, int quant)
        {
            client = new()
            {
                BaseAddress = new Uri(bank.ServerAddress)
            };
            return GetAsyncEmergency(client, bank, bType, quant).Result;
        }

        static async Task<int> GetAsync(HttpClient httpClient, BloodBank bank, string bType, int quant)
        {
            int blood = -1;
            client.Timeout = TimeSpan.FromSeconds(15);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bank.ApiKey);
            using HttpResponseMessage response = await httpClient.GetAsync("api/bloodbank/get/" + bank.Email.EmailAddress + "/" + bType + "/" + quant);

            response.EnsureSuccessStatusCode();

            string hasBlood = await response.Content.ReadAsStringAsync();
            blood = Int32.Parse(hasBlood);
            return blood;
        }

        static async Task<int> GetAsyncEmergency(HttpClient httpClient, BloodBank bank, string bType, int quant)
        {
            int blood = -1;
            using HttpResponseMessage response = await httpClient.GetAsync("api/bloodbank/getEmergency/" + bType + "/" + quant);

            response.EnsureSuccessStatusCode();

            string hasBlood = await response.Content.ReadAsStringAsync();
            blood = Int32.Parse(hasBlood);
            return blood;
        }
    }
}
