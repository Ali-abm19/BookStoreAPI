using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        // connnections with other entities
        public User User { get; set; }
        public List<Book> Book { get; set; }
    }
}
