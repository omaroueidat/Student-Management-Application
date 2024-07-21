using Entities.Models;
using ServicesContract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface IContactService
    {
        Task<int> AddNewContacts(List<ContactRequest> contactRequest, Guid StudentId);
        Task<List<Contact>> GetAllContacts(Guid studentId);
        Task<bool> UpdateContact(Guid studentId, Contact contact);

        Task<bool> DeleteContact(Guid? ContactId);
    }
}
