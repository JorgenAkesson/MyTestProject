namespace CompanyApi.Services
{
    public interface IMessageService
    {
        void SendMessageDirectWithRabbitMQ(string messageType, string productName);
    }
}
