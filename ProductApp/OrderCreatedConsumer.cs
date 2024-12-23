using MassTransit;
using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp
{
    class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($" [x] Received. OrderCreated message: {jsonMessage}");
        }
    }
}
