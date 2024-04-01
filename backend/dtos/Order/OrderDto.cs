using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.dtos
{
    public class OrderDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }
    }
}