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
    public class RefTraumaLevelController : ControllerBase
    {
        private readonly Context _context;

        public RefTraumaLevelController(Context context)
        {
            _context = context;
        }

        // GET: api/RefTraumaLevel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefTraumaLevel>>> GetRefTraumaLevel()
        {
            return await _context.RefTraumaLevel.ToListAsync();
        }

        // GET: api/RefTraumaLevel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefTraumaLevel>> GetRefTraumaLevel(int id)
        {
            var refTraumaLevel = await _context.RefTraumaLevel.FindAsync(id);

            if (refTraumaLevel == null)
            {
                return NotFound();
            }

            return refTraumaLevel;
        }

        // PUT: api/RefTraumaLevel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefTraumaLevel(int id, RefTraumaLevel refTraumaLevel)
        {
            if (id != refTraumaLevel.Id)
            {
                return BadRequest();
            }

            _context.Entry(refTraumaLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefTraumaLevelExists(id))
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

        // POST: api/RefTraumaLevel
        [HttpPost]
        public async Task<ActionResult<RefTraumaLevel>> PostRefTraumaLevel(RefTraumaLevel refTraumaLevel)
        {
            _context.RefTraumaLevel.Add(refTraumaLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefTraumaLevel", new { id = refTraumaLevel.Id }, refTraumaLevel);
        }

        // DELETE: api/RefTraumaLevel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefTraumaLevel>> DeleteRefTraumaLevel(int id)
        {
            var refTraumaLevel = await _context.RefTraumaLevel.FindAsync(id);
            if (refTraumaLevel == null)
            {
                return NotFound();
            }

            _context.RefTraumaLevel.Remove(refTraumaLevel);
            await _context.SaveChangesAsync();

            return refTraumaLevel;
        }

        private bool RefTraumaLevelExists(int id)
        {
            return _context.RefTraumaLevel.Any(e => e.Id == id);
        }
    }
}
