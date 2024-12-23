using MassTransit;
using PatientApi.Models;
using RabbitMQ.Client;
using SharedModels;
using System.Text;

namespace PatientApi.Services
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

        public async void SendMessageWithMassTransit(PatientDTO dto)
        {
            var patientAppointment = new BillingDTO()
            {
                BillingName = dto.PatientName,
                Quantity = 1,
                Price = 2
            };
            await _publishEndpoint.Publish<BillingCreated>(new
            {
                Id = 1,
                patientAppointment.BillingName,
                patientAppointment.Quantity,
                patientAppointment.Price
            });
        }

        public async void SendMessageWithRabbitMQ(string messageType, string text)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(text);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        }
    }
}
