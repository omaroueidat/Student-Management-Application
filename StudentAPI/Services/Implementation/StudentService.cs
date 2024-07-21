using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServicesContract;
using ServicesContract.DTO;

namespace Services
{
    public class StudentService : IStudentService
    {

        private readonly AppDbContext _context;

        private readonly IAddressService _addressService;
        private readonly IContactService _contactService;
        public StudentService(AppDbContext context, IAddressService addressService, IContactService contactService)
        {
            _context = context;
            _addressService = addressService;
            _contactService = contactService;
        }

        private static StudentResponse ConvertStudentToStudentResponse(Student student)
        {
            StudentResponse studentResponse = student.ToStudentResponse();
            /*studentResponse.DateOfBirth = student.DateOfBirth;*/
            return studentResponse;
        }

        public async Task<StudentResponse> AddNewStudent(StudentRequest student)
        {
            // Get Student
            Student st = student.ToStudent();

            // Get Addresses and set the StudentId
            List<Address> addresses = student.Addresses.Select(a => a.ToAddress(st.StudentId)).ToList();
            
            //Get Contacts and set the StudentId in each contact
            List<Contact> contacts = student.Contacts.Select(c => c.ToContact(st.StudentId)).ToList();

            

         

            await _context.Students.AddAsync(st);

            await _contactService.AddNewContacts(student.Contacts, st.StudentId);

            await _addressService.AddNewAddresses(student.Addresses, st.StudentId);

            await _context.SaveChangesAsync();

            return ConvertStudentToStudentResponse(st);
        }

        public async Task<bool> DeleteStudent(Guid? id)
        {
            if (id == null)
            {
                return false;
            }

            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if (student is null)
            {
                return false;
            }

            _context.Students.Remove(student);

            // Delete the addresses associated to the student
            var addresses = _context.Addresses.Where(a=> a.StudentId == id);
            _context.RemoveRange(addresses);

            // Delete the contacts associated to the student
            var contacts = _context.Contacts.Where(c => c.StudentId == id);
            _context.RemoveRange(contacts);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<StudentResponse>> FiterStudents(string? searchBy, string? searchString)
        {
            List<StudentResponse> students = await GetAllStudents();

            if (searchBy is null || searchString is null)
            {
                return students;
            }

            switch (searchBy)
            {
                case nameof(StudentResponse.StudentId):
                    students = students.Where(s => s.StudentId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(StudentResponse.DateOfBirth):
                    students = students.Where(s => s.DateOfBirth.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(StudentResponse.StudentName):
					students = students.Where(s => s.StudentName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
					break;

                case nameof(StudentResponse.gender):
					students = students.Where(s => s.gender.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
					break;
            }

            return students;
        }

        public async Task<List<StudentResponse>> GetAllStudents()
        {
            List<StudentResponse>studentResponses = await _context.Students.Select(s => ConvertStudentToStudentResponse(s)).ToListAsync();

            return studentResponses;
        }

        public async Task<StudentResponse?> GetStudent(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.Addresses)
                    .ThenInclude(a => a.Region)
                        .ThenInclude(r => r.Country)
                .Include(s => s.Addresses)
                    .ThenInclude(a => a.CodeValue)
                .Include(s => s.Contacts)
                    .ThenInclude(c => c.CodeValue)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student is null)
            {
                return null;
            }

            

            return ConvertStudentToStudentResponse(student);


                
        }

        public async Task<StudentResponse?> UpdateStudent(StudentUpdateRequest student)
        {
            if (student is null)
            {
                throw new ArgumentNullException();  
            }

            Student? studentUpdate = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

            if (studentUpdate is null)
            {
                return null;
            }

            int isPrimaryAddressCount = student.Addresses.Count(a => a.isPrimary == true);

            int isPrimaryContactCount = student.Contacts.Count(c => c.isPrimary == true);

            if (isPrimaryAddressCount != 1)
            {
                throw new Exception("You Should Specify only one Primary Address");
            }

            if (isPrimaryContactCount != 1)
            {
                throw new Exception("You Should Specify only one Primary Contact");
            }


            UpdateDetails(student, studentUpdate);

            // Get the new addresses
            List<AddressRequest> addresses = student.Addresses
                .Where(a => a.AddressId.ToString() == "00000000-0000-0000-0000-000000000000")
                .Select(a => new AddressRequest()
                {
                    AddressValue = a.AddressValue,
                    CodeValueId = a.CodeValueId,
                    isPrimary = a.isPrimary,
                    RegionId = a.RegionId
                })
                .ToList();

            // Get the new Contacts
            List<ContactRequest> contacts = student.Contacts
                .Where(c => c.ContactId.ToString() == "00000000-0000-0000-0000-000000000000")
                .Select(c => new ContactRequest()
                {
                    CodeValueId = c.CodeValueId,
                    isPrimary = c.isPrimary,
                    ContactValue = c.ContactValue

                })
                .ToList();



            foreach(Address address in student.Addresses)
            {
                if (address.AddressId.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    await _addressService.UpdateAddress(student.StudentId, address);   
                }
            }

            foreach (Contact contact in student.Contacts)
            {
                if (contact.ContactId.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    await _contactService.UpdateContact(student.StudentId, contact);
                }
            }

            await _addressService.AddNewAddresses(addresses, student.StudentId);

            await _contactService.AddNewContacts(contacts, student.StudentId);

            await _context.SaveChangesAsync();

            return ConvertStudentToStudentResponse(studentUpdate);

        }

        private void UpdateDetails(StudentUpdateRequest req, Student student)
        {
            student.StudentName = req.StudentName;
            student.DateOfBirth = req.DateOfBirth;
            student.gender = req.gender;
        }
    }
}
