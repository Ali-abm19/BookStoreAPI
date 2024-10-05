using System.ComponentModel.DataAnnotations;
using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.DTO
{
    public class CartItemsDTO
    {
        public class CartItemsCreateDto
        {
            [Required(ErrorMessage = "Book ID is required.")]
            public Guid BookId { get; set; }

            [Required(ErrorMessage = "Cart ID is required.")]
            public Guid CartId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
            public int Quantity { get; set; }
        }

        // Read cart (get data)
        public class CartItemsReadDto
        {
            public Guid CartItemsId { get; set; }
            public Guid CartId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public Book Book { get; set; }
        }

        // Update cart
        public class CartItemsUpdateDto
        {
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
            public int Quantity { get; set; }

            [Required(ErrorMessage = "Book ID is required.")]
            // book id is already provided in the endpoint so this is not needed.
            //However, I'll keep it because i'm not sure wher exacly feda used it. @ali
            public Guid BookId { get; set; }

            // public double Price { get; set; }
            // add book id or book
        }
    }
}
