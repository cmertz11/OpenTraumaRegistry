using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TraumaData;
using TraumaData.Models;

namespace TraumaRegistryAdmin.Pages.ReferenceDetails
{
    public class DeleteModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public DeleteModel(TraumaData.Context context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReferenceDetail = await _context.ReferenceDetails.FindAsync(id);

            if (ReferenceDetail != null)
            {
                _context.ReferenceDetails.Remove(ReferenceDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
