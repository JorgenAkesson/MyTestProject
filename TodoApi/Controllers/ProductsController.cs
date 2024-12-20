using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            return await _context.Products
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(long id)
        {
            var todoItem = await _context.Products.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductDTO todoDTO)
        {
            if (id != todoDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.Products.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO todoDTO)
        {
            var todoItem = new Product
            {
                Name = todoDTO.Name
            };

            _context.Products.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = todoItem.Id  },
                ItemToDTO(todoItem));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var todoItem = await _context.Products.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private static ProductDTO ItemToDTO(Product todoItem) =>
           new ProductDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
           };
    }
}
