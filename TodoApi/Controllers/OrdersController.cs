using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Models;
using CompanyApi.Services;

namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMessageService _messageService;

        public OrdersController(DBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService=messageService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            return await _context
                .Orders
                .Include(a => a.Account)
                .Select(a => OrderDTO.OrderToDTO(a, false))
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return OrderDTO.OrderToDTO(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var order = OrderDTO.DTOToOrder(dto);
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO dto)
        {
            var order = OrderDTO.DTOToOrder(dto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _messageService.SendMessageDirectWithRabbitMQ("NewProduct", "ProductName");

            return CreatedAtAction("GetOrder", new { id = order.Id }, dto);
        }

        // DELETE: api/Orders1/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
