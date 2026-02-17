namespace AutoService.Web.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public string Notes { get; set; }
    }
}