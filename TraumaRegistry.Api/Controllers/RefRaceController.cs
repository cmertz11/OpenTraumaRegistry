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
    public class RefRaceController : ControllerBase
    {
        private readonly Context _context;

        public RefRaceController(Context context)
        {
            _context = context;
        }

        // GET: api/RefRace
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefRace>>> GetRefRace()
        {
            return await _context.RefRace.ToListAsync();
        }

        // GET: api/RefRace/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefRace>> GetRefRace(int id)
        {
            var refRace = await _context.RefRace.FindAsync(id);

            if (refRace == null)
            {
                return NotFound();
            }

            return refRace;
        }

        // PUT: api/RefRace/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefRace(int id, RefRace refRace)
        {
            if (id != refRace.Id)
            {
                return BadRequest();
            }

            _context.Entry(refRace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefRaceExists(id))
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

        // POST: api/RefRace
        [HttpPost]
        public async Task<ActionResult<RefRace>> PostRefRace(RefRace refRace)
        {
            _context.RefRace.Add(refRace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefRace", new { id = refRace.Id }, refRace);
        }

        // DELETE: api/RefRace/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefRace>> DeleteRefRace(int id)
        {
            var refRace = await _context.RefRace.FindAsync(id);
            if (refRace == null)
            {
                return NotFound();
            }

            _context.RefRace.Remove(refRace);
            await _context.SaveChangesAsync();

            return refRace;
        }

        private bool RefRaceExists(int id)
        {
            return _context.RefRace.Any(e => e.Id == id);
        }
    }
}
