using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        public ICollection<ServiceRecord> ServiceRecords { get; set; }
    }
}