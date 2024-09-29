using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Repository
{
    public class CategoryRepository
    {
        // table - category
        //protected used in only in repo class - child class
        protected DbSet<Category> _category;
        protected DatabaseContext _databaseContext;

        // DI
        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext; //db
            // initialize the category in database
            _category = databaseContext.Set<Category>(); // table
        }

        // method
        // create category
        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            // add new category in category table
            await _category.AddAsync(newCategory);
            // save change
            await _databaseContext.SaveChangesAsync();
            return newCategory;
        }

        //Get All Categoreis
        public async Task<List<Category>> GetAllAsync()
        {
            return await _category.ToListAsync();
        }

        // get id
        public async Task<Category?> GetByIdAsync(Guid CategoryId)
        {
            return await _category.FindAsync(CategoryId);
        }

        // delete
        public async Task<bool> DeleteOneAsync(Category category)
        {
            _category.Remove(category);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // update category name & des
        public async Task<bool> UpdateOneAsync(Category updateCategory)
        {
            _category.Update(updateCategory);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

     
    }
}
