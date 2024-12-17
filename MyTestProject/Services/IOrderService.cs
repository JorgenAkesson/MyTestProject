using MyTestProject.Models;

namespace MyTestProject.Services
{
    public interface IOrderService
    {
        void PlaceOrder(Order order); 
        void GetOrder(int id); 
    }
}
