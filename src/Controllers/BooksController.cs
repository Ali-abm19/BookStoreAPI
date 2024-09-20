using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
