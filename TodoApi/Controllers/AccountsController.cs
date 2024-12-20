using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Controllers
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

        [HttpGet]
        public async Task<ActionResult<List<AccountDTO>>> GetAccounts()
        {
            return await _context.Accounts
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

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

        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostAccount(AccountDTO dto)
        {
            var accountItem = AccountDTO.DTOToAccount(dto);

            _context.Accounts.Add(accountItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAccount),
                new { id = accountItem.Id },
                ItemToDTO(accountItem));
        }

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

        private static AccountDTO ItemToDTO(Account account) =>
           new AccountDTO
           {
               id = account.Id,
               name = account.Name,
               isComplete = account.IsComplete,
               orders = account.Orders?.Select(a => OrderDTO.OrderToDTO(a)).ToList(),
           };
    }
}
