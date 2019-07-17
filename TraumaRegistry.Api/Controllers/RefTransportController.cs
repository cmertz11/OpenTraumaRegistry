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
    public class RefTransportController : ControllerBase
    {
        private readonly Context _context;

        public RefTransportController(Context context)
        {
            _context = context;
        }

        // GET: api/RefTransport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefTransport>>> GetRefTransport()
        {
            return await _context.RefTransport.ToListAsync();
        }

        // GET: api/RefTransport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefTransport>> GetRefTransport(int id)
        {
            var refTransport = await _context.RefTransport.FindAsync(id);

            if (refTransport == null)
            {
                return NotFound();
            }

            return refTransport;
        }

        // PUT: api/RefTransport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefTransport(int id, RefTransport refTransport)
        {
            if (id != refTransport.Id)
            {
                return BadRequest();
            }

            _context.Entry(refTransport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefTransportExists(id))
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

        // POST: api/RefTransport
        [HttpPost]
        public async Task<ActionResult<RefTransport>> PostRefTransport(RefTransport refTransport)
        {
            _context.RefTransport.Add(refTransport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefTransport", new { id = refTransport.Id }, refTransport);
        }

        // DELETE: api/RefTransport/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefTransport>> DeleteRefTransport(int id)
        {
            var refTransport = await _context.RefTransport.FindAsync(id);
            if (refTransport == null)
            {
                return NotFound();
            }

            _context.RefTransport.Remove(refTransport);
            await _context.SaveChangesAsync();

            return refTransport;
        }

        private bool RefTransportExists(int id)
        {
            return _context.RefTransport.Any(e => e.Id == id);
        }
    }
}
