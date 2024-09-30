using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class OrderDTO
    {
        //create order 
        public class OrderCreateDto
        {
            public float TotalPrice { get; set; }

        }

        // read or get infoe of the order 
        public class OrderReadDto
        {
            public Guid OrderId { get; set; }
            public float TotalPrice { get; set; }
            public DateTime DateCreated { get; set; }
            public Order.Status OrderStatus { get; set; }
        }

        //update 
        public class OrderUpdateDto
        {

            public DateTime? DateUpdated { get; set; }//update the date the updated happend
            public Order.Status OrderStatus { get; set; }
            public float? TotalPrice { get; set; } // Nullable  to allow price updates selectively

        }
    }
}