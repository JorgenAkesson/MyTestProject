using CompanyApi.Models;

namespace CompanyApi.Services
{
    public interface IMessageService
    {
        void SendMessageWithMassTransit(AccountDTO dto);
        void SendMessageWithRabbitMQ(string messageType, string productName);
    }
}
