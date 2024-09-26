namespace BookStore.src.Entity;

public class Book
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public float Price { get; set; }
    public int StockQuantity { get; set; }
    public Format BookFormat { get; set; }
}

public enum Format
{
    Audio,
    Paperback,
    Hardcover,
    Ebook,
}
