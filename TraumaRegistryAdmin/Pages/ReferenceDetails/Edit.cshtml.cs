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

namespace TraumaRegistryAdmin.Pages.ReferenceDetails
{
    public class EditModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public EditModel(TraumaData.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public ReferenceDetail ReferenceDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReferenceDetail = await _context.ReferenceDetails
                .Include(r => r.Reference).FirstOrDefaultAsync(m => m.Id == id);

            if (ReferenceDetail == null)
            {
                return NotFound();
            }
           ViewData["ReferenceId"] = new SelectList(_context.References, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ReferenceDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceDetailExists(ReferenceDetail.Id))
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

        private bool ReferenceDetailExists(int id)
        {
            return _context.ReferenceDetails.Any(e => e.Id == id);
        }
    }
}
