using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Book
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public enum Format
        {
            audio,
            paperback,
            hardcover,
            ebook,
        }

        public Format BookFormat { get; set; }

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
            _id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            StockQuantity = stockQuantity;
            Price = price;
            BookFormat = format;
        }
    }
}
