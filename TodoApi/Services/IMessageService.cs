using PatientApi.Models;

namespace PatientApi.Services
{
    public interface IMessageService
    {
        void SendMessageWithMassTransit(PatientDTO dto);
        void SendMessageWithRabbitMQ(string messageType, string text);
    }
}
