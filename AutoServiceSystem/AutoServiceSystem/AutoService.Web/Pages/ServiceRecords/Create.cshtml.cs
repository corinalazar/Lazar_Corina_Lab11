using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoService.Web.Data;
using AutoService.Web.Models;

namespace AutoService.Web.Pages.ServiceRecords
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Web.Data.ApplicationDbContext _context;

        public CreateModel(AutoService.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id");
        ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ServiceRecord ServiceRecord { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ServiceRecords.Add(ServiceRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
