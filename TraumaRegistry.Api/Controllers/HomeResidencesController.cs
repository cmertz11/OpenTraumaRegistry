using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeResidencesController : ControllerBase
    {
        private readonly Context _context;

        public HomeResidencesController(Context context)
        {
            _context = context;
        }

        // GET: api/HomeResidences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeResidence>>> GetHomeResidences()
        {
            return await _context.HomeResidences.ToListAsync();
        }

        // GET: api/HomeResidences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeResidence>> GetHomeResidence(int id)
        {
            var homeResidence = await _context.HomeResidences.FindAsync(id);

            if (homeResidence == null)
            {
                return NotFound();
            }

            return homeResidence;
        }

        // PUT: api/HomeResidences/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeResidence(int id, HomeResidence homeResidence)
        {
            if (id != homeResidence.Id)
            {
                return BadRequest();
            }

            _context.Entry(homeResidence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeResidenceExists(id))
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

        // POST: api/HomeResidences
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HomeResidence>> PostHomeResidence(HomeResidence homeResidence)
        {
            _context.HomeResidences.Add(homeResidence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomeResidence", new { id = homeResidence.Id }, homeResidence);
        }

        // DELETE: api/HomeResidences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeResidence>> DeleteHomeResidence(int id)
        {
            var homeResidence = await _context.HomeResidences.FindAsync(id);
            if (homeResidence == null)
            {
                return NotFound();
            }

            _context.HomeResidences.Remove(homeResidence);
            await _context.SaveChangesAsync();

            return homeResidence;
        }

        private bool HomeResidenceExists(int id)
        {
            return _context.HomeResidences.Any(e => e.Id == id);
        }
    }
}
