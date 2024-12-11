using Microsoft.AspNetCore.Mvc;
using MyTestProject.Models;

namespace MyTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                ID = index,
                Name = "Kalle"
            })
            .ToArray();
        }

        [HttpPut]
        public void Put(Product myClass)
        {
        }

        [HttpPost]
        public void Post(Product myClass)
        {
        }

        [HttpDelete]
        public void Delete(int index)
        {

        }
    }
}
