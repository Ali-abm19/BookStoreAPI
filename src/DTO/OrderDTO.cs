using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.DTO
{
    public class OrderDTO
    {
        //create order
        public class OrderCreateDto
        {
            //public DateTime DateCreated { get; set; }
            //public Guid OrderId { get; set; }
            //public Guid UserId { get; set; }
            //public float TotalPrice { get; set; }//
            //public List<OrderDetailCreateDto> OrderDetails { get; set; }
            public Guid CartId { get; set; } 
            //Manar used cartItems here. 
            //this is redundent logic for cart. we should just use cart @ali
        }

        // read or get infoe of the order
        public class OrderReadDto
        {
            public Guid OrderId { get; set; }
            public double TotalPrice { get; set; } //i'm changing this to double for consistency accross the code @ali
            public DateTime DateCreated { get; set; }
            public Order.Status OrderStatus { get; set; }
            public Guid UserId { get; set; }
            //public User user { get; set; }
            //user id will be enuogh we don't need the whole User object here.
            //I changed this in OrderDto as well @ali
            public Cart Cart { get; set; }
        }

        //update
        public class OrderUpdateDto
        {
            public DateTime? DateUpdated { get; set; } //update the date the updated happend
            //this should be handled in the repo. the user shouldn't write the date!
            //but i'll keep it for now @ali

            [Required(ErrorMessage = "You must provide a status")]
            public Order.Status OrderStatus { get; set; }
            public float? TotalPrice { get; set; } // Nullable  to allow price updates selectively
        }
    }
}
