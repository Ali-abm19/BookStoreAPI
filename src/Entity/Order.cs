using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Status OrderStatus { get; set; }

        //public String log

        // connnections with other entities
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public enum Status
        {
            Completed,
            Pending,
            Shipped,
            Cancelled,
        }
    }
}
