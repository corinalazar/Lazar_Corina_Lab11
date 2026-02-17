using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Web.Data;
using AutoService.Web.Models;

namespace AutoService.Web.Pages.ServiceRecords
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Web.Data.ApplicationDbContext _context;

        public IndexModel(AutoService.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ServiceRecord> ServiceRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ServiceRecord = await _context.ServiceRecords
                .Include(s => s.Appointment)
                .Include(s => s.Service).ToListAsync();
        }
    }
}
