using BookStore.src.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        static List<Book> books =
        [
            new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Yellowface",
                Author = "R.F. Kuang",
                Isbn = "9780063250833",
                StockQuantity = 5,
                Price = 15.81f,
                BookFormat = Format.Hardcover,
            },
        ];

        [HttpGet]
        public ActionResult GetBooks()
        {
            //pagination:
            int nOfPages = 5,
                sizeOfPage = 3;
            var listToReturn = books
                .Skip(nOfPages * sizeOfPage - nOfPages)
                .Take(sizeOfPage)
                .ToList();
            return Ok(listToReturn);
        }

        [HttpGet("{id}")]
        public ActionResult GetBookById(Guid id)
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
            return Created("api/v1/Books" + b.Id, b);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(Guid id)
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
        public ActionResult UpdateBook(Guid id, Book newBook)
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
            bookToUpdate.Isbn = newBook.Isbn;
            bookToUpdate.BookFormat = newBook.BookFormat;

            return Ok(bookToUpdate);
        }
    }
}
