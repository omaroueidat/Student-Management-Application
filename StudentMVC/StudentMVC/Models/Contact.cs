using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMVC.Models
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }

        [Required]
        public string? ContactValue { get; set; }

        [Required]
        public bool isPrimary { get; set; }

        [Required]
        public Guid CodeValueId { get; set; }

        public Guid StudentId { get; set; }


        [ForeignKey("CodeValueId")]
        public CodeValue? CodeValue { get; set; }
    }
}
