using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Region
    {
        public Guid RegionId { get; set; }

        [Required]
        [StringLength(30)]
        public string? RegionName { get; set; }

        [Required]
        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country? Country { get; set; }
    }
}
