using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using static BookStore.src.DTO.CartItemsDTO;
using System.ComponentModel.DataAnnotations;

namespace BookStore.src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            [Required(ErrorMessage = "User ID is required.")]
            public Guid UserId { get; set; }

        }

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
            [Required(ErrorMessage = "CartItems are required.")]
            public List<CartItems> CartItems { get; set; }
            
        }
    }
}
