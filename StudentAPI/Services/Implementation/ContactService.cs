using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServicesContract;
using ServicesContract.DTO;

namespace Services
{
	public class ContactService : IContactService
	{
		private readonly AppDbContext _context;

		public ContactService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<int> AddNewContacts(List<ContactRequest> contactRequest, Guid StudentId)
		{
			var contacts = contactRequest.Select(c => c.ToContact(StudentId)).ToList();

			await _context.Contacts.AddRangeAsync(contacts);

			return contacts.Count;
		}


        public Task<List<Contact>> GetAllContacts(Guid studentId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateContact(Guid studentId, Contact contact)
		{
			var contactToUpdate = await _context.Contacts
					.FirstOrDefaultAsync(c => c.ContactId == contact.ContactId);

			if (contactToUpdate is null || contactToUpdate.StudentId != studentId)
			{
				return false;
			}

			contactToUpdate.ContactValue = contact.ContactValue;
			contactToUpdate.CodeValueId = contact.CodeValueId;
			contactToUpdate.isPrimary = contact.isPrimary;

			await _context.SaveChangesAsync();

			return true;
		}
        public async Task<bool> DeleteContact(Guid? ContactId)
        {
			var contact = await _context.Contacts
				.FirstOrDefaultAsync(c => c.ContactId == ContactId);

			if (contact is null)
			{
				return false;
			}

			_context.Contacts.Remove(contact);

			await _context.SaveChangesAsync();

			return true;
        }
	}
}
