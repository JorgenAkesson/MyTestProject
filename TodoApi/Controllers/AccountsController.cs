using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DBContext _context;

        public AccountsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<List<AccountDTO>>> GetAccounts()
        {
            return await _context.Accounts
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(long id)
        {
            var todoItem = await _context.Accounts.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(long id, AccountDTO todoDTO)
        {
            if (id != todoDTO.id)
            {
                return BadRequest();
            }

            var todoItem = await _context.Accounts.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoDTO.name;
            todoItem.IsComplete = todoDTO.isComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AccountExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO todoDTO)
        {
            var todoItem = new Account
            {
                IsComplete = todoDTO.isComplete,
                Name = todoDTO.name
            };

            _context.Accounts.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAccount),
                new { id = todoItem.Id  },
                ItemToDTO(todoItem));
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(long id)
        {
            var todoItem = await _context.Accounts.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }

        private static AccountDTO ItemToDTO(Account todoItem) =>
           new AccountDTO
           {
               id = todoItem.Id,
               name = todoItem.Name,
               isComplete = todoItem.IsComplete
           };
    }
}
