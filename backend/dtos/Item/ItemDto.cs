using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos
{
    public class ItemDto
    {
        [Required]
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
    }
}