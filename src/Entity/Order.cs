using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }//good
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }
        public Status OrderStatus { get; set; }

        //public String log

        // connnections with other entities
        public Guid UserId { get; set; }
        public User User { get; set; }
       // public Guid CartId { get; set; }
        public LinkedList<CartItems> CartItems { get; set; }//i wamt list from the cat 1:21 more than book

        public enum Status
        {
            Completed,
            Pending,
            Shipped,
            Cancelled,
        }
    }
}
