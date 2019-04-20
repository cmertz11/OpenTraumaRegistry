using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TraumaData;
using TraumaData.Models;

namespace TraumaRegistryAdmin.Pages.ReferenceDetails
{
    public class CreateModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public CreateModel(TraumaData.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ReferenceId"] = new SelectList(_context.References, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ReferenceDetail ReferenceDetail { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReferenceDetails.Add(ReferenceDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}