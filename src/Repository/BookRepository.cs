using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

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
