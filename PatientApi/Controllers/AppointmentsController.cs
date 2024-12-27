using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientApi.Models;
using PatientApi.Services;

namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMessageService _messageService;

        public AppointmentsController(DBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService=messageService;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            return await _context
                .Appointments
                .Include(a => a.Patient)
                .Select(a => AppointmentDTO.AppointmentToDTO(a, false))
                .ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return AppointmentDTO.AppointmentToDTO(appointment);
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAppointment(int id, AppointmentDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var appointment = AppointmentDTO.DTOToAppointment(dto);
            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> PostAppointment(AppointmentDTO dto)
        {
            var appointment = AppointmentDTO.DTOToAppointment(dto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            _messageService.SendMessageWithRabbitMQ("NewBilling", "PatientName");

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, dto);
        }

        // DELETE: api/Appointments1/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
