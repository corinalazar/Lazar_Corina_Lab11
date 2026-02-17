using AutoService.Web.Data;
using AutoService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Web.Pages.Vehicles
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; }

        // LOAD VEHICLE
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Vehicle = await _context.Vehicles.FindAsync(id);

            if (Vehicle == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);

            // Client poate edita doar masina lui
            if (User.IsInRole("Client") && Vehicle.UserId != currentUserId)
                return Forbid();

            return Page();
        }

        // SAVE EDIT
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var vehicleFromDb = await _context.Vehicles.FindAsync(Vehicle.Id);

            if (vehicleFromDb == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);

            // securitate
            if (User.IsInRole("Client") && vehicleFromDb.UserId != currentUserId)
                return Forbid();

            // update DOAR campurile editabile
            vehicleFromDb.Brand = Vehicle.Brand;
            vehicleFromDb.Model = Vehicle.Model;
            vehicleFromDb.Year = Vehicle.Year;
            vehicleFromDb.LicensePlate = Vehicle.LicensePlate;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
