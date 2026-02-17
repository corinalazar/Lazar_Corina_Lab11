using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}