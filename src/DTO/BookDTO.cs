using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class BookDTO
    {
        public class CreateBookDto
        {
            public Guid Id { get; set; }
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public float Price { get; set; }
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }
        }

        public class ReadBookDto
        {
            public Guid Id { get; set; }
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public float Price { get; set; }
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }
        }

        public class UpdateBookDto
        {
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public float Price { get; set; }
            public int StockQuantity { get; set; }
            public Format BookFormat { get; set; }
        }
    }

    /*public enum Format
    {
        Audio,
        Paperback,
        Hardcover,
        Ebook,
    }*/
}
