using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } =  string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}