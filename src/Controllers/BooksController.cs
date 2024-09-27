using BookStore.src.DTO;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Services.book;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        protected readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById([FromRoute] Guid id)
        {
            var bookToReturn = await _bookService.GetBookByIdAsync(id);
            if (bookToReturn == null)
            {
                return NotFound();
            }

            return Ok(bookToReturn);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookDto b)
        {
            await _bookService.CreateOneAsync(b);
            return Created("api/v1/Books" + b.Id, b);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook([FromRoute] Guid id)
        {
            var b = _bookService.GetBookByIdAsync(id).Result;
            if (b == null)
            {
                return NotFound();
            }

            await _bookService.DeleteOneAsync(b.Id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(
            [FromRoute] Guid id,
            [FromBody] UpdateBookDto changes
        )
        {
            var isUpdated = await _bookService.UpdateOneAsync(id, changes);

            return Ok(isUpdated);
        }
    }
}
