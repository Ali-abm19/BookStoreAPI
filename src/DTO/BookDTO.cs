using System.ComponentModel.DataAnnotations;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class BookDTO
    {
        public class CreateBookDto
        {
            public string? Isbn { get; set; }

            [Required]
            public string Title { get; set; }
            public string? Author { get; set; }

            [Range(1, double.MaxValue, ErrorMessage = "Price is not valid")]
            public double Price { get; set; }

            [Range(1, double.MaxValue, ErrorMessage = "Stock Quantitiy is not valid")]
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }

            [Required]
            public string CategoryName { get; set; }
        }

        public class ReadBookDto
        {
            public Guid BookId { get; set; }
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public double Price { get; set; }
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }
            public Category Category { get; set; }
        }

        public class UpdateBookDto
        {
            public string? Isbn { get; set; }
            public string? Title { get; set; }
            public string? Author { get; set; }

            [Range(1, double.MaxValue, ErrorMessage = "Price is not valid")]
            public double? Price { get; set; }

            [Range(1, double.MaxValue, ErrorMessage = "Stock Quantitiy is not valid")]
            public int? StockQuantity { get; set; }
            public Guid? CategoryId { get; set; }
        }

        public class BookListDto
        {
            public List<ReadBookDto>? Books { get; set; }
            public int Count { get; set; }
        }
    }
}
