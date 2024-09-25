using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public Status OrderStatus { get; set; } 

        public enum Status
        {
            Completed,
            Pending,
            Shipped,
            Cancelled,
        }
    }
}
