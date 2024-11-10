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
            public Guid CartId { get; set; } //probably should use this in the endpoint instead
        }

        public class OrderReadDto
        {
            public Guid OrderId { get; set; }
            public double TotalPrice { get; set; }
            public DateTime DateCreated { get; set; }
            public Order.Status OrderStatus { get; set; }
            public List<String> Log { get; set; }
            public Guid UserId { get; set; }
            public Cart Cart { get; set; }
        }

        //update
        public class OrderUpdateDto
        {
            [Required(ErrorMessage = "You must provide a status")]
            public Order.Status OrderStatus { get; set; }
        }
    }
}
