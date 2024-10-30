using System.Linq.Expressions;
using BookStore.src.Database;
using BookStore.src.Entity;
using BookStore.src.Utils;
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
            return await _book.Include(i=> i.Category).FirstOrDefaultAsync(f=>f.BookId == id);
        }

        public async Task<List<Book>> GetAllAsync(PaginationOptions paginationOptions)
        {
            // Define an IQueryable to build the query dynamically
            IQueryable<Book> query = _book.Include(i => i.Category);

            if (!string.IsNullOrEmpty(paginationOptions.SearchByAuthor)) // search by author
            {
                query = query.Where(b =>
                    b.Author.ToLower().Contains(paginationOptions.SearchByAuthor.ToLower())
                );
            }

            if (!string.IsNullOrEmpty(paginationOptions.SearchByTitle)) // search by title
            {
                query = query.Where(b =>
                    b.Title.ToLower().Contains(paginationOptions.SearchByTitle.ToLower())
                );
            }

            // Apply sorting by price: "Low to high" or "High to low"
            if (paginationOptions.SortByPrice.ToLower() == "high_low")
            {
                query = query.OrderByDescending(b => b.Price);
            }
            else if (paginationOptions.SortByPrice.ToLower() == "low_high")
            {
                query = query.OrderBy(b => b.Price);
            }

            query = query.Where(b =>
                b.Price > paginationOptions.MinPrice && b.Price < paginationOptions.MaxPrice
            );
            // else // if null Low to high
            // query = query.OrderBy(b => b.Price);

            // // Apply pagination
            query = query.Skip(paginationOptions.Offset).Take(paginationOptions.Limit);

            return await query.ToListAsync();
        }

        public async Task<int> GetBooksCount()
        {
            return await _book.CountAsync();
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
