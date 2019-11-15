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
    public class SafetyDevicesController : ControllerBase
    {
        private readonly Context _context;

        public SafetyDevicesController(Context context)
        {
            _context = context;
        }

        // GET: api/SafetyDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SafetyDevices>>> GetSafetyDevices()
        {
            return await _context.SafetyDevices.ToListAsync();
        }

        // GET: api/SafetyDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SafetyDevices>> GetSafetyDevices(int id)
        {
            var safetyDevices = await _context.SafetyDevices.FindAsync(id);

            if (safetyDevices == null)
            {
                return NotFound();
            }

            return safetyDevices;
        }

        // PUT: api/SafetyDevices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSafetyDevices(int id, SafetyDevices safetyDevices)
        {
            if (id != safetyDevices.Id)
            {
                return BadRequest();
            }

            _context.Entry(safetyDevices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafetyDevicesExists(id))
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

        // POST: api/SafetyDevices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SafetyDevices>> PostSafetyDevices(SafetyDevices safetyDevices)
        {
            _context.SafetyDevices.Add(safetyDevices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSafetyDevices", new { id = safetyDevices.Id }, safetyDevices);
        }

        // DELETE: api/SafetyDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SafetyDevices>> DeleteSafetyDevices(int id)
        {
            var safetyDevices = await _context.SafetyDevices.FindAsync(id);
            if (safetyDevices == null)
            {
                return NotFound();
            }

            _context.SafetyDevices.Remove(safetyDevices);
            await _context.SaveChangesAsync();

            return safetyDevices;
        }

        private bool SafetyDevicesExists(int id)
        {
            return _context.SafetyDevices.Any(e => e.Id == id);
        }
    }
}
