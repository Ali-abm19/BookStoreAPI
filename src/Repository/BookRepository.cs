using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;
using BookStore.src.Utils;

namespace BookStore.src.Repository
{
    public class BookRepository
    {
        protected DbSet<Book> _book;
        protected DatabaseContext _databaseContext;

        public BookRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _book = databaseContext.Set<Book>();
        }

        public async Task<Book> CreateOneAsync(Book b)
        {
            await _book.AddAsync(b);
            await _databaseContext.SaveChangesAsync();

            return b;
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await _book.FindAsync(id);
        }

        public async Task<List<Book>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var books = await _book.ToListAsync();
            // Apply search and pagination
            var result = books
              .Where(c =>
              {
                  string searchTerm = paginationOptions.Search.ToLower();

                  if (searchTerm.StartsWith("author:")) // Search by Author
                  {
                      string authorName = searchTerm.Replace("author:", "").Trim();
                      return c.Author.ToLower().Contains(authorName);
                  }

                  else if (searchTerm.StartsWith("title:")) // Search by Title
                  {
                      string title = searchTerm.Replace("title:", "").Trim();
                      return c.Title.ToLower().Contains(title);
                  }
                  return false; // Return false if no valid search prefix is used
              })
              .Skip(paginationOptions.Offset)
              .Take(paginationOptions.Limit)
              .ToList();

            return result;

        }

        public async Task<List<Book>> GetAllAsyncWithConditions() //(Func<Book, bool> expression)
        {
            var list = await _book.Include(i => i.Category).ToListAsync();
            return list;
        }

        public async Task<bool> DeleteOneAsync(Book book)
        {
            _book.Remove(entity: book);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Book updateBook)
        {
            _book.Update(updateBook);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
