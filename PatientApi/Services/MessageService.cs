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
        private readonly IRequestClient<BillingCreatedRequest> _getBillingCretedClient;

        public MessageService(IPublishEndpoint publishEndpoint, IRequestClient<BillingCreatedRequest> getBillingCretedRequestClient)
        {
            _publishEndpoint=publishEndpoint;
            _getBillingCretedClient=getBillingCretedRequestClient;
        }

        public async void SendBillingCreatedMessageWithMassTransit(PatientDTO dto)
        {
            //var patientAppointment = new BillingDTO()
            //{
            //    BillingName = dto.PatientName,
            //    Quantity = 1,
            //    Price = 2
            //};
            //await _publishEndpoint.Publish<BillingCreatedRequest>(new
            //{
            //    Id = 1,
            //    patientAppointment.BillingName,
            //    patientAppointment.Quantity,
            //    patientAppointment.Price
            //});

            try
            {
                Response<BillingCreatedResponse> resp = await _getBillingCretedClient.GetResponse<BillingCreatedResponse>(new BillingCreatedRequest()
                {
                    PatientName = dto.PatientName,
                    Price = 100,
                    Quantity = 1
                });

            }
            catch (Exception)
            {
            }
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
