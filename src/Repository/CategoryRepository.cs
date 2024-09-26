using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Repository
{
    public class CategoryRepository
    {
        // table 
        protected DbSet<Category> _category;
        protected DatabaseContext _databaseContext;


        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _category = databaseContext.Set<Category>();
        }

        // create category
        public async Task<Category> CreateOneAsync(Category newCategory)
        {

            await _category.AddAsync(newCategory);
            await _databaseContext.SaveChangesAsync();
            return newCategory;

        }

        // get id
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _category.FindAsync(id);
        }

        // delete
        public async Task<bool> DeleteOneAsync(Category category)
        {
            _category.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update
        public async Task<bool> UpdateOneAsync(Category updateCategory)
        {
            _category.Update(updateCategory);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
