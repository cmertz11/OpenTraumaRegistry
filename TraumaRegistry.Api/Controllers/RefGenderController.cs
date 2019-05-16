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
    public class RefGenderController : ControllerBase
    {
        private readonly Context _context;

        public RefGenderController(Context context)
        {
            _context = context;
        }

        // GET: api/RefGender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefGender>>> GetRefGender()
        {
            return await _context.RefGender.ToListAsync();
        }

        // GET: api/RefGender/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefGender>> GetRefGender(int id)
        {
            var refGender = await _context.RefGender.FindAsync(id);

            if (refGender == null)
            {
                return NotFound();
            }

            return refGender;
        }

        // PUT: api/RefGender/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefGender(int id, RefGender refGender)
        {
            if (id != refGender.Id)
            {
                return BadRequest();
            }

            _context.Entry(refGender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefGenderExists(id))
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

        // POST: api/RefGender
        [HttpPost]
        public async Task<ActionResult<RefGender>> PostRefGender(RefGender refGender)
        {
            _context.RefGender.Add(refGender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefGender", new { id = refGender.Id }, refGender);
        }

        // DELETE: api/RefGender/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefGender>> DeleteRefGender(int id)
        {
            var refGender = await _context.RefGender.FindAsync(id);
            if (refGender == null)
            {
                return NotFound();
            }

            _context.RefGender.Remove(refGender);
            await _context.SaveChangesAsync();

            return refGender;
        }

        private bool RefGenderExists(int id)
        {
            return _context.RefGender.Any(e => e.Id == id);
        }
    }
}
