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
            var result = books
              .Where(c =>
              {
                  string searchTerm = paginationOptions.Search.ToLower();

                  if (searchTerm.StartsWith("author:"))
                  {
                      string authorName = searchTerm.Replace("author:", "").Trim();
                      return c.Author.ToLower().Contains(authorName);
                  }

                  else if (searchTerm.StartsWith("title:"))
                  {
                      string title = searchTerm.Replace("title:", "").Trim();
                      return c.Title.ToLower().Contains(title);
                  }
                  return false;
              })
              .Skip(paginationOptions.Offset)
              .Take(paginationOptions.Limit)
              .ToList();

            return result;
           
            //    var result = _book.Where(c => c.Title.ToLower().Contains(paginationOptions.Search.ToLower()));
            //    return await result.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();
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
