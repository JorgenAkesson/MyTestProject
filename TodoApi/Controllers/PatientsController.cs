using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using PatientApi.Models;
using PatientApi.Services;

namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMessageService _messageService;

        public PatientsController(DBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService=messageService;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients()
        {
            var a = await _context
                .Patients
                .Include(a => a.Appointments)
                .Select(x => PatientDTO.PatientToDTO(x, false))
                .ToListAsync();
            return Ok(a);
        }

        // GET: api/Patients/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return PatientDTO.PatientToDTO(patient);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutPatient(int id, PatientDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var patient = PatientDTO.DTOToPatient(dto);

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> PostPatient(PatientDTO dto)
        {
            var patient = PatientDTO.DTOToPatient(dto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            if (dto.Appointments.Any())
            {
                //_messageService.SendMessageWithRabbitMQ("NewBilling", "BillingName");
                _messageService.SendMessageWithMassTransit(dto);
            }

            return CreatedAtAction("GetPatient", new { id = patient.Id }, dto);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
