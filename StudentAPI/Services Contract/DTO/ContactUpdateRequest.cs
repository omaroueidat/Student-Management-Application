using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract.DTO
{
    public class ContactUpdateRequest
    {
        public Guid ContactId { get; set; }
        public string? ContactValue { get; set; }

        public bool isPrimary;

        public Guid CodeValueId { get; set; }

        public Contact ToContact()
        {
            return new Contact { ContactId = ContactId, CodeValueId = CodeValueId, isPrimary = isPrimary, ContactValue = ContactValue };
        }
    }
}
