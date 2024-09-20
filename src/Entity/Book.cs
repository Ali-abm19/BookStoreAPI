using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Book(
        int id,
        string title,
        string author,
        string isbn,
        int stockQuantity,
        float price,
        Book.Format format
    )
    {
        public int Id { get; set; } = id;
        public string ISBN { get; set; } = isbn;
        public string Title { get; set; } = title;
        public string Author { get; set; } = author;
        public float Price { get; set; } = price;
        public int StockQuantity { get; set; } = stockQuantity;
        public Format BookFormat { get; set; } = format;

        public enum Format
        {
            audio,
            paperback,
            hardcover,
            ebook,
        }
    }
}
