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
    public class AccountsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMessageService _messageService;

        public AccountsController(DBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService=messageService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            var a =  await _context
                .Accounts
                .Include(a => a.Orders)
                .Select(x => AccountDTO.AccountToDTO(x, false))
                .ToListAsync();
            return Ok(a);
        }

        // GET: api/Accounts/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return AccountDTO.AccountToDTO(account);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAccount(int id, AccountDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var account = AccountDTO.DTOToAccount(dto);

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO dto)
        {
            var account = AccountDTO.DTOToAccount(dto);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            if(dto.Orders.Any())
            {
                _messageService.SendMessageDirectWithRabbitMQ("NewProduct", "ProductName");
            }

            return CreatedAtAction("GetAccount", new { id = account.Id }, dto);
        }

        // DELETE: api/Accounts1/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
