using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public Guid UserId { get; set; }

        }

        /*public class AddCartItemDto
        {
            public CartItems CartItems { get; set; }
        }*/

        // Read cart (get data)
        public class CartReadDto
        {
            public Guid CartId { get; set; }
            public Guid UserId { get; set; }
            public List<CartItemsReadDto> CartItems { get; set; } // Use the DTO
            public double TotalPrice { get; set; }
        }

        // Update cart
        public class CartUpdateDto
        {
            public List<CartItems> CartItems { get; set; }
            //updated in the CartRepo ->   //public double TotalPrice { get; set; } //= totalAmount += CartItems.GetAll.getPrice
        }
    }
}
