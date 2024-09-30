using BookStore.src.Entity;
using BookStore.src.Services.book;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.BookDTO;
using BookStore.src.Utils;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        protected readonly IBookService _bookService;
        
        public BooksController(IBookService service)
        {
            _bookService = service;
        }

       

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] PaginationOptions paginationOptions)
        {
            var bookList = await _bookService.GetAllAsync(paginationOptions);
            return Ok(bookList);
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
