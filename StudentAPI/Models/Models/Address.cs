using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Address should be maximum 100 characters")]
        public string? AddressValue { get; set; }

        [Required]
        public bool isPrimary { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid CodeValueId { get; set; }

        // For Navigation

        [ForeignKey("RegionId")]
        public Region? Region { get; set; }
        
        [ForeignKey("CodeValueId")]
        public CodeValue? CodeValue { get; set; }
    }
}
