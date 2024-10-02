using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.DTO
{
    public class CartItemsDTO
    {
        public class CartItemsCreateDto
        {

            public Guid BookId { get; set; }
            public Guid CartId { get; set; }
            public int Quantity { get; set; }
        }

        // Read cart (get data)
        public class CartItemsReadDto
        {
            public Guid CartItemsId { get; set; }
            public Guid CartId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public Guid BookId { get; set; } 
            public ReadBookDto Book { get; set; }  //i want evrey thing about book
            public Guid? OrderId { get; set; } 
        }

        // Update cart
        public class CartItemsUpdateDto
        {
            public int Quantity { get; set; }
            public double Price { get; set; }
        }
    }
}
