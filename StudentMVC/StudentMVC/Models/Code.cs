﻿using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class Code
    {
        [Key]
        public int CodeId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
