using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
        public Format BookFormat { get; set; }

        public enum Format
        {
            audio,
            paperback,
            hardcover,
            ebook,
        }

        /*public Format Format0 { get; set; }

        public Book() { }

        public Book(
            int id,
            string title,
            string author,
            string isbn,
            int stockQuantity,
            float price,
            Format format
        )
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            StockQuantity = stockQuantity;
            Price = price;
            Format0 = format;
        }*/
    }
}
