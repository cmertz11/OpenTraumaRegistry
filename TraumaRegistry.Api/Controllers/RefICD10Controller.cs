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
    public class RefICD10Controller : ControllerBase
    {
        private readonly Context _context;

        public RefICD10Controller(Context context)
        {
            _context = context;
        }

        // GET: api/RefICD10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefICD10>>> GetRefICD10Codes()
        {
            return await _context.RefICD10Codes.ToListAsync();
        }

        // GET: api/RefICD10/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefICD10>> GetRefICD10(int id)
        {
            var refICD10 = await _context.RefICD10Codes.FindAsync(id);

            if (refICD10 == null)
            {
                return NotFound();
            }

            return refICD10;
        }

        // PUT: api/RefICD10/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefICD10(int id, RefICD10 refICD10)
        {
            if (id != refICD10.Id)
            {
                return BadRequest();
            }

            _context.Entry(refICD10).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefICD10Exists(id))
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

        // POST: api/RefICD10
        [HttpPost]
        public async Task<ActionResult<RefICD10>> PostRefICD10(RefICD10 refICD10)
        {
            _context.RefICD10Codes.Add(refICD10);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefICD10", new { id = refICD10.Id }, refICD10);
        }

        // DELETE: api/RefICD10/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefICD10>> DeleteRefICD10(int id)
        {
            var refICD10 = await _context.RefICD10Codes.FindAsync(id);
            if (refICD10 == null)
            {
                return NotFound();
            }

            _context.RefICD10Codes.Remove(refICD10);
            await _context.SaveChangesAsync();

            return refICD10;
        }

        private bool RefICD10Exists(int id)
        {
            return _context.RefICD10Codes.Any(e => e.Id == id);
        }
    }
}
