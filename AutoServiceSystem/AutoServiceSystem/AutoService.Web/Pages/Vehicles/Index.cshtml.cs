using AutoService.Web.Data;
using AutoService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Web.Pages.Vehicles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Vehicle> Vehicles { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                // Admin vede toate vehiculele
                Vehicles = await _context.Vehicles.Include(v => v.User).ToListAsync();
            }
            else
            {
                // Client vede doar vehiculele lui
                Vehicles = await _context.Vehicles
                    .Where(v => v.UserId == user.Id)
                    .ToListAsync();
            }
        }
    }
}
