using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public Guid UserId { get; set; }
            public List<Guid> CartItemIds { get; set; }
            //public List<CartItems> CartItems { get; set; }
            //public Guid CartId { get; set; }
            //public double TotalPrice { get; set; }
        }

        // Read cart (get data)
        public class CartReadDto
        {
            public Guid CartId { get; set; }
            public Guid UserId { get; set; }
            public List<CartItems> CartItems { get; set; }
            public double TotalPrice { get; set; }
        }

        // Update cart
        public class CartUpdateDto
        {
            public List<CartItems> CartItems { get; set; }
            public double TotalPrice { get; set; } //= totalAmount += CartItems.GetAll.getPrice
        }
    }
}
