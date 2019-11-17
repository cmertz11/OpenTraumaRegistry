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
    public class InjuryTypesController : ControllerBase
    {
        private readonly Context _context;

        public InjuryTypesController(Context context)
        {
            _context = context;
        }

        // GET: api/InjuryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InjuryTypes>>> GetInjuryTypes()
        {
            return await _context.InjuryTypes.ToListAsync();
        }

        // GET: api/InjuryTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InjuryTypes>> GetInjuryTypes(int id)
        {
            var injuryTypes = await _context.InjuryTypes.FindAsync(id);

            if (injuryTypes == null)
            {
                return NotFound();
            }

            return injuryTypes;
        }

        // PUT: api/InjuryTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInjuryTypes(int id, InjuryTypes injuryTypes)
        {
            if (id != injuryTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(injuryTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InjuryTypesExists(id))
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

        // POST: api/InjuryTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InjuryTypes>> PostInjuryTypes(InjuryTypes injuryTypes)
        {
            _context.InjuryTypes.Add(injuryTypes);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInjuryTypes", new { id = injuryTypes.Id }, injuryTypes);
            return Ok(injuryTypes);
        }

        // DELETE: api/InjuryTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InjuryTypes>> DeleteInjuryTypes(int id)
        {
            var injuryTypes = await _context.InjuryTypes.FindAsync(id);
            if (injuryTypes == null)
            {
                return NotFound();
            }

            _context.InjuryTypes.Remove(injuryTypes);
            await _context.SaveChangesAsync();

            return injuryTypes;
        }

        private bool InjuryTypesExists(int id)
        {
            return _context.InjuryTypes.Any(e => e.Id == id);
        }
    }
}
