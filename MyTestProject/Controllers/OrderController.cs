using Microsoft.AspNetCore.Mvc;
using MyTestProject.Models;

namespace MyTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Order
            {
                ID = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            })
            .ToArray();
        }

        [HttpPut]
        public void Put(Order myClass)
        {
        }

        [HttpPost]
        public void Post(Order myClass)
        {
        }

        [HttpDelete]
        public void Delete(int index)
        {

        }
    }
}
