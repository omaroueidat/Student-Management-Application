using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CodeValue
    {
        [Key]
        public Guid CodeValueId { get; set; }

        [Required]
        public int CodeId { get; set; }


        [Required]
        [StringLength(50)]
        public string? Value { get; set; }

    }
}
