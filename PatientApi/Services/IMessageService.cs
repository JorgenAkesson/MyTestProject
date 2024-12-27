using PatientApi.Models;

namespace PatientApi.Services
{
    public interface IMessageService
    {
        void SendBillingCreatedMessageWithMassTransit(PatientDTO dto);
        void SendMessageWithRabbitMQ(string messageType, string text);
    }
}
