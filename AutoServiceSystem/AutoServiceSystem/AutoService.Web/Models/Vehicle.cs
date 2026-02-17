using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; }

        [Required]
        public string UserId { get; set; }   // FK către ApplicationUser

        public ApplicationUser User { get; set; }  // Navigation property
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}