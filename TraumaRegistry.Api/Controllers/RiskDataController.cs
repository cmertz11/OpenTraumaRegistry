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
    public class RiskDataController : ControllerBase
    {
        private readonly Context _context;

        public RiskDataController(Context context)
        {
            _context = context;
        }

        // GET: api/RiskData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiskData>>> GetRiskData()
        {
            return await _context.RiskData.ToListAsync();
        }

        // GET: api/RiskData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RiskData>> GetRiskData(int id)
        {
            var riskData = await _context.RiskData.FindAsync(id);

            if (riskData == null)
            {
                return NotFound();
            }

            return riskData;
        }

        // PUT: api/RiskData/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiskData(int id, RiskData riskData)
        {
            if (id != riskData.Id)
            {
                return BadRequest();
            }

            _context.Entry(riskData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskDataExists(id))
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

        // POST: api/RiskData
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RiskData>> PostRiskData(RiskData riskData)
        {
            _context.RiskData.Add(riskData);
            await _context.SaveChangesAsync();

            return Ok(riskData);

            //return CreatedAtAction("GetRiskData", new { id = riskData.Id }, riskData);

        }

        // DELETE: api/RiskData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RiskData>> DeleteRiskData(int id)
        {
            var riskData = await _context.RiskData.FindAsync(id);
            if (riskData == null)
            {
                return NotFound();
            }

            _context.RiskData.Remove(riskData);
            await _context.SaveChangesAsync();

            return riskData;
        }

        private bool RiskDataExists(int id)
        {
            return _context.RiskData.Any(e => e.Id == id);
        }
    }
}
