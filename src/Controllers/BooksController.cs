using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class BooksController : ControllerBase
    {
        static List<Book> books = new List<Book>
        {
            new Book(
                1,
                "Yellowface",
                "R.F. Kuang",
                "9780063250833",
                5,
                15.81f,
                Book.Format.hardcover
            ),
            new Book(
                2,
                "Weyward",
                "Emilia Hart",
                "9781250280800",
                3,
                13.76f,
                Book.Format.paperback
            ),
            new Book(
                3,
                "The Hunger Games",
                "Suzanne Collins",
                "0439023483",
                3,
                12.79f,
                Book.Format.hardcover
            ),
            new Book(
                4,
                "Catching Fire",
                "Suzanne Collins",
                "0439023491",
                15,
                14.99f,
                Book.Format.audio
            ),
            // new Book(5, "The Hunger Games", "Suzanne Collins", "1338334921", 15, 44.99f, Book.Format.audio)
        };

        [HttpGet]
        public ActionResult GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult GetBookById(int id)
        {
            Book? bookToReturn = books.FirstOrDefault(i => i.Id == id);
            if (bookToReturn == null)
            {
                return NotFound();
            }

            return Ok(bookToReturn);
        }

        [HttpPost]
        public ActionResult CreateBook(Book b)
        {
            books.Add(b);
            return Created("", b);
            //return CreatedAtAction(nameof(GetBookById), new { id = b.Id }, b);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            Book? b = books.FirstOrDefault(i => i.Id == id);
            if (b == null)
            {
                return NotFound();
            }

            books.Remove(b);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, Book newBook)
        {
            Book? bookToUpdate = books.FirstOrDefault(i => i.Id == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            bookToUpdate.Id = newBook.Id;
            bookToUpdate.Title = newBook.Title;
            bookToUpdate.Author = newBook.Author;
            bookToUpdate.Price = newBook.Price;
            bookToUpdate.StockQuantity = newBook.StockQuantity;
            bookToUpdate.ISBN = newBook.ISBN;
            bookToUpdate.BookFormat = newBook.BookFormat;

            return Ok(bookToUpdate);
        }
    }
}
