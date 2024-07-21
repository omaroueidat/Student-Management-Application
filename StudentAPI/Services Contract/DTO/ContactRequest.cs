using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract.DTO
{
    public class ContactRequest
    {
        public string? ContactValue { get; set; }

        public bool isPrimary { get; set; }

        public Guid CodeValueId { get; set; }

        public Contact ToContact(Guid StudentId)
        {
            return new Contact { StudentId = StudentId, CodeValueId = CodeValueId, isPrimary = isPrimary, ContactValue = ContactValue };
        }
    }
}
