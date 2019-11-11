using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraumaRegistry.Data;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly Context _context;

        public EventsController(Context context)
        {
            _context = context;
        }

        // GET: api/Patients/5
        [HttpGet("{PatientId}")]
        public async Task<ActionResult<Patient>> GetPatientWithEvents(int PatientId)
        {
            var patient = await _context.Patients.Where(p => p.Id == PatientId)
                .Include(events => events.Events)
                .ThenInclude(events => events.Outcome)
                .Include(events => events.Events)
                .ThenInclude(events => events.TraumaLevel)
                .Include(events => events.Events)
                .ThenInclude(events => events.InjuryTypes)
                .Include(events => events.Events)
                .ThenInclude(events => events.SafetyDevices)
                .Include(events => events.Events)
                .ThenInclude(events => events.Vitals)
                .Include(events => events.Events)
                .ThenInclude(events => events.Injuries)
                .Include(events => events.Events)
                .ThenInclude(events => events.Procedures)
                .Include(events => events.Events)
                .ThenInclude(events => events.Complications)
                .Include(events => events.Events)
                .ThenInclude(events => events.Risks)
                .Include(events => events.Events)
                .ThenInclude(events => events.Consults)

                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }


        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
