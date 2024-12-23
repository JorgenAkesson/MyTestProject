using CompanyApi.Models;
using MassTransit;
using RabbitMQ.Client;
using SharedModels;
using System.Text;

namespace CompanyApi.Services
{
    public class MessageService : IMessageService
    {
        // https://code-maze.com/masstransit-rabbitmq-aspnetcore/
        // https://www.youtube.com/watch?v=KhYiaEOrw7Q

        private readonly IPublishEndpoint _publishEndpoint;

        public MessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint=publishEndpoint;
        }

        public async void SendMessageWithMassTransit(AccountDTO dto)
        {
            var orderDto = new OrderDto()
            {
                ProductName = dto.AccountName,
                Quantity = 1,
                Price = 2
            };
            await _publishEndpoint.Publish<OrderCreated>(new
            {
                Id = 1,
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            });
        }

        public async void SendMessageWithRabbitMQ(string messageType, string productName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(productName);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        }
    }
}
