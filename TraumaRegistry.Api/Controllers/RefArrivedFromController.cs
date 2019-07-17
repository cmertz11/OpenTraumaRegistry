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
    public class RefArrivedFromController : ControllerBase
    {
        private readonly Context _context;

        public RefArrivedFromController(Context context)
        {
            _context = context;
        }

        // GET: api/RefArrivedFrom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefArrivedFrom>>> GetRefArrivedFrom()
        {
            return await _context.RefArrivedFrom.ToListAsync();
        }

        // GET: api/RefArrivedFrom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefArrivedFrom>> GetRefArrivedFrom(int id)
        {
            var refArrivedFrom = await _context.RefArrivedFrom.FindAsync(id);

            if (refArrivedFrom == null)
            {
                return NotFound();
            }

            return refArrivedFrom;
        }

        // PUT: api/RefArrivedFrom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefArrivedFrom(int id, RefArrivedFrom refArrivedFrom)
        {
            if (id != refArrivedFrom.Id)
            {
                return BadRequest();
            }

            _context.Entry(refArrivedFrom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefArrivedFromExists(id))
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

        // POST: api/RefArrivedFrom
        [HttpPost]
        public async Task<ActionResult<RefArrivedFrom>> PostRefArrivedFrom(RefArrivedFrom refArrivedFrom)
        {
            _context.RefArrivedFrom.Add(refArrivedFrom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefArrivedFrom", new { id = refArrivedFrom.Id }, refArrivedFrom);
        }

        // DELETE: api/RefArrivedFrom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefArrivedFrom>> DeleteRefArrivedFrom(int id)
        {
            var refArrivedFrom = await _context.RefArrivedFrom.FindAsync(id);
            if (refArrivedFrom == null)
            {
                return NotFound();
            }

            _context.RefArrivedFrom.Remove(refArrivedFrom);
            await _context.SaveChangesAsync();

            return refArrivedFrom;
        }

        private bool RefArrivedFromExists(int id)
        {
            return _context.RefArrivedFrom.Any(e => e.Id == id);
        }
    }
}
