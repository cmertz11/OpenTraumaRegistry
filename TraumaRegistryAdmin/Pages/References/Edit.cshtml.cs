using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TraumaData;
using TraumaData.Models;

namespace TraumaRegistryAdmin.Pages.References
{
    public class EditModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public EditModel(TraumaData.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Reference Reference { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reference = await _context.References.FirstOrDefaultAsync(m => m.Id == id);

            if (Reference == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(Reference.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReferenceExists(int id)
        {
            return _context.References.Any(e => e.Id == id);
        }
    }
}
