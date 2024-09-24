using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
