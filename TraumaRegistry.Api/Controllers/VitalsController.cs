using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTraumaRegistry.Data;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsController : ControllerBase
    {
        private readonly Context _context;
        public VitalsController(Context context)
        {
            _context = context;
        }

        // GET: api/Vitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vitals>>> GetVitals()
        {
            return await _context.Vitals.ToListAsync();
        }

        // GET: api/Vitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vitals>> GetVitals(int id)
        {
            var vitals = await _context.Vitals.FindAsync(id);

            if (vitals == null)
            {
                return NotFound();
            }

            return vitals;
        }

        // PUT: api/Vitals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVitals(int id, Vitals vitals)
        {
            if (id != vitals.Id)
            {
                return BadRequest();
            }
            vitals.TimeStamp = DateTime.Now;
            _context.Entry(vitals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VitalsExists(id))
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

        // POST: api/Vitals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Vitals>> PostVitals(Vitals vitals)
        {
            vitals.TimeStamp = DateTime.Now;
            _context.Vitals.Add(vitals);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetVitals", new { id = vitals.Id }, vitals);

            return Ok(vitals);
        }

        // DELETE: api/Vitals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vitals>> DeleteVitals(int id)
        {
            var vitals = await _context.Vitals.FindAsync(id);
            if (vitals == null)
            {
                return NotFound();
            }

            _context.Vitals.Remove(vitals);
            await _context.SaveChangesAsync();

            return vitals;
        }

        private bool VitalsExists(int id)
        {
            return _context.Vitals.Any(e => e.Id == id);
        }
    }
}
