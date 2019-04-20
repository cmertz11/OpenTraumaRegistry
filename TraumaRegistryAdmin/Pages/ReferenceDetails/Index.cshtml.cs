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
    public class IndexModel : PageModel
    {
        private readonly TraumaData.Context _context;

        public IndexModel(TraumaData.Context context)
        {
            _context = context;
        }

        public IList<ReferenceDetail> ReferenceDetail { get;set; }

        public async Task OnGetAsync(int ReferenceId)
        {
            ReferenceDetail = await _context.ReferenceDetails
                .Where(r => r.ReferenceId == ReferenceId)
                .Include(r => r.Reference).ToListAsync();
        }
    }
}
