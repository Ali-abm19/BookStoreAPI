using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class BookDTO
    {
        public class CreateBookDto
        {
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public double Price { get; set; }
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }
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
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public double Price { get; set; }
            public int StockQuantity { get; set; }
            public Guid CategoryId { get; set; }
        }
    }
}
