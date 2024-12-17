using CompanyApi.Models;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BlazorServerApp.Services
{
    public class MyService : IMyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }

        public void AddAccount( AccountDTO account)
        {
            var jsonAccount = JsonContent.Create(account);
            var httpClient = _httpClientFactory.CreateClient("Company");
            httpClient.PostAsync("/api/Accounts", jsonAccount);
        }

        public async Task<List<AccountDTO>> GetAccounts()
        {
            var httpClient = _httpClientFactory.CreateClient("Company");
            var httpResponseMessage = await httpClient.GetAsync(
                "/api/Accounts");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                //contentStream.Position = 0;
                //using (StreamReader reader = new StreamReader(contentStream, Encoding.UTF8))
                //{
                //    var a =  reader.ReadToEnd();
                //}

                var accounts = await JsonSerializer.DeserializeAsync
                    <List<AccountDTO>>(contentStream);

                return accounts.ToList();
            }
            return null;
        }
    }
}
