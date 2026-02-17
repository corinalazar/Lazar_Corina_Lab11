using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class Appointment
    {
        public int Id { get; set; }

    
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        // Navigation Property pentru ServiceRecords 
        public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();

        // Navigation Property pentru Review
        public Review Review { get; set; }
    }
}