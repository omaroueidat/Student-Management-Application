using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMVC.Models.DTO
{
    public class StudentUpdateRequest
    {
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender gender { get; set; }

        public List<Address>? Addresses { get; set; }
        public List<Contact>? Contacts { get; set; }

        public Student ToStudent()
        {
            return new Student {StudentId = StudentId, StudentName = StudentName, DateOfBirth = DateOfBirth, gender = gender };
        }
    }
}
