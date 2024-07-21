using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }

        [Required]
        public string? CountryName { get; set; }

    }
}
