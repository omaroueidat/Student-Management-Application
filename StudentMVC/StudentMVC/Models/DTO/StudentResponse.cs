using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentMVC.Models.DTO
{
    public class StudentResponse
    {
        [DisplayName("Student Id")]
        public Guid StudentId { get; set; }

        [DisplayName("Student Name")]
        public string? StudentName { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Gender")]
        public Gender gender { get; set; }

        public List<Address>? Addresses { get; set; }
        public List<Contact>? Contacts { get; set; }

        public StudentUpdateRequest ToStudentUpdateRequest()
        {
            return new StudentUpdateRequest { StudentId = StudentId, StudentName = StudentName, DateOfBirth = DateOfBirth, gender=gender, Addresses = Addresses, };
        }
    }

    public static class StudentExtensions
    {
        public static StudentResponse ToStudentResponse(this Student student)
        {
            return new StudentResponse()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                DateOfBirth = student.DateOfBirth,
                gender = student.gender,
                Addresses = student.Addresses,
                Contacts = student.Contacts
            };
        }
    }
}
