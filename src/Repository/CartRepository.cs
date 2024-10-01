using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Repository
{
    public class CartRepository
    {
        // table
        protected DbSet<Cart> _cart;
        protected DatabaseContext _databaseContext;

        public CartRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _cart = databaseContext.Set<Cart>();
        }

        // create new cart
        public async Task<Cart> CreateOneAsync(Cart newCart)
        {
            newCart.CartItems = null;
            await _cart.AddAsync(newCart);
            await _databaseContext.SaveChangesAsync();
            return newCart;
        }

        //get cart by ID
        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _cart.FindAsync(id);
        }

        // get all
        public async Task<List<Cart>> GetAllAsync()
        {
            return await _cart.ToListAsync();
        }

        //delete
        public async Task<bool> DeleteOneAsync(Cart cart)
        {
            _cart.Remove(cart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //update cart
        public async Task<bool> UpdateOneAsync(Cart updateCart)
        {
            _cart.Update(updateCart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
