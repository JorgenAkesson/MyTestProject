using BillingAPI.Models;
using MassTransit;
using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BillingApp
{
    class BillingCreatedConsumer : IConsumer<BillingCreatedRequest>
    {
        public async Task Consume(ConsumeContext<BillingCreatedRequest> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($" [x] Received. BillingCreatedRequest message: {jsonMessage}");

            var billing = new Billing() { BillingName = context.Message.PatientName, Price = context.Message.Price, Quantity = context.Message.Quantity };
            var sc = JsonContent.Create(billing);

            // Call Billing API
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7252");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync("api/Billings", sc);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadAsStringAsync();
                    await context.RespondAsync(new BillingCreatedResponse() { StatusMessage = "Billing successfully created." });
                }
            }
        }
    }
}
