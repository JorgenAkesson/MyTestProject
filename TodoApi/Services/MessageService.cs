using RabbitMQ.Client;
using System.Text;

namespace CompanyApi.Services
{
    public class MessageService : IMessageService
    {
        // https://www.youtube.com/watch?v=KhYiaEOrw7Q
        // 
        public MessageService()
        {
            
        }

        public async void SendMessageDirectWithRabbitMQ(string messageType, string productName)
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
