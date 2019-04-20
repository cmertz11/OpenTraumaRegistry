using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TraumaData;
using TraumaData.Models;

namespace TraumaRegistryAdmin.Pages.References
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
            return Page();
        }

        [BindProperty]
        public Reference Reference { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.References.Add(Reference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}