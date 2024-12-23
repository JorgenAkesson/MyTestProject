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

        public void AddAccount(AccountDTO account)
        {
            var jsonAccount = JsonContent.Create(account);
            var httpClient = _httpClientFactory.CreateClient("Company");
            httpClient.PostAsync("/api/Accounts", jsonAccount);
        }

        public async Task<List<AccountDTO>> GetAccounts()
        {
            var httpClient = _httpClientFactory.CreateClient("Company");
            var httpResponseMessage = await httpClient.GetAsync("/api/Accounts");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var accounts = JsonSerializer.Deserialize<List<AccountDTO>>(contentStream, options);

                var a = accounts.ToList();
                return a;
            }
            return null;
        }

    }
}
