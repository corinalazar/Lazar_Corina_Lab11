using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }
    }
}