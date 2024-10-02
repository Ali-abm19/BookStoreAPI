using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.DTO
{
    public class CartItemsDTO
    {
        public class CartItemsCreateDto
        {
            //public Guid CartId { get; set; }
            //public double TotalPrice { get; set; }
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
            public ReadBookDto Book { get; set; } //i want evrey thing about book
        }

        // Update cart
        public class CartItemsUpdateDto
        {
            public int Quantity { get; set; }
            public double Price { get; set; }
        }
    }
}
