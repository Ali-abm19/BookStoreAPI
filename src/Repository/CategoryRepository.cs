using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Database;
using Microsoft.EntityFrameworkCore;


namespace BookStore.src.Repository
{
    public class CategoryRepository
    {
         // table - category
        protected DbSet<Category> _category;
        protected DatabaseContext _databaseContext;


        // DI
        public CategoryRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            // initialize the category in database
            _category = databaseContext.Set<Category>();
        }


        // method
        // create category
        public async Task<Category> CreateOneAsync(Category newCategory)
        {
            // logic
            // name: category1
            // add new category in category table 
            await _category.AddAsync(newCategory);
            // save change
            await _databaseContext.SaveChangesAsync();
            return newCategory;
            // id: auto
            // name: category1
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