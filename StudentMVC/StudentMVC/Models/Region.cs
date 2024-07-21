using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class Region
    {
        public Guid RegionId { get; set; }

        [Required]
        [StringLength(30)]
        public string? RegionName { get; set; }

        [Required]
        public Guid CountryId { get; set; }

        // For Navigation
        public virtual Country? Country { get; set; }

    }
}
