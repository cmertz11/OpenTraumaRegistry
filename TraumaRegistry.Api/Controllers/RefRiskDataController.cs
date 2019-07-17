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
    public class RefRiskDataController : ControllerBase
    {
        private readonly Context _context;

        public RefRiskDataController(Context context)
        {
            _context = context;
        }

        // GET: api/RefRiskData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefRiskData>>> GetRefRiskData()
        {
            return await _context.RefRiskData.ToListAsync();
        }

        // GET: api/RefRiskData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefRiskData>> GetRefRiskData(int id)
        {
            var refRiskData = await _context.RefRiskData.FindAsync(id);

            if (refRiskData == null)
            {
                return NotFound();
            }

            return refRiskData;
        }

        // PUT: api/RefRiskData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefRiskData(int id, RefRiskData refRiskData)
        {
            if (id != refRiskData.Id)
            {
                return BadRequest();
            }

            _context.Entry(refRiskData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefRiskDataExists(id))
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

        // POST: api/RefRiskData
        [HttpPost]
        public async Task<ActionResult<RefRiskData>> PostRefRiskData(RefRiskData refRiskData)
        {
            _context.RefRiskData.Add(refRiskData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefRiskData", new { id = refRiskData.Id }, refRiskData);
        }

        // DELETE: api/RefRiskData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RefRiskData>> DeleteRefRiskData(int id)
        {
            var refRiskData = await _context.RefRiskData.FindAsync(id);
            if (refRiskData == null)
            {
                return NotFound();
            }

            _context.RefRiskData.Remove(refRiskData);
            await _context.SaveChangesAsync();

            return refRiskData;
        }

        private bool RefRiskDataExists(int id)
        {
            return _context.RefRiskData.Any(e => e.Id == id);
        }
    }
}
