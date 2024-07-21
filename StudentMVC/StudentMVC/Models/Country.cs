using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }

        [Required]
        public string? CountryName { get; set; }

        // for navigation
        public virtual List<Region>? Regions { get; set; }
    }
}
