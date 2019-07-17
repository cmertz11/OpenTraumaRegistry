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
    public class RefInjuryTypeController : ControllerBase
    {
        private readonly Context _context;

        public RefInjuryTypeController(Context context)
        {
            _context = context;
        }

        // GET: api/RefInjuryType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefInjuryType>>> GetRefInjuryType()
        {
            return await _context.RefInjuryType.ToListAsync();
        }

        // GET: api/RefInjuryType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefInjuryType>> GetRefInjuryType(int id)
        {
            var refInjuryType = await _context.RefInjuryType.FindAsync(id);

            if (refInjuryType == null)
            {
                return NotFound();
            }

            return refInjuryType;
        }

        // PUT: api/RefInjuryType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefInjuryType(int id, RefInjuryType refInjuryType)
        {
            if (id != refInjuryType.Id)
            {
                return BadRequest();
            }

            _context.Entry(refInjuryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefInjuryTypeExists(id))
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

        // POST: api/RefInjuryType
        [HttpPost]
        public async Task<ActionResult<RefInjuryType>> PostRefInjuryType(RefInjuryType refInjuryType)
        {
            _context.RefInjuryType.Add(refInjuryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefInjuryType", new { id = refInjuryType.Id }, refInjuryType);
        }

        // DELETE: api/RefInjuryType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefInjuryType>> DeleteRefInjuryType(int id)
        {
            var refInjuryType = await _context.RefInjuryType.FindAsync(id);
            if (refInjuryType == null)
            {
                return NotFound();
            }

            _context.RefInjuryType.Remove(refInjuryType);
            await _context.SaveChangesAsync();

            return refInjuryType;
        }

        private bool RefInjuryTypeExists(int id)
        {
            return _context.RefInjuryType.Any(e => e.Id == id);
        }
    }
}
