using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos
{
    public class UpdateItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}