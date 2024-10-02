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
            // Initialize TotalPrice to 0
            newCart.TotalPrice = 0;

            if (newCart.CartItems != null && newCart.CartItems.Any())
            {
                // Calculate TotalPrice based on quantity and price
                newCart.TotalPrice = newCart.CartItems.Sum(item => item.Price * item.Quantity);
            }

            await _cart.AddAsync(newCart);
            await _databaseContext.SaveChangesAsync();
            return newCart;
        }

        //get cart by ID
        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }

  
        public async Task<List<Cart>> GetAllAsync()
        {
            return await _cart
                .Include(c => c.CartItems) // Include CartItems 
                .ToListAsync();
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
