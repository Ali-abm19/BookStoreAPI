using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Status OrderStatus { get; set; }

        // connnections with other entities
        public Guid UserId { get; set; }
        public List<Book> Book { get; set; }

        public enum Status
        {
            Completed,
            Pending,
            Shipped,
            Cancelled,
        }
    }
}
