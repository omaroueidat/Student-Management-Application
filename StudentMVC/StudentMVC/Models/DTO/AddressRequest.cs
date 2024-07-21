using StudentMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMVC.Models.DTO
{
    public class AddressRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Address should be maximum 100 characters")]
        public string? AddressValue { get; set; }

        [Required]
        public bool isPrimary { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid CodeValueId { get; set; }

        public Address ToAddress(Guid StudentId)
        {
            return new Address { StudentId = StudentId ,AddressValue = AddressValue, CodeValueId = CodeValueId, RegionId = RegionId, isPrimary = isPrimary };
        }
    }
}
