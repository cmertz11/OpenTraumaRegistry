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
    public class RefSafetyDevicesController : ControllerBase
    {
        private readonly Context _context;

        public RefSafetyDevicesController(Context context)
        {
            _context = context;
        }

        // GET: api/RefSafetyDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefSafetyDevices>>> GetRefSafetyDevices()
        {
            return await _context.RefSafetyDevices.ToListAsync();
        }

        // GET: api/RefSafetyDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefSafetyDevices>> GetRefSafetyDevices(int id)
        {
            var refSafetyDevices = await _context.RefSafetyDevices.FindAsync(id);

            if (refSafetyDevices == null)
            {
                return NotFound();
            }

            return refSafetyDevices;
        }

        // PUT: api/RefSafetyDevices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefSafetyDevices(int id, RefSafetyDevices refSafetyDevices)
        {
            if (id != refSafetyDevices.Id)
            {
                return BadRequest();
            }

            _context.Entry(refSafetyDevices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefSafetyDevicesExists(id))
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

        // POST: api/RefSafetyDevices
        [HttpPost]
        public async Task<ActionResult<RefSafetyDevices>> PostRefSafetyDevices(RefSafetyDevices refSafetyDevices)
        {
            _context.RefSafetyDevices.Add(refSafetyDevices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefSafetyDevices", new { id = refSafetyDevices.Id }, refSafetyDevices);
        }

        // DELETE: api/RefSafetyDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefSafetyDevices>> DeleteRefSafetyDevices(int id)
        {
            var refSafetyDevices = await _context.RefSafetyDevices.FindAsync(id);
            if (refSafetyDevices == null)
            {
                return NotFound();
            }

            _context.RefSafetyDevices.Remove(refSafetyDevices);
            await _context.SaveChangesAsync();

            return refSafetyDevices;
        }

        private bool RefSafetyDevicesExists(int id)
        {
            return _context.RefSafetyDevices.Any(e => e.Id == id);
        }
    }
}
