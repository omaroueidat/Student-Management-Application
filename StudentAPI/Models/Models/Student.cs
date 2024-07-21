using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string? StudentName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender gender { get; set; }

        // For Navigation Purposes
        public virtual List<Address>? Addresses { get; set; }

        public virtual List<Contact>? Contacts { get; set; }
    }
}
