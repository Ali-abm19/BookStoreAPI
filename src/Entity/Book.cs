namespace BookStore.src.Entity;

public class Book
{
    public Guid BookId { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public Format BookFormat { get; set; }

    //connections to other entities
    public required Guid CategoryId { get; set; }
    public Category Category { get; set; }
}

public enum Format
{
    Audio,
    Paperback,
    Hardcover,
    Ebook,
}
