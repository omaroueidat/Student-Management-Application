using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract.DTO
{
    public class StudentRequest
    {
        [DisplayName("Student Name")]
        [Required]
        [StringLength(50)]
        public string? StudentName { get; set; }

        [DisplayName("Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Gender")]
        [Required]
        public Gender gender { get; set; }

        public List<AddressRequest>? Addresses { get; set; }

        public List<ContactRequest>? Contacts { get; set; }

        public Student ToStudent()
        {
            return new Student { StudentName = StudentName, DateOfBirth = DateOfBirth, gender = gender };
        }
    }
}
