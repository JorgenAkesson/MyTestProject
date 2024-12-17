using Microsoft.AspNetCore.Mvc;
using MyTestProject.Models;

namespace MyTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public AccountController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Account
            {
                ID = index,
                Name = index.ToString(),
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
