using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<Order> Orders { get; set; } = [];
        [Column(TypeName = "decimal(18,2)")]
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}