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
    public class RefLocationController : ControllerBase
    {
        private readonly Context _context;

        public RefLocationController(Context context)
        {
            _context = context;
        }

        // GET: api/RefLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefLocation>>> GetRefLocation()
        {
            return await _context.RefLocation.ToListAsync();
        }

        // GET: api/RefLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefLocation>> GetRefLocation(int id)
        {
            var refLocation = await _context.RefLocation.FindAsync(id);

            if (refLocation == null)
            {
                return NotFound();
            }

            return refLocation;
        }

        // PUT: api/RefLocation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefLocation(int id, RefLocation refLocation)
        {
            if (id != refLocation.Id)
            {
                return BadRequest();
            }

            _context.Entry(refLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefLocationExists(id))
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

        // POST: api/RefLocation
        [HttpPost]
        public async Task<ActionResult<RefLocation>> PostRefLocation(RefLocation refLocation)
        {
            _context.RefLocation.Add(refLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefLocation", new { id = refLocation.Id }, refLocation);
        }

        // DELETE: api/RefLocation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefLocation>> DeleteRefLocation(int id)
        {
            var refLocation = await _context.RefLocation.FindAsync(id);
            if (refLocation == null)
            {
                return NotFound();
            }

            _context.RefLocation.Remove(refLocation);
            await _context.SaveChangesAsync();

            return refLocation;
        }

        private bool RefLocationExists(int id)
        {
            return _context.RefLocation.Any(e => e.Id == id);
        }
    }
}
