using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TraumaData;
using TraumaData.Models;

namespace TraumaRegistryAdmin.Pages.References
{
    public class DeleteModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public DeleteModel(TraumaData.Context context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reference = await _context.References.FindAsync(id);

            if (Reference != null)
            {
                _context.References.Remove(Reference);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
