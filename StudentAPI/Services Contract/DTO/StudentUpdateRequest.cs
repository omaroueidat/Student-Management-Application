using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract.DTO
{
    public class StudentUpdateRequest
    {
        public string? StudentName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender gender { get; set; }

        public List<Address>? Addresses { get; set; }
        public List<Contact>? Contacts { get; set; }

        public Student ToStudent()
        {
            return new Student {StudentName = StudentName, DateOfBirth = DateOfBirth, gender = gender };
        }
    }
}
