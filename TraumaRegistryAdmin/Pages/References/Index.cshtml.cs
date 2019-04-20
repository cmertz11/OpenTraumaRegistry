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
    public class IndexModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public IndexModel(TraumaData.Context context)
        {
            _context = context;
        }

        public IList<Reference> Reference { get;set; }

        public async Task OnGetAsync()
        {
            Reference = await _context.References.ToListAsync();
        }
    }
}
