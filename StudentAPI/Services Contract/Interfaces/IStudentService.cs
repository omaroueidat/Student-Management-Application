using ServicesContract.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface IStudentService
    {
        Task<List<StudentResponse>> GetAllStudents();

        Task<StudentResponse?> GetStudent(Guid id);

        Task<StudentResponse> AddNewStudent(StudentRequest student);

        Task<List<StudentResponse>> FiterStudents(string? searchBy, string? searchString);


		Task<StudentResponse?> UpdateStudent(StudentUpdateRequest student);

        Task<bool> DeleteStudent(Guid? id);
    }
}
