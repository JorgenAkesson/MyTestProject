using PatientApi.Models;
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

        public void AddPatient(PatientDTO patient)
        {
            var jsonpatient = JsonContent.Create(patient);
            var httpClient = _httpClientFactory.CreateClient("Company");
            httpClient.PostAsync("/api/Patients", jsonpatient);
        }

        public async Task<List<PatientDTO>> GetPatients()
        {
            var httpClient = _httpClientFactory.CreateClient("Company");
            var httpResponseMessage = await httpClient.GetAsync("/api/Patients");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var patients = JsonSerializer.Deserialize<List<PatientDTO>>(contentStream, options);

                var a = patients.ToList();
                return a;
            }
            return null;
        }

    }
}
