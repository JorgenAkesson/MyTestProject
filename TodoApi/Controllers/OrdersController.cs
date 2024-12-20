//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CompanyApi.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace TodoApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly DBContext _context;

//        public OrdersController(DBContext context)
//        {
//            _context = context;
//            Console.WriteLine(context.Model.ToDebugString());
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
//        {
//            return await _context.Orders
//                .Include(r => r.Account)
//                //.Include(r => r.Products)
//                .Select(x => OrderDTO.OrderToDTO(x))
//                .ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<OrderDTO>> GetOrders(long id)
//        {
//            var todoItem = await _context.Orders.FindAsync(id);

//            if (todoItem == null)
//            {
//                return NotFound();
//            }

//            return OrderDTO.OrderToDTO(todoItem);
//        }

//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutOrder(long id, OrderDTO todoDTO)
//        {
//            if (id != todoDTO.id)
//            {
//                return BadRequest();
//            }

//            var todoItem = await _context.Orders.FindAsync(id);
//            if (todoItem == null)
//            {
//                return NotFound();
//            }

//            todoItem.Name = todoDTO.name;


//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException) when (!OrderExists(id))
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }

//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO dto)
//        {
//            var orderItem = OrderDTO.DTOToOrder(dto);

//            _context.Orders.Add(orderItem);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(
//                nameof(GetOrders),
//                new { id = orderItem.Id },
//                OrderDTO.OrderToDTO(orderItem));
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrder(long id)
//        {
//            var todoItem = await _context.Orders.FindAsync(id);
//            if (todoItem == null)
//            {
//                return NotFound();
//            }

//            _context.Orders.Remove(todoItem);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OrderExists(long id)
//        {
//            return _context.Orders.Any(e => e.Id == id);
//        }
//    }
//}
