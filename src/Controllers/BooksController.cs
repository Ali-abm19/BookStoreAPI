using BookStore.src.Entity;
using BookStore.src.Services.book;
using BookStore.src.Services.category;
using BookStore.src.Utils;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        protected readonly IBookService _bookService;
        protected readonly ICategoryService _categoryService;

        public BooksController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks(
            [FromQuery] PaginationOptions paginationOptions
        )
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
            var createdBook = await _bookService.CreateOneAsync(b);
            return Created("api/v1/Books" + createdBook.BookId, createdBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook([FromRoute] Guid id)
        {
            var b = _bookService.GetBookByIdAsync(id).Result;
            if (b == null)
            {
                return NotFound();
            }

            await _bookService.DeleteOneAsync(b.BookId);
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

        [HttpGet("category/{category}")]
        public async Task<ActionResult> GetBooksByCategory([FromRoute] string category)
        {
            var booksFromDB = await _bookService.GetAllAsyncWithConditions();
            var booksWithinCategory = booksFromDB.FindAll(cat =>
                cat.Category.CategoryName == category
            );

            if (booksWithinCategory == null)
            {
                return NotFound();
            }

            return Ok(booksWithinCategory);
        }
    }
}
