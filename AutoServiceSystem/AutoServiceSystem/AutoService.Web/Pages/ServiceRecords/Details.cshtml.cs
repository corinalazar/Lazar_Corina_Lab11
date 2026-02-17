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
    public class DetailsModel : PageModel
    {
        private readonly AutoService.Web.Data.ApplicationDbContext _context;

        public DetailsModel(AutoService.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ServiceRecord ServiceRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicerecord = await _context.ServiceRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (servicerecord == null)
            {
                return NotFound();
            }
            else
            {
                ServiceRecord = servicerecord;
            }
            return Page();
        }
    }
}
